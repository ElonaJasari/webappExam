namespace FirstMVC.StoryContent.Act3;

public static class Act3_04_FinalTest
{
    // Scene 61 - Bridge to endings
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 61 — Final reflection before ending
            new {
                SceneId = 61,
                ActCategory = 3,
                Title = "Walking Home",
                CharacterCode = (string?)null,
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "The week is over. You pack your things and prepare to head home.\r\n\r\n" +
                    "You think about everything you've learned:\r\n" +
                    "- New words in Northern Sámi\r\n" +
                    "- Conversations with Áilu and Áila\r\n" +
                    "- Practice sessions with the teacher\r\n\r\n" +
                    "How do you feel about your progress?",
                Choices = new[] {
                    new {
                        Text = "I'm proud of what I've learned",
                        NextSceneId = 62,
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "Time to see how far you've come..."
                    }
                }
            }
        };
    }
}
