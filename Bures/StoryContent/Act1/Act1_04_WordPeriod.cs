namespace Bures.StoryContent.Act1;

public static class Act1_04_WordPeriod
{
    // New learning period: blackboard word session
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            new {
                SceneId = 21,
                ActCategory = 1,
                Title = "Word Learning Period",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/teacher.png",
                Content = "Teacher: 'In this period we will learn some new words.'",
                Choices = new[] {
                    new {
                        Text = "Continue",
                        NextSceneId = 22,
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "The teacher leads you to the blackboard."
                    }
                }
            },
            new {
                SceneId = 22,
                ActCategory = 1,
                Title = "Blackboard Word Session",
                CharacterCode = "Null",
                ImageUrl = (string?)"/images/blackEndingScreen.png",
                Content = "Words will appear on the blackboard. Try to read and repeat each one!",
                Choices = new[] {
                    new {
                        Text = "Continue",
                        NextSceneId = 23,
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "You finish the word session and get ready for the rest of the day."
                    }
                }
            }
        };
    }
}
