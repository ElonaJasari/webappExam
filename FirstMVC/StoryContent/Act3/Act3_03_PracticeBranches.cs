namespace FirstMVC.StoryContent.Act3;

public static class Act3_03_PracticeBranches
{
    // Act 3: Practice branches converging at final conversation (53-57)
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 53 — Volunteer branch
            new {
                SceneId = 53,
                ActCategory = 3,
                Title = "Confident Practice",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "You stand up and practice with the teacher.\r\n\r\n" +
                    "Teacher: \"Bures! Mo don orrot?\" (Hello! How are you?)\r\n\r\n" +
                    "You respond confidently, using all the words you've learned.",
                Choices = new[] {
                    new {
                        Text = "Answer: \"Bures! Mun lean buorre, giitu! Mun namma lea...\"",
                        NextSceneId = 57,
                        TrustChange = +6,
                        IsCorrect = true,
                        ResponseDialog = "Teacher: \"Fábelaš! (Excellent!) You've learned so much!\""
                    }
                }
            },

            // Scene 54 — Observing branch
            new {
                SceneId = 54,
                ActCategory = 3,
                Title = "Learning by Watching",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "You watch other students practice, learning from their examples.\r\n\r\n" +
                    "Teacher: \"Now it's your turn. Don't worry, just try!\"",
                Choices = new[] {
                    new {
                        Text = "Try: \"Bures! Mun lean... buorre\"",
                        NextSceneId = 57,
                        TrustChange = +4,
                        IsCorrect = true,
                        ResponseDialog = "Teacher: \"Buorre! (Good!) You're doing great!\""
                    }
                }
            },

            // Scene 55 — Practice with Áilu branch
            new {
                SceneId = 55,
                ActCategory = 3,
                Title = "Peer Practice",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "You and Áilu practice together quietly.\r\n\r\n" +
                    "Áilu: \"Bures! Mo don orrot? Mun namma lea Áilu.\" (Hello! How are you? My name is Áilu.)\r\n\r\n" +
                    "You practice responding, building confidence.",
                Choices = new[] {
                    new {
                        Text = "Answer: \"Bures! Mun lean buorre! Mun namma lea...\"",
                        NextSceneId = 57,
                        TrustChange = +5,
                        IsCorrect = true,
                        ResponseDialog = "Áilu: \"Perfect! You're ready!\""
                    }
                }
            },

            // Scene 56 — Additional practice scene (optional convergence point)
            // Actually, let's have scene 57 be the convergence

            // Scene 57 — Converge: Final conversation with teacher
            new {
                SceneId = 57,
                ActCategory = 3,
                Title = "Final Conversation",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "The teacher calls you up for a final conversation.\r\n\r\n" +
                    "Teacher: \"Let's have a full conversation. I'll ask you questions, and you answer in Sámi.\"",
                Choices = new[] {
                    new {
                        Text = "Nod confidently: \"Jus don vealahit du\"",
                        NextSceneId = 58,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Teacher: \"Of course! Let's begin!\""
                    }
                }
            }
        };
    }
}