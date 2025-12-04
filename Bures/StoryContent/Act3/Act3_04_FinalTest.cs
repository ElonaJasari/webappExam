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
            // Scene 62 — Test sheet (handled by controller/view)
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
                        NextSceneId = 60, // Next scene after test
                        TrustChange = 0, // Trust change handled by controller after scoring
                        IsCorrect = true,
                        ResponseDialog = "Your test is graded."
                    }
                }
            }
        };
    }
}
