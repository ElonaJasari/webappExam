namespace FirstMVC.StoryContent.Act1;

public static class ActMorningWakeUp
{
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 1 - Opening Scene
            new { 
                SceneId = 1,
                ActCategory = 1,
                Title = "Morning Wake Up", 
                CharacterCode = "ID_PARENT", 
                Content = "*tug *tug* tug\r\n\r\n" +
                          "Wake up! School starts in 30 minutes!\r\n\r\n" +
                          "Seriously, if you miss the bus again, you're walking!\r\n\r\n" +
                          "Come on, get dressed and come down for breakfast.",
                Choices = new[] {
                    new { 
                        Text = "Just 5 more minutes...", 
                        NextSceneId = 2,
                        TrustChange = -5, 
                        IsCorrect = false,
                        ResponseDialog = "Fine, but don't blame me when you're late!"
                    },
                    new { 
                        Text = "Okay, I'm getting up!", 
                        NextSceneId = 3,
                        TrustChange = +5, 
                        IsCorrect = true,
                        ResponseDialog = "That's my child! Breakfast is ready in 5."
                    },
                    new { 
                        Text = "I'm already awake!", 
                        NextSceneId = 4,
                        TrustChange = 0, 
                        IsCorrect = false,
                        ResponseDialog = "Oh really? Then why are you still in bed? Get moving!"
                    }
                }
            },
            
            // Scene 2 - Response to "Just 5 more minutes..."
            new { 
                SceneId = 2,
                ActCategory = 1,
                Title = "Parent's Response", 
                CharacterCode = "ID_PARENT", 
                Content = "Fine, but don't blame me when you're late!",
                Choices = new[] {
                    new { Text = "Continue...", NextSceneId = 5, TrustChange = 0, IsCorrect = false, ResponseDialog = "" }
                }
            },
            
            // Scene 3 - Response to "Okay, I'm getting up!"
            new { 
                SceneId = 3,
                ActCategory = 1,
                Title = "Parent's Response", 
                CharacterCode = "ID_PARENT", 
                Content = "That's my child! Breakfast is ready in 5.",
                Choices = new[] {
                    new { Text = "Continue...", NextSceneId = 5, TrustChange = 0, IsCorrect = false, ResponseDialog = "" }
                }
            },
            
            // Scene 4 - Response to "I'm already awake!"
            new { 
                SceneId = 4,
                ActCategory = 1,
                Title = "Parent's Response", 
                CharacterCode = "ID_PARENT", 
                Content = "Oh really? Then why are you still in bed? Get moving!",
                Choices = new[] {
                    new { Text = "Continue...", NextSceneId = 5, TrustChange = 0, IsCorrect = false, ResponseDialog = "" }
                }
            },
        };
    }
}

