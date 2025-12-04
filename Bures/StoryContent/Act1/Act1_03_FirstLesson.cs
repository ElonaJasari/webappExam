namespace Bures.StoryContent.Act1;

public static class Act1_03_FirstLesson
{
    // Continues after Act1_02 (last NextSceneId was 11)
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // 12 — Teacher asks you to introduce yourself in Sámi
            new {
                SceneId = 12,
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
                        NextSceneId = 13,
                        TrustChange = +5,
                        IsCorrect = true,
                        ResponseDialog = "Nugo, hui buorre! (Exactly, very good!)"
                    },
                    new {
                        Text = "Answer in English: \"My name is ...\"",
                        NextSceneId = 14,
                        TrustChange = -2,
                        IsCorrect = false,
                        ResponseDialog = "Buorre álgu, muhto geahččal Sámegillii. (Good start, but try in Sámi.)"
                    },
                    new {
                        Text = "Freeze up and say nothing",
                        NextSceneId = 15,
                        TrustChange = -5,
                        IsCorrect = false,
                        ResponseDialog = "Ii leat váttis. Bassi beaivi — let’s try together. (No problem. Let’s try together.)"
                    }
                }
            },

            // 12 — Perfect answer branch
            new {
                SceneId = 13,
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
                        NextSceneId = 16,
                        TrustChange = +1,
                        IsCorrect = true,
                        ResponseDialog = "Iežat leat buorre ovdánan! (You’re progressing well!)"
                    }
                }
            },

            // 13 — English answer branch
            new {
                SceneId = 14,
                ActCategory = 1,
                Title = "Try In Sámi",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Geahččal fas: \"Mun namma lea ...\". I'll help you with pronunciation.",
                Choices = new[] {
                    new {
                        Text = "Repeat carefully in Sámi",
                        NextSceneId = 16,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Dakka! (There you go!)"
                    }
                }
            },

            // 14 — Froze up branch
            new {
                SceneId = 15,
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
                        NextSceneId = 16,
                        TrustChange = +1,
                        IsCorrect = true,
                        ResponseDialog = "Buorre! (Good!)"
                    }
                }
            },

            // 15 — Converge: Pair practice with Áilu
            new {
                SceneId = 16,
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
                        NextSceneId = 17,
                        TrustChange = +4,
                        IsCorrect = true,
                        ResponseDialog = "Iežat! (Nice!)"
                    },
                    new {
                        Text = "Make a joke instead of answering",
                        NextSceneId = 18,
                        TrustChange = 0,
                        IsCorrect = false,
                        ResponseDialog = "Hehe… muhto geahččalit ge sániid. (Heh… but try the words too.)"
                    },
                    new {
                        Text = "Admit you don't know how to say it",
                        NextSceneId = 19,
                        TrustChange = +1,
                        IsCorrect = true,
                        ResponseDialog = "Mun vealahin du. (I’ll help you.)"
                    }
                }
            },

            // 17 — Response to "Answer in Sámi"
            new {
                SceneId = 17,
                ActCategory = 1,
                Title = "Good Answer",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Iežat! (Nice!)",
                Choices = new[] {
                    new {
                        Text = "Continue",
                        NextSceneId = 20,
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "Let's keep going."
                    }
                }
            },

            // 18 — Response to "Make a joke"
            new {
                SceneId = 18,
                ActCategory = 1,
                Title = "Joking Around",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Hehe… muhto geahččalit ge sániid. (Heh… but try the words too.)",
                Choices = new[] {
                    new {
                        Text = "Continue",
                        NextSceneId = 19,
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "Let's keep going."
                    }
                }
            },

            // 19 — Response to "Admit you don't know"
            new {
                SceneId = 19,
                ActCategory = 1,
                Title = "Help Offered",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Mun vealahin du. (I'll help you.)",
                Choices = new[] {
                    new {
                        Text = "Sure, lets go",
                        NextSceneId = 20,
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "Let's keep going."
                    }
                }
            },

            // 20 — Converge: Wrap-up and homework
            new {
                SceneId = 20,
                ActCategory = 1,
                Title = "Wrap-Up",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Great work today!\r\n\r\n" +
                    "Homework: practice \"Mun namma lea ...\", \"Gos don orrot?\".\r\n\r\n" +
                    "And the words written on the tabelt.\r\n\r\n" +
                    "Tomorrow we'll learn greetings and small talk.",
                Choices = new[] {
                    new {
                        Text = "Pack your things and head out with Áilu",
                        NextSceneId = 21, // Routes to Scene 21 (iPad vocabulary scene)
                        TrustChange = +0,
                        IsCorrect = true,
                        ResponseDialog = "Oaidnaleapmi iđđes! (See you tomorrow!)"
                    }
                }
            }, 
            new {
                SceneId = 21,
                ActCategory = 1,
                Title = "iPad Practice",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                    Content =
                        "Before you leave, the teacher gives out an iPad to each student.\r\n\r\n" +
                        "On the screen, you see today's new words and phrases.\r\n\r\n" +
                        "Take a moment to review them and practice writing each one.",
                Choices = new[] {
                    new {
                        Text = "Review the words on the iPad",
                        NextSceneId = 22, // Routes to Scene 22 (Act1_04_AfterSchool)
                        TrustChange = +0,
                        IsCorrect = true,
                        ResponseDialog = "Buorre! (Good job reviewing!)"
                    }
                }
            }
        };
    }
}