namespace Bures.StoryContent.Act3;

public static class Act3_04_FinalTest
{
    // Act 3: Final test announcement and test sheet
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 61 — Teacher announces the test
            new {
                SceneId = 58,
                ActCategory = 3,
                Title = "Final Test Announcement",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/teacher.png",
                Content = "Teacher: 'Now it's time for your final test! You will be given a sheet with words and sentences. Translate them as best you can.'",
                Choices = new[] {
                    new {
                        Text = "Begin the test",
                        NextSceneId = 59,
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "You receive the test sheet."
                    }
                }
            },
            // Scene 59 — Test sheet (handled by controller/view)
            new {
                SceneId = 59,
                ActCategory = 3,
                Title = "Final Test Sheet",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/test_sheet.png",
                Content = "Translate the words and sentences on your test sheet. Your score will affect your trust value!",
                Choices = new[] {
                    new {
                        Text = "Submit answers",
                        NextSceneId = 60, // Convergence scene - StoryController will route to appropriate ending (61/62/63)
                        TrustChange = 0, // Trust change handled by controller after scoring
                        IsCorrect = true,
                        ResponseDialog = "Your test is graded."
                    }
                }
            },

            // Scene 60 — Convergence: Journey reflection (routes to ending scenes 60/61/62 based on trust)
            new {
                SceneId = 60,
                ActCategory = 3,
                Title = "Journey Continues",
                CharacterCode = (string?)null,
                ImageUrl = (string?)"/images/home.png",
                Content =
                    "You walk home, reflecting on everything you've learned.\r\n\r\n" +
                    "Northern Sámi is no longer a foreign language—it's becoming part of who you are.\r\n\r\n" +
                    "Your parent greets you at the door: \"Mo don orrot?\" (How are you?)\r\n\r\n" +
                    "You think about your week—the friends you've made, the words you've learned, and how far you've come...",
                Choices = new[] {
                    new {
                        Text = "Reflect on your journey",
                        NextSceneId = 60, // StoryController will route to 61/62/63 based on trust
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "You've completed your first week of learning!"
                    }
                }
            }
        };
    }
}
