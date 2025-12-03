namespace FirstMVC.StoryContent.Act1;

public static class Act1_01_MorningWakeUp
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
                ImageUrl = (string?)"/images/classroom.png",
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
                ImageUrl = (string?)"/images/classroom.png",
                Content = "Fine, but don't blame me when you're late!",
                Choices = new[] {

                    new { Text = "I'm never late!",
                     NextSceneId = 5,
                    TrustChange = 0,
                    IsCorrect = false, ResponseDialog = "You are always late honey" }

                    
                }
            },
            
            // Scene 3 - Response to "Okay, I'm getting up!"
            new { 
                SceneId = 3,
                ActCategory = 1,
                Title = "Parent's Response", 
                CharacterCode = "ID_PARENT",
                ImageUrl = (string?)"/images/classroom.png",
                Content = "That's my child! Breakfast is ready in 5.",
                Choices = new[] {
                    new { Text = "*GASP*  good morning mom and dad!",
                    NextSceneId = 5,
                    TrustChange = 0,
                    IsCorrect = false,
                    ResponseDialog = "Well hello little one, I hope you slept well!" }
                }
            },
            
            // Scene 4 - Response to "I'm already awake!"
            new { 
                SceneId = 4,
                ActCategory = 1,
                Title = "Parent's Response", 
                CharacterCode = "ID_PARENT",
                ImageUrl = (string?)"/images/classroom.png",
                Content = "Oh really? Then why are you still in bed? Get moving!",
                Choices = new[] {
                    new { Text = "I just wanted a few more minutes to relax.",
                    NextSceneId = 5, 
                    TrustChange = 0, 
                    IsCorrect = false, 
                    ResponseDialog = "GET GOING YOUNG ONE, TIME IS TICKING!" }
                }
            },

            new { 
                SceneId = 5,
                ActCategory = 1,
                Title = "Breakfast", 
                CharacterCode = "null",
                ImageUrl = (string?)"/images/classroom.png",
                Content = "You head down to the kitchen and see your parents sitting at the table eating breakfast. They look up and smile as you enter.",
                Choices = new[] {
                    new { 
                        Text = "Wow, this looks amazing! I better eat quickly so I don't miss the bus.",
                        NextSceneId = 6, 
                        TrustChange = 0, 
                        IsCorrect = true, 
                        ResponseDialog = "Your backpack is by the door, don't forget it when you leave! Have a great day at school!"
                    }
                }
            },
        };
    }
}

