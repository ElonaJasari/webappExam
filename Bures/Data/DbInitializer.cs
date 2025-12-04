using Bures.Models;

namespace Bures.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Ensure database is created
            context.Database.EnsureCreated();

            // Seed characters if needed
            if (!context.Characters.Any())
            {
                var characters = new Characters[]
                {
                    new Characters { Name = "Friend 1", Role = "ID_FRIEND1", Description = "Your first friend in the story", Dialog = "", ImageUrl = "", Translate = "" },
                    new Characters { Name = "Friend 2", Role = "ID_FRIEND2", Description = "Your second friend in the story", Dialog = "", ImageUrl = "", Translate = "" },
                    new Characters { Name = "Parent", Role = "ID_PARENT", Description = "The parent character", Dialog = "", ImageUrl = "", Translate = "" },
                    new Characters { Name = "Principal", Role = "ID_PRINCIPAL", Description = "The school principal", Dialog = "", ImageUrl = "", Translate = "" }
                };
                context.Characters.AddRange(characters);
                context.SaveChanges();
            }

            // Seed Act1_03_FirstLesson scenes if needed
            if (!context.StoryActs.Any(a => a.StoryActId == 100))
            {
                // Minimal seeding for scenes 21, 100 (iPad practice/vocab) and their choices
                var teacher = context.Characters.FirstOrDefault(c => c.Role == "ID_TEACHER");
                var scenes = new[] {
                    new StoryAct {
                        StoryActId = 21,
                        Title = "iPad Practice",
                        Content = "Before you leave, the teacher gives out an iPad to each student.\r\n\r\nOn the screen, you see today's new words and phrases.\r\n\r\nTake a moment to review them and practice writing each one.",
                        Description = "Act 1",
                        ImageUrl = "/images/classroom.png",
                        Character = teacher
                    },
                    new StoryAct {
                        StoryActId = 100,
                        Title = "iPad Vocabulary",
                        Content = "On your iPad, you see the words you need to learn. Review them before heading out.",
                        Description = "Act 1",
                        ImageUrl = "/images/classroom.png",
                        Character = teacher
                    }
                };
                context.StoryActs.AddRange(scenes);
                context.SaveChanges();

                // Add choices for scene 21 (to 100) and scene 100 (to 22)
                var choice21 = new Choice {
                    Text = "Review the words on the iPad",
                    NextActId = 100,
                    TrustChange = 0,
                    IsCorrect = true,
                    ResponseDialog = "Buorre! (Good job reviewing!)",
                    StoryActId = 21
                };
                var choice100 = new Choice {
                    Text = "Continue",
                    NextActId = 22,
                    TrustChange = 0,
                    IsCorrect = true,
                    ResponseDialog = "Ready to go!",
                    StoryActId = 100
                };
                context.Choices.AddRange(choice21, choice100);
                context.SaveChanges();
            }
        }
    }
}
