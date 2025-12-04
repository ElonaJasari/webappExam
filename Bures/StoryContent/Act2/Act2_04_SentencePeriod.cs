namespace Bures.StoryContent.Act2;

public static class Act2_04_SentencePeriod
{
    // Act 2: Sentence practice scene (shows sentences only, like Act1 scene 21 shows words)
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 43 — iPad Sentences Practice (similar to Act1 scene 21 for words)
            new {
                SceneId = 43,
                ActCategory = 2,
                Title = "iPad Sentences Practice",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Before you leave, the teacher gives out an iPad to each student.\r\n\r\n" +
                    "On the screen, you see sentences using the words you learned today.\r\n\r\n" +
                    "Take a moment to review them and practice reading each sentence out loud.",
                Choices = new[] {
                    new {
                        Text = "Review the sentences on the iPad",
                        NextSceneId = 44,
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "Buorre! (Good job reviewing!)"
                    }
                }
            },

            // Scene 44 — Continue after sentence review
            new {
                SceneId = 44,
                ActCategory = 2,
                Title = "End of Day Two",
                CharacterCode = "ID_PLAYER",
                ImageUrl = (string?)"/images/walking_home.png",
                Content =
                    "As you walk home, you reflect on today's lesson.\r\n\r\n" +
                    "You practiced sentences and had conversations with your friends.\r\n\r\n" +
                    "You feel more confident using Northern Sámi in complete sentences.",
                Choices = new[] {
                    new {
                        Text = "Think about the sentences you learned",
                        NextSceneId = 0,
                        TrustChange = +1,
                        IsCorrect = true,
                        ResponseDialog = "Keep practicing!"
                    }
                }
            }
        };
    }
}

