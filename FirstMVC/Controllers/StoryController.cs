using System;
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

    // Starting scene ID (Scene 1, which belongs to Act 1 category)
    private const int ACT1_ID = 1;
    // Note: ACT2_ID and ACT3_ID are kept for compatibility, but Act categories are now tracked via Description field
    private const int ACT2_ID = 2;
    private const int ACT3_ID = 3;
    private const int MIN_TRUST = 0;
    private const int MAX_TRUST = 100;

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

            // Get the user's selected character
            var characterSelection = await _context.UserCharacterSelection
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (characterSelection == null)
            {
                _logger.LogError("No character selected for user");
                return RedirectToAction("Character", "Home");
            }

            progress = new UserProgressDB
            {
                UserID = userId,
                CurrentStoryActId = startingAct.StoryActId,
                CurrentStoryAct = startingAct,
                Trust = 0,
                EndingType = null,
                LastUpdated = DateTime.UtcNow,
                SelectedCharacterId = characterSelection.CharacterId,
                SelectedCharacterName = characterSelection.CustomName
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

        // If progress has no character but user has a selection, update progress
        if (!progress.SelectedCharacterId.HasValue)
        {
            var characterSelection = await _context.UserCharacterSelection
                .FirstOrDefaultAsync(s => s.UserId == userId);
            
            if (characterSelection != null)
            {
                progress.SelectedCharacterId = characterSelection.CharacterId;
                progress.SelectedCharacterName = characterSelection.CustomName;
                await _context.SaveChangesAsync();
            }
            else
            {
                // No character selected yet, redirect to selection
                return RedirectToAction("Character", "Home");
            }
        }

        // If we already have an ending, go straight to the ending screen.
        if (!string.IsNullOrEmpty(progress.EndingType))
        {
            return RedirectToAction(nameof(Ending));
        }

        // Load player's selected character
        Characters? playerCharacter = null;
        if (progress.SelectedCharacterId.HasValue)
        {
            playerCharacter = await _context.Characters
                .FirstOrDefaultAsync(c => c.CharacterID == progress.SelectedCharacterId.Value);
        }

        var vm = StoryPlayViewModel.FromUserProgress(progress);
        vm.PlayerCharacter = playerCharacter;
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

        if (choice == null)
        {
            return await Play(error: "That choice is no longer available.");
        }

        // Only allow moving forward (no going backwards)
        int nextActId = choice.NextActId;
        if (nextActId < progress.CurrentStoryActId)
        {
            return await Play(error: "Invalid story progression.");
        }

        // Apply trust change from this choice (-5, 0, +5 etc.)
        progress.Trust += choice.TrustChange;

        // Check if we're coming from scene 57 - route to appropriate ending scene
        if (progress.CurrentStoryActId == 57 && nextActId == 62)
        {
            // Calculate ending based on trust (after applying trust change)
            var endingType = CalculateEnding(progress.Trust);
            int endingSceneId;
            
            if (endingType == "Bad")
                endingSceneId = 62;
            else if (endingType == "Good")
                endingSceneId = 63;
            else // True
                endingSceneId = 64;
            
            // Move to ending scene
            var endingScene = await _context.StoryActs
                .Include(a => a.Choices)
                .Include(a => a.Character)
                .FirstOrDefaultAsync(a => a.StoryActId == endingSceneId);
            
            if (endingScene != null)
            {
                progress.CurrentStoryActId = endingSceneId;
                progress.CurrentStoryAct = endingScene;
                progress.EndingType = endingType;
            }
            else
            {
                _logger.LogError("Ending scene {SceneId} not found", endingSceneId);
                return await Play(error: "Could not load ending scene.");
            }
        }
        // Check if we're at an ending scene (62, 63, 64) - redirect to ending view
        else if (progress.CurrentStoryActId >= 62 && progress.CurrentStoryActId <= 64)
        {
            // Player has completed the ending scene, show ending view
            if (string.IsNullOrEmpty(progress.EndingType))
            {
                progress.EndingType = CalculateEnding(progress.Trust);
            }
        }
        // Move to next act normally
        else if (nextActId != progress.CurrentStoryActId)
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
            
            // Set ending when reaching any scene in Act 3 (check Act category from Description)
            var currentAct = await _context.StoryActs
                .FirstOrDefaultAsync(a => a.StoryActId == progress.CurrentStoryActId);
            
            if (currentAct != null && currentAct.Description == "Act 3" && string.IsNullOrEmpty(progress.EndingType))
            {
                // Only set ending type if we're at an ending scene
                if (progress.CurrentStoryActId >= 62 && progress.CurrentStoryActId <= 64)
                {
                    progress.EndingType = CalculateEnding(progress.Trust);
                }
            }
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

        // If we're at an ending scene (62, 63, 64) and player made a choice, show ending view
        if (progress.CurrentStoryActId >= 62 && progress.CurrentStoryActId <= 64 && !string.IsNullOrEmpty(progress.EndingType))
        {
            // Check if the choice points back to the same ending scene (self-loop means ending is complete)
            if (choice.NextActId == progress.CurrentStoryActId || choice.NextActId >= 62)
            {
                return RedirectToAction(nameof(Ending));
            }
        }

        return RedirectToAction(nameof(Play));
    }

    // Guard: prevent accidental GET navigation to /Story/Choose
    [HttpGet]
    public IActionResult Choose()
    {
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

        var progress = await _context.UserProgress
            .Include(p => p.CurrentStoryAct)
            .FirstOrDefaultAsync(p => p.UserID == userId);
        
        if (progress == null || string.IsNullOrEmpty(progress.EndingType))
        {
            return RedirectToAction(nameof(Play));
        }

        // Load the full ending scene content (62, 63, or 64)
        var endingScene = await _context.StoryActs
            .FirstOrDefaultAsync(a => a.StoryActId == progress.CurrentStoryActId);

        ViewData["EndingType"] = progress.EndingType;
        ViewData["Trust"] = progress.Trust;
        ViewData["EndingContent"] = endingScene?.Content ?? "The end.";
        ViewData["EndingTitle"] = endingScene?.Title ?? "Ending";
        return View();
    }

    // Jump to the first scene of the user's current act category (Act 1/2/3)
    [Authorize]
    public async Task<IActionResult> StartOfAct()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var progress = await _context.UserProgress
            .Include(p => p.CurrentStoryAct)
            .FirstOrDefaultAsync(p => p.UserID == userId);

        if (progress == null)
        {
            // No progress yet; just start normally
            return RedirectToAction(nameof(Play));
        }

        // Ensure current act loaded
        if (progress.CurrentStoryAct == null)
        {
            progress.CurrentStoryAct = await _context.StoryActs
                .FirstOrDefaultAsync(a => a.StoryActId == progress.CurrentStoryActId);
        }

        // Determine the act category using the Description (e.g., "Act 2")
        var actDescription = progress.CurrentStoryAct?.Description;
        if (string.IsNullOrWhiteSpace(actDescription))
        {
            return RedirectToAction(nameof(Play));
        }

        // Find the first scene (lowest StoryActId) within the same act description
        var firstSceneInAct = await _context.StoryActs
            .Where(a => a.Description == actDescription)
            .OrderBy(a => a.StoryActId)
            .FirstOrDefaultAsync();

        if (firstSceneInAct != null && progress.CurrentStoryActId != firstSceneInAct.StoryActId)
        {
            progress.CurrentStoryActId = firstSceneInAct.StoryActId;
            progress.CurrentStoryAct = null; // will be reloaded by Play
            progress.LastUpdated = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Play));
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
            // Also reset character selection so user re-selects OC
            progress.SelectedCharacterId = null;
            progress.SelectedCharacterName = string.Empty;
            await _context.SaveChangesAsync();
        }

        // Delete the UserCharacterSelection so user can pick a new character
        var characterSelection = await _context.UserCharacterSelection
            .FirstOrDefaultAsync(s => s.UserId == userId);
        if (characterSelection != null)
        {
            _context.UserCharacterSelection.Remove(characterSelection);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Character", "Home");
    }
}

