namespace FirstMVC.StoryContent.Act3;

public static class Act3_02_FinalLesson
{
    // Act 3: Final lesson branches converging at practice (48-52)
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 48 — Confident greeting branch
            new {
                SceneId = 48,
                ActCategory = 3,
                Title = "Eager to Learn",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Áilu grins widely.\r\n\r\n" +
                    "Áilu: \"Don hállat buoremusat! (You speak so well!) Today we'll practice everything we learned!\"",
                Choices = new[] {
                    new {
                        Text = "Say: \"Jus don vealahit du!\" (If you help me!)",
                        NextSceneId = 52,
                        TrustChange = +3,
                        IsCorrect = true,
                        ResponseDialog = "Áilu: \"Of course! That's what friends do!\""
                    }
                }
            },

            // Scene 49 — Nervous greeting branch
            new {
                SceneId = 49,
                ActCategory = 3,
                Title = "Supportive Friend",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Áilu puts a hand on your shoulder.\r\n\r\n" +
                    "Áilu: \"Ii leat váttis! (No worries!) You've learned so much. Today we'll practice together!\"",
                Choices = new[] {
                    new {
                        Text = "Smile: \"Giitu, Áilu!\"",
                        NextSceneId = 52,
                        TrustChange = +3,
                        IsCorrect = true,
                        ResponseDialog = "Áilu: \"You're welcome! Let's go!\""
                    }
                }
            },

            // Scene 50 — Shy greeting branch
            new {
                SceneId = 50,
                ActCategory = 3,
                Title = "Encouragement",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Áilu walks with you into the classroom.\r\n\r\n" +
                    "Áilu: \"Today we'll review everything: greetings, family, food. You know these words!\"",
                Choices = new[] {
                    new {
                        Text = "Nod: \"Jus don vealahit du\"",
                        NextSceneId = 52,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Áilu: \"Always! Let's start!\""
                    }
                }
            },

            // Scene 51 — Teacher's final lesson intro (if needed, but let's go straight to 52)
            // Actually, let me add a scene 51 for the teacher's introduction

            // Scene 52 — Converge: Final practice session
            new {
                SceneId = 52,
                ActCategory = 3,
                Title = "Final Practice Session",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "The teacher addresses the class.\r\n\r\n" +
                    "Teacher: \"Today we practice everything: greetings, introductions, family, food.\"\r\n\r\n" +
                    "\"Let's have a conversation practice. Who wants to start?\"",
                Choices = new[] {
                    new {
                        Text = "Volunteer confidently",
                        NextSceneId = 53,
                        TrustChange = +5,
                        IsCorrect = true,
                        ResponseDialog = "Teacher: \"Hui buorre! (Very good!) Let's hear you!\""
                    },
                    new {
                        Text = "Wait and observe first",
                        NextSceneId = 54,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Teacher: \"That's fine. Watch and learn, then try!\""
                    },
                    new {
                        Text = "Practice quietly with Áilu",
                        NextSceneId = 55,
                        TrustChange = +3,
                        IsCorrect = true,
                        ResponseDialog = "Áilu: \"Good! Practice makes perfect!\""
                    }
                }
            }
        };
    }
}

