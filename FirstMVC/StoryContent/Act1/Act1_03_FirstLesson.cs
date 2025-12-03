namespace FirstMVC.StoryContent.Act1;

public static class Act1_03_FirstLesson
{
    // Continues after Act1_02 (last NextSceneId was 11)
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // 11 — Teacher asks you to introduce yourself in Sámi
            new {
                SceneId = 11,
                ActCategory = 1,
                Title = "First Lesson: Introductions",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Alright, let's begin with simple introductions.\r\n\r\n" +
                    "In Northern Sámi we say: \"Mun namma lea ...\" (My name is ...).\r\n\r\n" +
                    "Could you introduce yourself to the class?",
                Choices = new[] {
                    new {
                        Text = "Say: \"Mun namma lea ...\" confidently",
                        NextSceneId = 12,
                        TrustChange = +5,
                        IsCorrect = true,
                        ResponseDialog = "Nugo, hui buorre! (Exactly, very good!)"
                    },
                    new {
                        Text = "Answer in English: \"My name is ...\"",
                        NextSceneId = 13,
                        TrustChange = -2,
                        IsCorrect = false,
                        ResponseDialog = "Buorre álgu, muhto geahččal Sámegillii. (Good start, but try in Sámi.)"
                    },
                    new {
                        Text = "Freeze up and say nothing",
                        NextSceneId = 14,
                        TrustChange = -5,
                        IsCorrect = false,
                        ResponseDialog = "Ii leat váttis. Bassi beaivi — let’s try together. (No problem. Let’s try together.)"
                    }
                }
            },

            // 12 — Perfect answer branch
            new {
                SceneId = 12,
                ActCategory = 1,
                Title = "Confident Start",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Fábelaš álgu! Hold that confidence.\r\n\r\n" +
                    "Class, repeat after me: \"Mun namma lea ...\".",
                Choices = new[] {
                    new {
                        Text = "Smile and nod",
                        NextSceneId = 15,
                        TrustChange = +1,
                        IsCorrect = true,
                        ResponseDialog = "Iežat leat buorre ovdánan! (You’re progressing well!)"
                    }
                }
            },

            // 13 — English answer branch
            new {
                SceneId = 13,
                ActCategory = 1,
                Title = "Try In Sámi",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Geahččal fas: \"Mun namma lea ...\". I'll help you with pronunciation.",
                Choices = new[] {
                    new {
                        Text = "Repeat carefully in Sámi",
                        NextSceneId = 15,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Dakka! (There you go!)"
                    }
                }
            },

            // 14 — Froze up branch
            new {
                SceneId = 14,
                ActCategory = 1,
                Title = "We'll Do It Together",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Veahkkin: \"Mun... namma... lea...\". Little by little.\r\n\r\n" +
                    "Everyone starts somewhere.",
                Choices = new[] {
                    new {
                        Text = "Whisper it with the class",
                        NextSceneId = 15,
                        TrustChange = +1,
                        IsCorrect = true,
                        ResponseDialog = "Buorre! (Good!)"
                    }
                }
            },

            // 15 — Converge: Pair practice with Áilu
            new {
                SceneId = 15,
                ActCategory = 1,
                Title = "Pair Practice",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Áilu turns to you.\r\n\r\n" +
                    "Áilu: \"Gos don orrot?\" (Where do you live?)",
                Choices = new[] {
                    new {
                        Text = "Answer in Sámi: \"Mun orron ...\"",
                        NextSceneId = 16,
                        TrustChange = +4,
                        IsCorrect = true,
                        ResponseDialog = "Iežat! (Nice!)"
                    },
                    new {
                        Text = "Make a joke instead of answering",
                        NextSceneId = 16,
                        TrustChange = 0,
                        IsCorrect = false,
                        ResponseDialog = "Hehe… muhto geahččalit ge sániid. (Heh… but try the words too.)"
                    },
                    new {
                        Text = "Admit you don’t know how to say it",
                        NextSceneId = 16,
                        TrustChange = +1,
                        IsCorrect = true,
                        ResponseDialog = "Mun vealahin du. (I’ll help you.)"
                    }
                }
            },

            // 16 — Converge: Wrap-up and homework
            new {
                SceneId = 16,
                ActCategory = 1,
                Title = "Wrap-Up",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Great work today!\r\n\r\n" +
                    "Homework: practice \"Mun namma lea ...\" and \"Gos don orrot?\".\r\n\r\n" +
                    "Tomorrow we'll learn greetings and small talk.",
                Choices = new[] {
                    new {
                        Text = "Pack your things and head out with Áilu",
                        NextSceneId = 17, // next act/scene
                        TrustChange = +1,
                        IsCorrect = true,
                        ResponseDialog = "Oaidnaleapmi iđđes! (See you tomorrow!)"
                    }
                }
            }
        };
    }
}