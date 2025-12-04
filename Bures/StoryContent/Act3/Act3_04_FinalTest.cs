namespace Bures.StoryContent.Act3;

public static class Act3_04_FinalTest
{
    // Act 3: Final test scenes (59-61) based on TaskDB
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 59 — Test introduction
            new {
                SceneId = 59,
                ActCategory = 3,
                Title = "Final Test Begins",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "The teacher hands you an iPad with the test questions.\r\n\r\n" +
                    "Teacher: \"This is your final test. Answer each question by selecting the correct translation. " +
                    "Take your time and do your best. Your performance will show how much you've learned this week.\"",
                Choices = new[] {
                    new {
                        Text = "Start the test",
                        NextSceneId = 60,
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "Teacher: \"Good luck!\""
                    }
                }
            },

            // Scene 60 — Test scene (will show test from TaskDB via LoadContextualVocabulary)
            new {
                SceneId = 60,
                ActCategory = 3,
                Title = "Taking the Test",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "You focus on the test questions, carefully reading each word and sentence in Northern Sámi.\r\n\r\n" +
                    "You select the correct translations, remembering everything you've learned this week.",
                Choices = new[] {
                    new {
                        Text = "Complete the test",
                        NextSceneId = 61,
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "Teacher: \"Well done! Let me check your answers.\""
                    }
                }
            },

            // Scene 61 — Test completion and results
            new {
                SceneId = 61,
                ActCategory = 3,
                Title = "Test Complete",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "You finish the test and hand the iPad back to the teacher.\r\n\r\n" +
                    "Teacher: \"Thank you! I'll review your answers. You've worked hard this week, and that's what matters most.\"",
                Choices = new[] {
                    new {
                        Text = "Wait for results",
                        NextSceneId = 62,
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "Teacher: \"Let's see how you did...\""
                    }
                }
            }
        };
    }
}
