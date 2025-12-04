namespace Bures.StoryContent.Act2;

public static class Act2_03_FamilyWordsLesson
{
    // Act 2: Practice branches with both friends converging at lunch
    // NOTE: Scene 39 is the iPad vocabulary scene (sentences) - it routes to Scene 42
    // Scene 40 is the slower practice branch, Scene 41 is lunch, Scene 42 is end of day
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 39 — iPad Sentences (NEW)
            new {
                SceneId = 39,
                ActCategory = 2,
                Title = "iPad Sentences",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content = "On your iPad, you see sentences using the words you learned. Practice reading and saying them before lunch.",
                Choices = new[] {
                    new {
                        Text = "Continue to practice with friends",
                        NextSceneId = 40, // Goes to Patient Learning
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "Ready for more practice!"
                    }
                }
            },

            // Scene 40 — Slower practice branch (Scene 39 is iPad, so this stays at 40)
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
                        NextSceneId = 41,
                        TrustChange = +3,
                        IsCorrect = true,
                        ResponseDialog = "Both: \"Ipmelattá! (You're welcome!) Let's eat!\""
                    }
                }
            },

            // Scene 41 — Lunch with both friends
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

            // Scene 42 — Converge: End of day with both friends (iPad scene 39 routes here)
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