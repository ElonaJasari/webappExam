namespace Bures.StoryContent.Act2;

public static class Act2_03_FamilyWordsLesson
{
    // Act 2: Practice branches with both friends converging at lunch (39-42)
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 39 — Confident practice branch
            new {
                SceneId = 39,
                ActCategory = 2,
                Title = "Great Progress",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "You practice confidently with both Áilu and Áila.\r\n\r\n" +
                    "Áilu: \"Hui buorre! (Very good!) You're learning so fast!\"\r\n\r\n" +
                    "Áila: \"Don hállat bures! (You speak well!) Keep it up!\"\r\n\r\n" +
                    "The bell rings for lunch.",
                Choices = new[] {
                    new {
                        Text = "Say: \"Giitu! Let's go to lunch!\"",
                        NextSceneId = 42,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Both friends: \"Jus don háliidat! (If you want!)\""
                    }
                }
            },

            // Scene 40 — Slower practice branch
            new {
                SceneId = 40,
                ActCategory = 2,
                Title = "Patient Learning",
                CharacterCode = "ID_FRIEND2",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Áila slows down and helps you practice each word carefully.\r\n\r\n" +
                    "Áila: \"Say: 'eahket'... good! Now 'áhčči'... perfect!\"\r\n\r\n" +
                    "Áilu: \"You're doing great! Practice makes perfect!\"\r\n\r\n" +
                    "The lunch bell rings.",
                Choices = new[] {
                    new {
                        Text = "Thank both friends: \"Giitu, Áilu ja Áila!\"",
                        NextSceneId = 42,
                        TrustChange = +3,
                        IsCorrect = true,
                        ResponseDialog = "Both: \"Ipmelattá! (You're welcome!) Let's eat!\""
                    }
                }
            },

            // Scene 41 — Lunch with both friends (new scene)
            new {
                SceneId = 41,
                ActCategory = 2,
                Title = "Lunch with Friends",
                CharacterCode = "ID_FRIEND2",
                ImageUrl = (string?)"/images/cafeteria.png",
                Content =
                    "You, Áilu, and Áila sit together at lunch.\r\n\r\n" +
                    "Áila: \"Maid don borrat?\" (What are you eating?)\r\n\r\n" +
                    "Áilu: \"And how do you say it in Sámi?\"",
                Choices = new[] {
                    new {
                        Text = "Answer: \"Mun borran láibbi ja vuostá\" (I'm eating bread and cheese)",
                        NextSceneId = 42,
                        TrustChange = +5,
                        IsCorrect = true,
                        ResponseDialog = "Both: \"Oho! Don hállat buoremusat! (Wow! You speak so well!)\""
                    },
                    new {
                        Text = "Try but mix some English",
                        NextSceneId = 42,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Áila: \"Good try! Keep practicing!\""
                    }
                }
            },

            // Scene 42 — Converge: End of day with both friends
            new {
                SceneId = 42,
                ActCategory = 2,
                Title = "End of Day Two",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "The school day ends. You, Áilu, and Áila walk together.\r\n\r\n" +
                    "Áilu: \"Don oahppat buoremusat! (You learn so well!) Want to practice more tomorrow?\"\r\n\r\n" +
                    "Áila: \"We can all practice together! It's more fun with friends!\"",
                Choices = new[] {
                    new {
                        Text = "Say: \"Jus don háliidat! Giitu!\" (If you want! Thanks!)",
                        NextSceneId = 43,
                        TrustChange = +4,
                        IsCorrect = true,
                        ResponseDialog = "Both: \"Oaidnaleapmi iđđes! (See you tomorrow!)\""
                    },
                    new {
                        Text = "Nod and wave goodbye",
                        NextSceneId = 43,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Both friends wave: \"Oaidnaleapmi!\""
                    }
                }
            }
        };
    }
}