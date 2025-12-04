namespace Bures.StoryContent.Act2
{
    public static class Act2_03_SentencePeriod
    {
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
                            Text = "Continue",
                            NextSceneId = 39,
                            TrustChange = 0,
                            IsCorrect = true,
                            ResponseDialog = "The teacher leads you to the blackboard."
                        }
                    }
                },
                new {
                    SceneId = 39,
                    ActCategory = 2,
                    Title = "Blackboard Sentence Session",
                    CharacterCode = "Null",
                    ImageUrl = (string?)"/images/blackEndingScreen.png",
                    Content = "Sentences will appear on the blackboard. Try to read and repeat each one!",
                    Choices = new[] {
                        new {
                            Text = "Continue",
                            NextSceneId = 40,
                            TrustChange = 0,
                            IsCorrect = true,
                            ResponseDialog = "You finish the sentence session and get ready for the rest of the day."
                        }
                    }
                }
            };
        }
    }
}

