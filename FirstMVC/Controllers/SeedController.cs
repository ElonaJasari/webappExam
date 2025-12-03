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
            var charactersToAdd = new[]
            {
                new Characters
                {
                    Name = "Friend 1",
                    Role = "Female Friend",
                    CharacterCode = "ID_FRIEND1",
                    Description = "Your first friend in the story",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                },
                new Characters
                {
                    Name = "Friend 2",
                    Role = "Male Friend",
                    CharacterCode = "ID_FRIEND2",
                    Description = "Your second friend in the story",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                },
                new Characters
                {
                    Name = "Parent",
                    Role = "Parent",
                    CharacterCode = "ID_PARENT",
                    Description = "The parent character",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                },
                new Characters
                {
                    Name = "Principal",
                    Role = "Principal",
                    CharacterCode = "ID_PRINCIPAL",
                    Description = "The school principal",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = ""
                },
                new Characters
                {
                    Name = "Teach",
                    Role = "Teacher",
                    CharacterCode = "ID_TEACHER",
                    Description = "The school teacher",
                    Dialog = "",
                    ImageUrl = "",
                    Translate = "" 
                }
            };

            _context.Characters.AddRange(charactersToAdd);
            await _context.SaveChangesAsync();
            
            return Content($"Added 5 characters: Friend 1, Friend 2, Parent, Principal, Teach. Go to /Character to manage them.");
        }


        // Navigate to /Seed/Story to create all story acts and choices
        // Story is organized in files: StoryContent/Act1/, Act2/, Act3/
        // Each file (like MorningWakeUp.cs) contains a scene sequence
        public async Task<IActionResult> Story()
        {
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

                // Get the character
                var characterCode = (string)story.CharacterCode;
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.CharacterCode == characterCode);
                if (character == null)
                {
                    errors.Add($"Scene {story.SceneId} (Act {story.ActCategory}): Character '{characterCode}' not found. Skipping.");
                    continue;
                }

                // Create the story act (SceneId becomes StoryActId in database)
                var act = new StoryAct
                {
                    StoryActId = story.SceneId,  // SceneId is the unique scene number
                    Title = story.Title,
                    Content = story.Content,
                    CharacterId = character.CharacterID,
                    Description = $"Act {story.ActCategory}"  // Store which Act category this belongs to
                };

                _context.StoryActs.Add(act);
                await _context.SaveChangesAsync(); // Save to get the StoryActId

                var choiceCount = 0;
                // Create choices if provided
                if (story.Choices != null && story.Choices.Any())
                {
                    foreach (var choiceData in story.Choices)
                    {
                        var choice = new Choice
                        {
                            Text = choiceData.Text,
                            NextActId = choiceData.NextSceneId,  // NextSceneId points to next scene
                            TrustChange = choiceData.TrustChange,
                            IsCorrect = choiceData.IsCorrect,
                            StoryActId = act.StoryActId
                        };
                        _context.Choices.Add(choice);
                        choiceCount++;
                    }
                    await _context.SaveChangesAsync();
                }

                created.Add($"Scene {story.SceneId} (Act {story.ActCategory}): '{story.Title}' ({character.Name}) - {choiceCount} choices");
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
