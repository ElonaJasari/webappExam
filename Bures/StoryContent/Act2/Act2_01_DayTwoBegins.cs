namespace Bures.StoryContent.Act2;

public static class Act2_01_DayTwoBegins
{
    // Act 2: Day Two - Morning scenes (27-31, shifted forward by 1)
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 27 — Morning of Day Two (shifted from 26)
            new {
                SceneId = 27,
                ActCategory = 2,
                Title = "Day Two Begins",
                CharacterCode = "ID_PARENT",
                ImageUrl = (string?)"/images/kitchen.png",
                Content =
                    "Good morning! Ready for another day?\r\n\r\n" +
                    "Your parent asks: \"Maid don háliidat borrat?\" (What do you want to eat?)\r\n\r\n" +
                    "Breakfast options are on the table.",
                Choices = new[] {
                    new {
                        Text = "Answer in Sámi: \"Mun háliidan láibbi\" (I want bread)",
                        NextSceneId = 28,
                        TrustChange = +5,
                        IsCorrect = true,
                        ResponseDialog = "Hui buorre! (Very good!) Here you go!"
                    },
                    new {
                        Text = "Point at the food and say \"That one\"",
                        NextSceneId = 29,
                        TrustChange = 0,
                        IsCorrect = false,
                        ResponseDialog = "Try using Sámi words next time, dear."
                    },
                    new {
                        Text = "Say nothing and just take food",
                        NextSceneId = 30,
                        TrustChange = -2,
                        IsCorrect = false,
                        ResponseDialog = "Remember to use your words, even if it's hard."
                    }
                }
            },

            // Scene 28 — Good Sámi answer branch (shifted from 27)
            new {
                SceneId = 28,
                ActCategory = 2,
                Title = "Encouraging Response",
                CharacterCode = "ID_PARENT",
                ImageUrl = (string?)"/images/kitchen.png",
                Content =
                    "Your parent smiles warmly.\r\n\r\n" +
                    "\"Don hállat buoremusat! (You speak so well!) I'm proud of you.\"",
                Choices = new[] {
                    new {
                        Text = "Smile and say \"Giitu!\" (Thanks!)",
                        NextSceneId = 31,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "You're welcome, sweetheart!"
                    }
                }
            },

            // Scene 29 — Pointing branch (shifted from 28)
            new {
                SceneId = 29,
                ActCategory = 2,
                Title = "Gentle Reminder",
                CharacterCode = "ID_PARENT",
                ImageUrl = (string?)"/images/kitchen.png",
                Content =
                    "Your parent hands you the food but looks a bit disappointed.\r\n\r\n" +
                    "\"Remember: 'láibbi' (bread), 'vuostá' (cheese), 'báhpu' (coffee). Try using them!\"",
                Choices = new[] {
                    new {
                        Text = "Nod and try: \"Giitu, láibbi\"",
                        NextSceneId = 31,
                        TrustChange = +3,
                        IsCorrect = true,
                        ResponseDialog = "That's better! Keep practicing!"
                    }
                }
            },

            // Scene 30 — Silent branch (shifted from 29)
            new {
                SceneId = 30,
                ActCategory = 2,
                Title = "Learning Moment",
                CharacterCode = "ID_PARENT",
                ImageUrl = (string?)"/images/kitchen.png",
                Content =
                    "Your parent sits down with you.\r\n\r\n" +
                    "\"I know it's hard, but language learning takes practice. Even one word is progress.\"",
                Choices = new[] {
                    new {
                        Text = "Try: \"Giitu, áhčči/eadni\" (Thanks, dad/mom)",
                        NextSceneId = 31,
                        TrustChange = +4,
                        IsCorrect = true,
                        ResponseDialog = "Your parent's eyes light up with joy!"
                    }
                }
            },

            // Scene 31 — Converge: Heading to school (shifted from 30)
            new {
                SceneId = 31,
                ActCategory = 2,
                Title = "On the Way to School",
                CharacterCode = "",
                ImageUrl = (string?)"/images/street.png",
                Content =
                    "You head to school, feeling more confident.\r\n\r\n" +
                    "As you approach the school gate, you see Áilu waiting.\r\n\r\n" +
                    "Áilu: \"Bures! Mo don orrot?\" (Hello! How are you?)",
                Choices = new[] {
                    new {
                        Text = "Answer: \"Mun lean buorre, giitu!\" (I'm good, thanks!)",
                        NextSceneId = 32,
                        TrustChange = +4,
                        IsCorrect = true,
                        ResponseDialog = "Oho! Don hállat buoremusat! (Wow! You speak so well!)"
                    },
                    new {
                        Text = "Wave and say \"Bures!\"",
                        NextSceneId = 33,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Áilu grins and walks with you inside."
                    },
                    new {
                        Text = "Just nod and smile",
                        NextSceneId = 34,
                        TrustChange = 0,
                        IsCorrect = false,
                        ResponseDialog = "Áilu: \"Ii leat váttis. (No worries.) Let's practice more today!\""
                    }
                }
            }
        };
    }
}