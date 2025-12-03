using System.Security.Claims;
using FirstMVC.Data;
using FirstMVC.Models;
using FirstMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstMVC.Controllers;

//Controller for story progression with branching dialog choices.

[Authorize]
public class StoryController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<StoryController> _logger;

    // Narrative act IDs – your three big beats
    private const int ACT1_ID = 1;
    private const int ACT2_ID = 2;
    private const int ACT3_ID = 3;

    public StoryController(ApplicationDbContext context, ILogger<StoryController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Display the current story act and choices
    public async Task<IActionResult> Play(string? error = null)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var progress = await _context.UserProgress
            .Include(p => p.CurrentStoryAct)!.ThenInclude(a => a!.Choices)
            .Include(p => p.CurrentStoryAct)!.ThenInclude(a => a!.Character)
            .FirstOrDefaultAsync(p => p.UserID == userId);

        // Create new progress if none exists
        if (progress == null)
        {
            var startingAct = await _context.StoryActs
                .Include(a => a.Choices)
                .Include(a => a.Character)
                .FirstOrDefaultAsync(a => a.StoryActId == ACT1_ID);

            if (startingAct == null)
            {
                _logger.LogError("Starting act not found");
                return View("Error", new ErrorViewModel { RequestId = "STORY_NO_START" });
            }

            progress = new UserProgressDB
            {
                UserID = userId,
                CurrentStoryActId = startingAct.StoryActId,
                CurrentStoryAct = startingAct,
                Trust = 0,
                EndingType = null,
                LastUpdated = DateTime.UtcNow
            };

            _context.UserProgress.Add(progress);
            await _context.SaveChangesAsync();
        }

        // Load current act if not already loaded
        if (progress.CurrentStoryAct == null)
        {
            progress.CurrentStoryAct = await _context.StoryActs
                .Include(a => a.Choices)
                .Include(a => a.Character)
                .FirstAsync(a => a.StoryActId == progress.CurrentStoryActId);
        }

        // If we already have an ending, go straight to the ending screen.
        if (!string.IsNullOrEmpty(progress.EndingType))
        {
            return RedirectToAction(nameof(Ending));
        }

        var vm = StoryPlayViewModel.FromUserProgress(progress);
        vm.Trust = progress.Trust;
        vm.ErrorMessage = error;
        return View(vm);
    }

    // Process the player's choice and advance the story
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Choose(StoryPlayViewModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        if (!model.SelectedChoiceId.HasValue)
        {
            return await Play(error: "You must pick a choice.");
        }

        var progress = await _context.UserProgress
            .Include(p => p.CurrentStoryAct)!.ThenInclude(a => a!.Choices)
            .FirstOrDefaultAsync(p => p.UserID == userId);

        if (progress == null)
        {
            return await Play(error: "Your progress was not found.");
        }

        var choice = await _context.Choices
            .FirstOrDefaultAsync(c => c.Id == model.SelectedChoiceId.Value);

        
        // Only allow moving to same act or next act (no skipping or going backwards)
        int nextActId = choice.NextActId;
        if (nextActId < progress.CurrentStoryActId || nextActId > progress.CurrentStoryActId + 1)
        {
            return await Play(error: "Invalid story progression.");
        }

        // Apply trust change from this choice (-5, 0, +5 etc.)
        progress.Trust += choice.TrustChange;

        // Move to next act if needed
        if (nextActId != progress.CurrentStoryActId)
        {
            var nextAct = await _context.StoryActs
                .Include(a => a.Choices)
                .Include(a => a.Character)
                .FirstOrDefaultAsync(a => a.StoryActId == choice.NextActId);

            if (nextAct == null)
            {
                _logger.LogError("Next act {ActId} not found", nextActId);
                return await Play(error: "Could not load next part of story.");
            }

            progress.CurrentStoryActId = nextAct.StoryActId;
            progress.CurrentStoryAct = nextAct;
        }

        // Set ending when reaching Act 3
        if (progress.CurrentStoryActId == ACT3_ID && string.IsNullOrEmpty(progress.EndingType))
        {
            progress.EndingType = CalculateEnding(progress.Trust);
        }

        progress.LastUpdated = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to save progress after choice for user {UserId}", userId);
            return await Play(error: "Your choice could not be saved. Please try again.");
        }

        if (!string.IsNullOrEmpty(progress.EndingType))
        {
            return RedirectToAction(nameof(Ending));
        }

        return RedirectToAction(nameof(Play));
    }

    // Chooses ending based on trust value
    private static string CalculateEnding(int trust)
    {
        if (trust < 50) return "Bad";           // 0-49 = Bad ending
        if (trust < 100) return "Good";         // 50-99 = Good ending
        return "True";                          // 100+ = True ending
    }

    //
    // Displays the final ending screen based on the stored ending type.
    public async Task<IActionResult> Ending()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var progress = await _context.UserProgress.FirstOrDefaultAsync(p => p.UserID == userId);
        if (progress == null || string.IsNullOrEmpty(progress.EndingType))
        {
            return RedirectToAction(nameof(Play));
        }

        ViewData["EndingType"] = progress.EndingType;
        ViewData["Trust"] = progress.Trust;
        return View();
    }

    // Reset game back to character selection screen, trust = 0, act = 1
    public async Task<IActionResult> ResetAndStart()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var progress = await _context.UserProgress.FirstOrDefaultAsync(p => p.UserID == userId);

        if (progress != null)
        {
            progress.CurrentStoryActId = ACT1_ID;
            progress.Trust = 0;
            progress.EndingType = null;
            progress.LastUpdated = DateTime.UtcNow;
            progress.CurrentStoryAct = null;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Character", "Home");
    }
}

