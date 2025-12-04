namespace Bures.StoryContent.Act3;

public static class Act3_01_FinalDayBegins
{
    // Act 3: Final Day - Morning scenes (44-48)
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 44 — Morning of Final Day (continues from Act 2 Scene 43)
            new {
                SceneId = 44,
                ActCategory = 3,
                Title = "Final Day Begins",
                CharacterCode = (string?)"ID_PARENT",
                ImageUrl = (string?)"/images/kitchen.png",
                Content =
                    "Good morning! Today is your final day of this week's lessons.\r\n\r\n" +
                    "Your parent: \"Mo don orrot?\" (How are you?)\r\n\r\n" +
                    "You feel a mix of excitement and nervousness about the day ahead.",
                Choices = new[] {
                    new {
                        Text = "Answer confidently: \"Mun lean buorre!\" (I'm good!)",
                        NextSceneId = 45,
                        TrustChange = +3,
                        IsCorrect = true,
                        ResponseDialog = "Hui buorre! (Very good!) You're doing great!"
                    },
                    new {
                        Text = "Answer: \"Mun lean... nervous\" (mixing languages)",
                        NextSceneId = 46,
                        TrustChange = +1,
                        IsCorrect = false,
                        ResponseDialog = "It's okay to be nervous. You've learned so much!"
                    },
                    new {
                        Text = "Just nod and smile",
                        NextSceneId = 47,
                        TrustChange = 0,
                        IsCorrect = false,
                        ResponseDialog = "Your parent gives you an encouraging hug."
                    }
                }
            },

            // Scene 45 — Confident answer branch
            new {
                SceneId = 45,
                ActCategory = 3,
                Title = "Confident Start",
                CharacterCode = (string?)"ID_PARENT",
                ImageUrl = (string?)"/images/kitchen.png",
                Content =
                    "Your parent beams with pride.\r\n\r\n" +
                    "\"Don hállat buoremusat! (You speak so well!) I'm so proud of your progress.\"",
                Choices = new[] {
                    new {
                        Text = "Say: \"Giitu! Mun háliidan oahppat buoremusat!\" (Thanks! I want to learn well!)",
                        NextSceneId = 48,
                        TrustChange = +4,
                        IsCorrect = true,
                        ResponseDialog = "That's the spirit! Have a great day!"
                    }
                }
            },

            // Scene 46 — Mixed language branch
            new {
                SceneId = 46,
                ActCategory = 3,
                Title = "Encouragement",
                CharacterCode = (string?)"ID_PARENT",
                ImageUrl = (string?)"/images/kitchen.png",
                Content =
                    "Your parent smiles understandingly.\r\n\r\n" +
                    "\"It's okay! Try: 'Mun lean...' (I am...). You're learning, and that's what matters.\"",
                Choices = new[] {
                    new {
                        Text = "Try: \"Mun lean buorre, muhto mun lean nervous\"",
                        NextSceneId = 48,
                        TrustChange = +3,
                        IsCorrect = true,
                        ResponseDialog = "Good! You're trying, and that's important!"
                    }
                }
            },

            // Scene 47 — Silent branch
            new {
                SceneId = 47,
                ActCategory = 3,
                Title = "Supportive Moment",
                CharacterCode = (string?)"ID_PARENT",
                ImageUrl = (string?)"/images/kitchen.png",
                Content =
                    "Your parent gives you a warm hug.\r\n\r\n" +
                    "\"Remember: 'Mun lean buorre' (I am good). You've come so far. Believe in yourself!\"",
                Choices = new[] {
                    new {
                        Text = "Hug back and say: \"Giitu, áhčči/eadni\"",
                        NextSceneId = 48,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "You're welcome, sweetheart. You've got this!"
                    }
                }
            },

            // Scene 48 — Converge: Heading to school for final day
            new {
                SceneId = 48,
                ActCategory = 3,
                Title = "Final Day at School",
                CharacterCode = (string?)null,
                ImageUrl = (string?)"/images/street.png",
                Content =
                    "You walk to school, thinking about everything you've learned.\r\n\r\n" +
                    "Áilu and Áila are waiting at the gate with big smiles.\r\n\r\n" +
                    "Áilu: \"Bures! Dál lea maŋimus beaivi! (Hello! Today is the last day!)\"\r\n\r\n" +
                    "Áila: \"Let's make it a great day!\"",
                Choices = new[] {
                    new {
                        Text = "Answer: \"Joo! Mun háliidan oahppat buoremusat!\" (Yes! I want to learn well!)",
                        NextSceneId = 49,
                        TrustChange = +4,
                        IsCorrect = true,
                        ResponseDialog = "Both: \"Perfect! Let's make it count!\""
                    },
                    new {
                        Text = "Say: \"Bures! Mun lean nervous\"",
                        NextSceneId = 50,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Áila: \"Ii leat váttis! (No worries!) We'll help each other!\""
                    },
                    new {
                        Text = "Just wave and smile",
                        NextSceneId = 51,
                        TrustChange = 0,
                        IsCorrect = false,
                        ResponseDialog = "Áilu: \"Come on, let's go! Today will be fun!\""
                    }
                }
            }
        };
    }
}