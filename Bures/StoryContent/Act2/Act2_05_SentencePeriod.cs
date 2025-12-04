namespace Bures.StoryContent.Act2;

public static class Act2_05_SentencePeriod
{
    // Sentence learning period - alternative path accessed from scene 34
    // Scene 38: Sentence Learning Period → Scene 39
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            new {
                SceneId = 38,
                ActCategory = 2,
                Title = "Sentence Learning Period",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/teacher.png",
                Content = 
                    "The teacher calls the class to attention.\r\n\r\n" +
                    "Teacher: 'Today we will advance a bit more for our sentence learning. Let's practice sentences on the blackboard.'\r\n\r\n" +
                    "You see sentences appear on the blackboard. The teacher points to each one.\r\n\r\n" +
                    "You practice reading and repeating the sentences in Northern Sámi.",
                Choices = new[] {
                    new {
                        Text = "Continue to practice sentences",
                        NextSceneId = 39,
                        TrustChange = +3,
                        IsCorrect = true,
                        ResponseDialog = "You finish the sentence session and get ready for the rest of the day."
                    }
                }
            }
        };
    }
}

