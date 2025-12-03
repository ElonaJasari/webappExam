namespace FirstMVC.StoryContent.Act1;

public static class Act1_04_AfterSchool
{
    // Continues after Act1_03 (last NextSceneId was 20)
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 21 — After school, walking home with Áilu
            new {
                SceneId = 21,
                ActCategory = 1,
                Title = "Walking Home",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/street.png",
                Content =
                    "You and Áilu walk together after school.\r\n\r\n" +
                    "Áilu: \"Mii leat dál?\" (What now?)\r\n\r\n" +
                    "The sun is still bright, and you have some free time.",
                Choices = new[] {
                    new {
                        Text = "Suggest going to the library to study",
                        NextSceneId = 22,
                        TrustChange = +3,
                        IsCorrect = true,
                        ResponseDialog = "Buorre idea! (Good idea!)"
                    },
                    new {
                        Text = "Ask if they want to hang out at the park",
                        NextSceneId = 23,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Jus don háliidat. (If you want.)"
                    },
                    new {
                        Text = "Say you need to go home",
                        NextSceneId = 24,
                        TrustChange = 0,
                        IsCorrect = false,
                        ResponseDialog = "Oaidnaleapmi iđđes! (See you tomorrow!)"
                    }
                }
            },

            // Scene 22 — Library branch
            new {
                SceneId = 22,
                ActCategory = 1,
                Title = "At the Library",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "You both head to the library. Áilu finds a quiet corner.\r\n\r\n" +
                    "Áilu: \"Geahččal sániid: 'girji' (book), 'skuvla' (school), 'oahppat' (to learn).\"\r\n\r\n" +
                    "(Try these words: 'girji' (book), 'skuvla' (school), 'oahppat' (to learn).)",
                Choices = new[] {
                    new {
                        Text = "Repeat the words carefully",
                        NextSceneId = 25,
                        TrustChange = +4,
                        IsCorrect = true,
                        ResponseDialog = "Hui buorre! (Very good!)"
                    },
                    new {
                        Text = "Ask what they mean in English",
                        NextSceneId = 25,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Áilu explains each word patiently."
                    }
                }
            },

            // Scene 23 — Park branch
            new {
                SceneId = 23,
                ActCategory = 1,
                Title = "At the Park",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/street.png",
                Content =
                    "You sit on a bench in the park. Áilu points at things around you.\r\n\r\n" +
                    "Áilu: \"Geahččal: 'muorra' (tree), 'beaivi' (sun), 'biegga' (wind).\"\r\n\r\n" +
                    "(Look: 'muorra' (tree), 'beaivi' (sun), 'biegga' (wind).)",
                Choices = new[] {
                    new {
                        Text = "Point and repeat: 'muorra', 'beaivi', 'biegga'",
                        NextSceneId = 25,
                        TrustChange = +4,
                        IsCorrect = true,
                        ResponseDialog = "Don oahppat buoremusat! (You learn well!)"
                    },
                    new {
                        Text = "Just nod and listen",
                        NextSceneId = 25,
                        TrustChange = +1,
                        IsCorrect = false,
                        ResponseDialog = "Áilu smiles and continues teaching."
                    }
                }
            },

            // Scene 24 — Going home branch
            new {
                SceneId = 24,
                ActCategory = 1,
                Title = "Parting Ways",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/street.png",
                Content =
                    "Áilu waves goodbye.\r\n\r\n" +
                    "Áilu: \"Oaidnaleapmi iđđes! Geahččal sániid goas fas. (See you tomorrow! Try the words again later.)\"",
                Choices = new[] {
                    new {
                        Text = "Wave back: 'Oaidnaleapmi!'",
                        NextSceneId = 25,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Áilu grins and walks away."
                    }
                }
            },

            // Scene 25 — Converge: End of Act 1, transition to Act 2
            new {
                SceneId = 25,
                ActCategory = 1,
                Title = "End of Day One",
                CharacterCode = "",
                ImageUrl = (string?)"/images/bedroom.png",
                Content =
                    "You arrive home, feeling a bit more confident with Northern Sámi.\r\n\r\n" +
                    "Your parent greets you: \"Maid don oahppat?\" (What did you learn?)\r\n\r\n" +
                    "You think about the day—new friends, new words, and a new beginning.\r\n\r\n" +
                    "Tomorrow brings another day of learning...",
                Choices = new[] {
                    new {
                        Text = "Rest and prepare for tomorrow",
                        NextSceneId = 26, // Start of Act 2
                        TrustChange = +1,
                        IsCorrect = true,
                        ResponseDialog = "You sleep well, ready for the next day."
                    }
                }
            }
        };
    }
}