using System;
using System.Security.Claims;
using Bures.Data;
using Bures.Models;
using Bures.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bures.Controllers;

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
        
        // Load vocabulary contextually based on current act
        await LoadContextualVocabulary(vm, progress);
        
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
        // Clamp trust value between MIN_TRUST and MAX_TRUST
        if (progress.Trust < MIN_TRUST) progress.Trust = MIN_TRUST;
        if (progress.Trust > MAX_TRUST) progress.Trust = MAX_TRUST;

        // Check if we're coming from scene 60 (convergence scene) - route to appropriate ending scene
        if (progress.CurrentStoryActId == 60 && nextActId == 60)
        {
            // Calculate ending based on trust (after applying trust change)
            var endingType = CalculateEnding(progress.Trust);
            int endingSceneId;
            
            if (endingType == "Bad")
                endingSceneId = 61;
            else if (endingType == "Good")
                endingSceneId = 62;
            else // True
                endingSceneId = 63;
            
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
        // Check if we're at an ending scene (61, 62, 63) - redirect to ending view
        else if (progress.CurrentStoryActId >= 61 && progress.CurrentStoryActId <= 63)
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

    /// <summary>
    /// Loads vocabulary contextually based on the current story act.
    /// Act 1: Shows words/nouns at specific moments (customize StoryActIds below)
    /// Act 2: Shows sentences at specific moments (customize StoryActIds below)
    /// Act 3: Shows test before ending (customize StoryActIds below)
    /// 
    /// To customize when vocabulary appears, modify the StoryActId checks below.
    /// </summary>
    private async Task LoadContextualVocabulary(StoryPlayViewModel vm, UserProgressDB progress)
    {
        try
        {
            var currentAct = progress.CurrentStoryAct;
            if (currentAct == null) return;

            // Determine act category from Description (e.g., "Act 1", "Act 2", "Act 3")
            var actDescription = currentAct.Description ?? "";
            int actCategory = 0;
            if (actDescription.StartsWith("Act "))
            {
                int.TryParse(actDescription.Replace("Act ", "").Trim(), out actCategory);
            }

            // Act 1: Show ONLY WORDS/Nouns at specific scenes
            // Admins add words via admin panel with Type="word" or Type="noun"
            // CUSTOMIZE: Add the StoryActIds where you want vocabulary to appear in Act 1
            if (actCategory == 1)
            {
                // Add your specific StoryActIds here where vocabulary should appear
                var vocabularySceneIds = new[] { 6, 5 }; // Customize these IDs
                
                if (vocabularySceneIds.Contains(progress.CurrentStoryActId))
                {
                    // Filter: Only get words/nouns (case-insensitive matching)
                    // These are added by admins before game start via admin panel
                    var words = await _context.Tasks
                        .Where(t => t.Type.ToLower() == "word" || t.Type.ToLower() == "noun")
                        .OrderBy(t => t.Text)
                        .ToListAsync();
                    
                    if (words.Any())
                    {
                        vm.VocabularyWords = words;
                        vm.ShouldShowVocabulary = true;
                        vm.VocabularyMessage = "On your iPad, there are " + words.Count() + " words you need to learn. Take a moment to review them!";
                    }
                }
            }
            // Act 2: Show ONLY SENTENCES at specific scenes
            // Admins add sentences via admin panel with Type="sentence" or Type="sentences"
            // CUSTOMIZE: Add the StoryActIds where you want sentences to appear in Act 2
            else if (actCategory == 2)
            {
                // Add your specific StoryActIds here where sentences should appear
                var sentenceSceneIds = new[] { 42, 43 }; // Customize these IDs
                
                if (sentenceSceneIds.Contains(progress.CurrentStoryActId))
                {
                    // Filter: Only get sentences (case-insensitive matching)
                    // These are added by admins before game start via admin panel
                    var sentences = await _context.Tasks
                        .Where(t => t.Type.ToLower() == "sentence" || t.Type.ToLower() == "sentences")
                        .OrderBy(t => t.Text)
                        .ToListAsync();
                    
                    if (sentences.Any())
                    {
                        vm.VocabularySentences = sentences;
                        vm.ShouldShowVocabulary = true;
                        vm.VocabularyMessage = "On your iPad, there are " + sentences.Count() + " sentences using the words you learned. Practice them!";
                    }
                }
            }
            // Act 3: Show TEST with BOTH words and sentences
            // Test includes all tasks (both words and sentences) from TaskDB
            // CUSTOMIZE: Add the StoryActId where the test should appear (before ending)
            else if (actCategory == 3)
            {
                // Add your specific StoryActId where the test should appear
                // This should be the scene right before the ending (scene 60 in your case)
                var testSceneIds = new[] { 60, 59, 58 }; // Customize these IDs - typically scene 60 before ending
                
                if (testSceneIds.Contains(progress.CurrentStoryActId))
                {
                    // Check if user already took the test (prevent retaking)
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var hasTakenTest = await _context.UserTaskResults
                        .AnyAsync(r => r.UserId == userId && r.ActNumber == 3);
                    
                    if (!hasTakenTest)
                    {
                        // Get ALL tasks (both words AND sentences) for the test
                        // Admins add these via admin panel before game start
                        var allTasks = await _context.Tasks.ToListAsync();
                        var random = new Random();
                        // Randomly select up to 10 tasks, or all if less than 10
                        var testTasks = allTasks.OrderBy(x => random.Next()).Take(Math.Min(10, allTasks.Count)).ToList();
                        
                        if (testTasks.Any())
                        {
                            vm.TestTasks = testTasks;
                            vm.ShouldShowTest = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to load contextual vocabulary for StoryActId {StoryActId}", progress.CurrentStoryActId);
            // Continue without vocabulary - not critical
        }
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

    [AllowAnonymous]
    public async Task<IActionResult> Act1Words()
    {
        // Get all single words (Type == "Word") from Tasks
        var words = await _context.Tasks
            .Where(t => t.Type.ToLower() == "word")
            .ToListAsync();
        return View("Act1Words", words);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Act3Test()
    {
        // Randomly select words and sentences from Tasks
        var allTasks = await _context.Tasks.ToListAsync();
        var random = new Random();
        var selectedTasks = allTasks.OrderBy(x => random.Next()).Take(10).ToList(); // 10 random items
        return View("Act3Test", selectedTasks);
    }

    /// <summary>
    /// Handles test submission from within the story flow.
    /// Updates trust values based on performance:
    /// - Under 70%: -40 trust
    /// - 71-90%: +10 trust  
    /// - Over 90%: +20 trust
    /// </summary>
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitStoryTest(Dictionary<int, string> answers)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var progress = await _context.UserProgress.FirstOrDefaultAsync(p => p.UserID == userId);
        if (progress == null)
        {
            return RedirectToAction(nameof(Play), new { error = "Progress not found." });
        }

        // Check if user already took the test
        var hasTakenTest = await _context.UserTaskResults
            .AnyAsync(r => r.UserId == userId && r.ActNumber == 3);
        
        if (hasTakenTest)
        {
            return RedirectToAction(nameof(Play), new { error = "You have already completed the test." });
        }

        var allTasks = await _context.Tasks.ToListAsync();
        int correct = 0;
        int total = 0;

        foreach (var task in allTasks.Where(t => answers.ContainsKey(t.TaskId)))
        {
            total++;
            var userAnswer = answers[task.TaskId]?.Trim().ToLower();
            var correctText = task.Text.ToLower();
            var correctDescription = task.Description.ToLower();
            
            // Check if answer matches either the Sami text or Norwegian description
            if (userAnswer == correctText || userAnswer == correctDescription || 
                correctText.Contains(userAnswer) || correctDescription.Contains(userAnswer))
            {
                correct++;
            }

            // Record the result
            _context.UserTaskResults.Add(new UserTaskResult
            {
                UserId = userId,
                TaskId = task.TaskId,
                ActNumber = 3,
                IsCorrect = userAnswer == correctText || userAnswer == correctDescription ||
                           correctText.Contains(userAnswer) || correctDescription.Contains(userAnswer)
            });
        }

        double percent = total > 0 ? (double)correct / total * 100 : 0;
        int trustChange = 0;
        
        // Trust value changes based on performance
        if (percent < 70)
            trustChange = -40;
        else if (percent < 91)
            trustChange = 10;
        else
            trustChange = 20; // Changed from 50 to 20 as per user request

        // Apply trust change
        progress.Trust += trustChange;
        if (progress.Trust < MIN_TRUST) progress.Trust = MIN_TRUST;
        if (progress.Trust > MAX_TRUST) progress.Trust = MAX_TRUST;

        await _context.SaveChangesAsync();

        // Store test result in ViewBag for the result view
        TempData["TestScore"] = percent;
        TempData["TestCorrect"] = correct;
        TempData["TestTotal"] = total;
        TempData["TrustChange"] = trustChange;

        // Continue to next scene (ending)
        return RedirectToAction(nameof(Play));
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Act3TestSubmit(Dictionary<int, string> answers)
    {
        var allTasks = await _context.Tasks.ToListAsync();
        int correct = 0;
        foreach (var task in allTasks.Where(t => answers.ContainsKey(t.TaskId)))
        {
            // Check answer: either Text or Description matches
            var userAnswer = answers[task.TaskId]?.Trim().ToLower();
            var correctAnswer = (task.Text + "," + task.Description).ToLower();
            if (correctAnswer.Contains(userAnswer)) correct++;
        }
        int total = answers.Count;
        double percent = total > 0 ? (double)correct / total * 100 : 0;
        int trustChange = 0;
        if (percent < 70) trustChange = -40;
        else if (percent < 91) trustChange = 10;
        else trustChange = 20; // Updated to match user's requirement
        // Apply trust change to user progress
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId != null)
        {
            var progress = await _context.UserProgress.FirstOrDefaultAsync(p => p.UserID == userId);
            if (progress != null)
            {
                progress.Trust += trustChange;
                if (progress.Trust < MIN_TRUST) progress.Trust = MIN_TRUST;
                if (progress.Trust > MAX_TRUST) progress.Trust = MAX_TRUST;
                await _context.SaveChangesAsync();
            }
        }
        ViewBag.Score = percent;
        ViewBag.TrustChange = trustChange;
        return View("Act3TestResult");
    }
}

