namespace Bures.StoryContent.Act2;

public static class Act2_02_SchoolGreeting
{
    // Act 2: School greeting branches converging at lesson (31-34)
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 31 — Confident greeting branch
            new {
                SceneId = 31,
                ActCategory = 2,
                Title = "Impressive Progress",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Áilu's eyes widen with surprise and delight.\r\n\r\n" +
                    "Áilu: \"Don oahppat buoremusat! (You learn so well!) Today we'll learn about family words.\"\r\n\r\n" +
                    "As you walk into the classroom, you notice another student sitting nearby. Áilu notices your glance.\r\n\r\n" +
                    "Áilu: \"Oh! That's Áila. She's also learning Sámi. Let me introduce you!\"",
                Choices = new[] {
                    new {
                        Text = "Show interest: \"Sáhtán oahppat?\" (Can I learn?)",
                        NextSceneId = 34,
                        TrustChange = +3,
                        IsCorrect = true,
                        ResponseDialog = "Áilu: \"Of course! And Áila can help too!\""
                    }
                }
            },

            // Scene 32 — Simple greeting branch
            new {
                SceneId = 32,
                ActCategory = 2,
                Title = "Friendly Walk",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "You walk together into the classroom.\r\n\r\n" +
                    "Áilu: \"Today we'll learn family words. Ready?\"",
                Choices = new[] {
                    new {
                        Text = "Nod enthusiastically",
                        NextSceneId = 34,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Great! Let's begin!"
                    }
                }
            },

            // Scene 33 — Shy branch
            new {
                SceneId = 33,
                ActCategory = 2,
                Title = "Encouragement",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Áilu pats your shoulder.\r\n\r\n" +
                    "Áilu: \"Ii leat váttis. Everyone learns at their own pace. Today we'll practice family words together.\"",
                Choices = new[] {
                    new {
                        Text = "Smile and say \"Giitu\" (Thanks)",
                        NextSceneId = 34,
                        TrustChange = +3,
                        IsCorrect = true,
                        ResponseDialog = "You're welcome! Let's learn!"
                    }
                }
            },

            // Scene 34 — Converge: Family words lesson with Áila introduction
            new {
                SceneId = 34,
                ActCategory = 2,
                Title = "Meeting Áila",
                CharacterCode = "ID_FRIEND2",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Áilu leads you to Áila's desk.\r\n\r\n" +
                    "Áilu: \"Áila, dát lea...\" (Áila, this is...)\r\n\r\n" +
                    "Áila looks up and smiles warmly.\r\n\r\n" +
                    "Áila: \"Bures! Mun namma lea Áila. Soma beassat diehtit du!\" (Hello! My name is Áila. Nice to meet you!)\r\n\r\n" +
                    "She speaks with a gentle, encouraging tone.\r\n\r\n" +
                    "The teacher stands up: \"Today we have two learning activities. You can practice family words with your friends, or join the sentence learning session at the blackboard.\"",
                Choices = new[] {
                    new {
                        Text = "Answer: \"Bures! Mun namma lea... Soma beassat diehtit du!\"",
                        NextSceneId = 35,
                        TrustChange = +5,
                        IsCorrect = true,
                        ResponseDialog = "Áila: \"Oho! Don hállat bures! (Wow! You speak well!)\""
                    },
                    new {
                        Text = "Say: \"Bures! Mun lean... nervous\"",
                        NextSceneId = 36,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Áila: \"Ii leat váttis! (No worries!) We're all learning together!\""
                    },
                    new {
                        Text = "Just wave and smile",
                        NextSceneId = 37,
                        TrustChange = +1,
                        IsCorrect = false,
                        ResponseDialog = "Áila: \"Don sáhtát ságastit Sámegillii goas don háliidat! (You can speak Sámi whenever you want!)\""
                    },
                    new {
                        Text = "Ask to join the sentence learning session",
                        NextSceneId = 38,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Teacher: \"Good choice! Let's practice sentences together!\""
                    }
                }
            },

            // Scene 35 — Good introduction branch
            new {
                SceneId = 35,
                ActCategory = 2,
                Title = "Three Friends",
                CharacterCode = "ID_FRIEND2",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Áila's face lights up with joy.\r\n\r\n" +
                    "Áila: \"Hui buorre! (Very good!) We should practice together! Áilu and I can help you!\"\r\n\r\n" +
                    "The teacher calls the class to attention.\r\n\r\n" +
                    "Teacher: \"Today we learn family: 'eahket' (mother), 'áhčči' (father), 'oappát' (sister), 'viellja' (brother).\"",
                Choices = new[] {
                    new {
                        Text = "Repeat each word carefully with Áilu and Áila",
                        NextSceneId = 39,
                        TrustChange = +5,
                        IsCorrect = true,
                        ResponseDialog = "Both friends smile: \"Perfect!\""
                    }
                }
            },

            // Scene 36 — Nervous introduction branch
            new {
                SceneId = 36,
                ActCategory = 2,
                Title = "Encouraging Friends",
                CharacterCode = "ID_FRIEND2",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Áila and Áilu both give you encouraging smiles.\r\n\r\n" +
                    "Áila: \"Ii leat váttis! (No worries!) We're all learning. Let's practice together!\"\r\n\r\n" +
                    "Áilu: \"Jus don vealahit du! (If you help me!) We'll help each other!\"\r\n\r\n" +
                    "The teacher begins the lesson: \"Today we learn family words...\"\r\n\r\n" +
                    "You, Áilu, and Áila practice together. Áilu: \"Say: 'eahket' (mother)...\" Áila: \"Then 'áhčči' (father)...\"",
                Choices = new[] {
                    new {
                        Text = "Practice with both friends",
                        NextSceneId = 40,
                        TrustChange = +3,
                        IsCorrect = true,
                        ResponseDialog = "You feel supported by your new friends."
                    }
                }
            },

            // Scene 37 — Shy introduction branch
            new {
                SceneId = 37,
                ActCategory = 2,
                Title = "Patient Friends",
                CharacterCode = "ID_FRIEND2",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Áila smiles understandingly.\r\n\r\n" +
                    "Áila: \"Don sáhtát ságastit goas don háliidat. (You can speak whenever you want.) No pressure!\"\r\n\r\n" +
                    "Áilu: \"We'll practice together. It's more fun with friends!\"\r\n\r\n" +
                    "The teacher starts: \"Family words: 'eahket', 'áhčči', 'oappát', 'viellja'...\"\r\n\r\n" +
                    "You try repeating the words quietly with Áilu and Áila helping you.",
                Choices = new[] {
                    new {
                        Text = "Continue practicing",
                        NextSceneId = 39,
                        TrustChange = +3,
                        IsCorrect = true,
                        ResponseDialog = "Áila: \"Good! You're trying!\""
                    }
                }
            }
        };
    }
}