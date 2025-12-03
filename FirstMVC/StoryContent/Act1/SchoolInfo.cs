namespace FirstMVC.StoryContent.Act1;

public static class SchoolIntro
{
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 5 - At School (continues from MorningWakeUp)
            new { 
                SceneId = 5,
                ActCategory = 1,
                Title = "At School", 
                CharacterCode = "ID_TEACHER", 
                Content = "Good morning class! Today we'll be learning about...",
                Choices = new[] {
                    new { 
                        Text = "I'm ready to learn!", 
                        NextSceneId = 6,
                        TrustChange = +5, 
                        IsCorrect = true,
                        ResponseDialog = "Great enthusiasm!"
                    },
                    new { 
                        Text = "This is boring...", 
                        NextSceneId = 7,
                        TrustChange = -5, 
                        IsCorrect = false,
                        ResponseDialog = "Try to stay focused, please."
                    },
                    new { 
                        Text = "Can we skip this?", 
                        NextSceneId = 8,
                        TrustChange = -10, 
                        IsCorrect = false,
                        ResponseDialog = "No, we need to cover this material."
                    }
                }
            },
            
            // Scene 6 - Response to "I'm ready to learn!"
            new { 
                SceneId = 6,
                ActCategory = 1,
                Title = "Teacher's Response", 
                CharacterCode = "ID_TEACHER", 
                Content = "Great enthusiasm! Let's begin...",
                Choices = new[] {
                    new { Text = "Continue...", NextSceneId = 9, TrustChange = 0, IsCorrect = false, ResponseDialog = "" }
                }
            },
            
            // Scene 7 - Response to "This is boring..."
            new { 
                SceneId = 7,
                ActCategory = 1,
                Title = "Teacher's Response", 
                CharacterCode = "ID_TEACHER", 
                Content = "Try to stay focused, please. This is important.",
                Choices = new[] {
                    new { Text = "Continue...", NextSceneId = 9, TrustChange = 0, IsCorrect = false, ResponseDialog = "" }
                }
            },
            
            // Scene 8 - Response to "Can we skip this?"
            new { 
                SceneId = 8,
                ActCategory = 1,
                Title = "Teacher's Response", 
                CharacterCode = "ID_TEACHER", 
                Content = "No, we need to cover this material. Please pay attention.",
                Choices = new[] {
                    new { Text = "Continue...", NextSceneId = 9, TrustChange = 0, IsCorrect = false, ResponseDialog = "" }
                }
            },
            
            // Scene 9 - Next scene (add more scenes here or create new file)
            // new { 
            //     SceneId = 9,
            //     ActCategory = 1,
            //     Title = "Next Scene", 
            //     CharacterCode = "ID_FRIEND1", 
            //     Content = "...",
            //     Choices = new[] {
            //         new { Text = "...", NextSceneId = 10, TrustChange = 0, IsCorrect = false, ResponseDialog = "" }
            //     }
            // },
        };
    }
}

