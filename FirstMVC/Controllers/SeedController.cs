using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstMVC.Data;
using FirstMVC.Models;
using FirstMVC.StoryContent.Act1;
using FirstMVC.StoryContent.Act2;
using FirstMVC.StoryContent.Act3;

namespace FirstMVC.Controllers
{
    public class SeedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Navigate to /Seed/Characters to run this
        public async Task<IActionResult> Characters()
        {
            // Clear existing characters and related data to avoid duplicates
            if (await _context.Characters.AnyAsync())
            {
                // Temporarily disable FK enforcement for cleanup (SQLite-specific)
                await _context.Database.ExecuteSqlRawAsync("PRAGMA foreign_keys = OFF;");

                // First delete foreign key references
                var userCharacterSelections = await _context.UserCharacterSelection.ToListAsync();
                _context.UserCharacterSelection.RemoveRange(userCharacterSelections);
                
                var userProgress = await _context.UserProgress.ToListAsync();
                foreach (var progress in userProgress)
                {
                    progress.SelectedCharacterId = null;
                    // Keep NOT NULL constraint happy by using empty string
                    progress.SelectedCharacterName = string.Empty;
                }
                
                var storyActs = await _context.StoryActs.ToListAsync();
                foreach (var act in storyActs)
                {
                    act.CharacterId = null;
                }
                
                await _context.SaveChangesAsync();
                
                // Now safe to delete characters
                _context.Characters.RemoveRange(_context.Characters);
                await _context.SaveChangesAsync();

                // Re-enable FK enforcement
                await _context.Database.ExecuteSqlRawAsync("PRAGMA foreign_keys = ON;");
            }
          
            var charactersToAdd = new[]
            {
                // ===== Selectable Player Characters (for /Home/Character) =====
                new Characters
                {
                    Name = "Eiven Nordflamme",
                    Role = "Playable Character",
                    CharacterCode = "ID_COOL_DUDE",
                    Description = "Calm, grounded, quiet clumsy",
                    ImageUrl = "/images/cool_dude.png",
                },
                new Characters
                {
                    Name = "Vargár Ravdna",
                    Role = "Playable Character",
                    CharacterCode = "ID_CONFIDENT_DUDE",
                    Description = "HIMOTHY",
                    ImageUrl = "/images/confident_dude.png",
                },
                new Characters
                {
                    Name = "TUNG TUNG SAMUR",
                    Role = "Playable Character",
                    CharacterCode = "ID_TUNG_TUNG",
                    Description = "TUNG TUNG THE GREAT",
                    ImageUrl = "/images/tung_tung.png",
                },
                new Characters
                {
                    Name = "Aurora Borealis",
                    Role = "Playable Character",
                    CharacterCode = "ID_AURORA",
                    Description = "caring, fearless, strong",
                    ImageUrl = "/images/awsome_girl.png",
                },
                new Characters
                {
                    Name = "Chloe Kelly",
                    Role = "Playable Character",
                    CharacterCode = "ID_CHLOEKELLY",
                    Description = "Pressure? What pressure?",
                    ImageUrl = "/images/chloekelly.png",
                },
                
                // ===== Story NPCs (appear in scenes) =====
                new Characters
                {
                    Name = "Mother",
                    Role = "Parent NPC",
                    CharacterCode = "ID_PARENT",
                    Description = "The parent character",
                    ImageUrl = "/images/mother.png",
                },
                new Characters
                {
                    Name = "Teach",
                    Role = "Teacher NPC",
                    CharacterCode = "ID_TEACHER",
                    Description = "The school teacher",
                    ImageUrl = "/images/teacher.png",
                }
                ,
                new Characters
                {
                    Name = "Áilu",
                    Role = "Friend NPC",
                    CharacterCode = "ID_FRIEND1",
                    Description = "Helpful classmate who encourages the player",
                    ImageUrl = "/images/friend1.png",
                },
                new Characters
                {
                    Name = "Áila",
                    Role = "Friend NPC",
                    CharacterCode = "ID_FRIEND2",
                    Description = "Helpful classmate who encourages the player",
                    ImageUrl = "/images/Friend_2.png",
                }
            };

            _context.Characters.AddRange(charactersToAdd);
            await _context.SaveChangesAsync();
            
            return Content($"Seeded characters: 5 playable (Eiven, Vargár, TUNG TUNG, Aurora, Chloe) + 4 NPCs (Mother, Teach, Áila and Áilu). Go to /Character to manage them.");
        }

        // Diagnostic: Check what's in the database
        public async Task<IActionResult> CheckParent()
        {
            var parent = await _context.Characters.FirstOrDefaultAsync(c => c.CharacterCode == "ID_PARENT");
            if (parent == null)
                return Content("Parent character NOT FOUND in database. Run /Seed/Characters first.");
            
            return Content($"Parent character found:\nCharacterID: {parent.CharacterID}\nName: {parent.Name}\nCharacterCode: {parent.CharacterCode}\nImageUrl: '{parent.ImageUrl}'\n\nNow checking Scene 1...\n\n" +
                          await CheckScene1());
        }

        private async Task<string> CheckScene1()
        {
            var scene1 = await _context.StoryActs
                .Include(a => a.Character)
                .FirstOrDefaultAsync(a => a.StoryActId == 1);
            
            if (scene1 == null)
                return "Scene 1 NOT FOUND. Run /Seed/Story?reset=true";
            
            return $"Scene 1 found:\nTitle: {scene1.Title}\nCharacterId FK: {scene1.CharacterId}\nCharacter loaded: {(scene1.Character != null ? "YES" : "NO")}\n" +
                   (scene1.Character != null ? $"Character Name: {scene1.Character.Name}\nCharacter ImageUrl: '{scene1.Character.ImageUrl}'" : "");
        }


        // Navigate to /Seed/Story to create all story acts and choices
        // Story is organized in files: StoryContent/Act1/, Act2/, Act3/
        // Each file (like MorningWakeUp.cs) contains a scene sequence
        public async Task<IActionResult> Story(bool reset = false)
        {
            // Clear existing data if reset is requested
            if (reset)
            {
                _context.Choices.RemoveRange(_context.Choices);
                _context.StoryActs.RemoveRange(_context.StoryActs);
                _context.UserProgress.RemoveRange(_context.UserProgress);
                await _context.SaveChangesAsync();
            }

            // Load scenes from all story content files
            var storyData = GetAllScenesInOrder();
                
            var created = new List<string>();
            var errors = new List<string>();

            foreach (var story in storyData)
            {
                // Check if scene already exists
                var sceneId = (int)story.SceneId;
                var existing = await _context.StoryActs.FirstOrDefaultAsync(a => a.StoryActId == sceneId);
                if (existing != null)
                {
                    errors.Add($"Scene {story.SceneId} (Act {story.ActCategory}) already exists. Skipping.");
                    continue;
                }

                // Get the character (allow scenes with no character sprite)
                var characterCode = (string)story.CharacterCode;
                Characters? character = null;
                if (!string.IsNullOrWhiteSpace(characterCode))
                {
                    character = await _context.Characters.FirstOrDefaultAsync(c => c.CharacterCode == characterCode);
                    if (character == null)
                    {
                        errors.Add($"Scene {story.SceneId} (Act {story.ActCategory}): Character '{characterCode}' not found. Skipping.");
                        continue;
                    }
                }

                // Create the story act (SceneId becomes StoryActId in database)
                var act = new StoryAct
                {
                    StoryActId = story.SceneId,  // SceneId is the unique scene number
                    Title = story.Title,
                    Content = story.Content,
                    CharacterId = character?.CharacterID,
                    Description = $"Act {story.ActCategory}",  // Store which Act category this belongs to
                    ImageUrl = story.ImageUrl ?? null  // Include image if provided
                };

                _context.StoryActs.Add(act);
                await _context.SaveChangesAsync(); // Save to get the StoryActId

                var choiceCount = 0;
                // Create choices if provided
                dynamic? choices = story.Choices;
                if (choices != null && choices.Length > 0)
                {
                    foreach (var choiceData in choices)
                    {
                        var choice = new Choice
                        {
                            Text = choiceData.Text ?? string.Empty,
                            NextActId = choiceData.NextSceneId ?? 0,  // NextSceneId points to next scene
                            TrustChange = choiceData.TrustChange ?? 0,
                            IsCorrect = choiceData.IsCorrect ?? false,
                            StoryActId = act.StoryActId
                        };
                        _context.Choices.Add(choice);
                        choiceCount++;
                    }
                    await _context.SaveChangesAsync();
                }

                var characterName = character?.Name ?? "No Character";
                created.Add($"Scene {story.SceneId} (Act {story.ActCategory}): '{story.Title}' ({characterName}) - {choiceCount} choices");
            }

            await _context.SaveChangesAsync();

            var result = "Story seeding complete!\n\n";
            if (created.Any())
            {
                result += "Created:\n" + string.Join("\n", created) + "\n\n";
            }
            if (errors.Any())
            {
                result += "Errors:\n" + string.Join("\n", errors) + "\n\n";
            }
            result += "Go to /Story/Play to see it!";

            return Content(result);
        }

        // Helper method to load all scenes in the correct order
        // Uses reflection to find all classes with GetScenes() method in StoryContent namespace
        private static IEnumerable<dynamic> GetAllScenesInOrder()
        {
            var allScenes = new List<dynamic>();
            
            // Get all types from the StoryContent namespaces
            var assembly = typeof(Act1_01_MorningWakeUp).Assembly;
            var sceneProviderTypes = assembly.GetTypes()
                .Where(t => t.Namespace != null && 
                           (t.Namespace.StartsWith("FirstMVC.StoryContent.Act1") ||
                            t.Namespace.StartsWith("FirstMVC.StoryContent.Act2") ||
                            t.Namespace.StartsWith("FirstMVC.StoryContent.Act3")))
                .Where(t => t.GetMethod("GetScenes", 
                    System.Reflection.BindingFlags.Public | 
                    System.Reflection.BindingFlags.Static) != null)
                .OrderBy(t => t.Namespace) // Orders by Act
                .ThenBy(t => t.Name)       // Then alphabetically within each act
                .ToList();

            // Call GetScenes() on each discovered class
            foreach (var type in sceneProviderTypes)
            {
                var method = type.GetMethod("GetScenes");
                if (method != null)
                {
                    var scenes = method.Invoke(null, null) as IEnumerable<dynamic>;
                    if (scenes != null)
                    {
                        allScenes.AddRange(scenes);
                    }
                }
            }

            return allScenes;
        }

    }
}
