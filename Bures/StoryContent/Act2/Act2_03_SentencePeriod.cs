namespace Bures.StoryContent.Act2;

public static class Act2_03_SentencePeriod
{
    // New learning period: blackboard sentence session
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            new {
                SceneId = 30, // Start of period
                ActCategory = 2,
                Title = "Sentence Learning Period",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/teacher.png",
                Content = "Teacher: 'Today we will advance a bit more for our sentence learning.'",
                Choices = new[] {
                    new {
                        Text = "Continue",
                        NextSceneId = 31,
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "The teacher leads you to the blackboard."
                    }
                }
            },
            new {
                SceneId = 31,
                ActCategory = 2,
                Title = "Blackboard Sentence Session",
                CharacterCode = "Null",
                ImageUrl = (string?)"/images/blackEndingScreen.png",
                Content = "Sentences will appear on the blackboard. Try to read and repeat each one!",
                Choices = new[] {
                    new {
                        Text = "Continue",
                        NextSceneId = 39,
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "You finish the sentence session and get ready for the rest of the day."
                    }
                }
            }
        };
    }
}
