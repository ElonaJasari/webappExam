namespace FirstMVC.StoryContent.Act3;

public static class Act3_04_FinalTest
{
    // Act 3: Final test and ending preparation (58-60)
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 58 — Final test conversation
            new {
                SceneId = 58,
                ActCategory = 3,
                Title = "Final Test",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Teacher: \"Bures! Mo don orrot?\" (Hello! How are you?)\r\n\r\n" +
                    "You answer confidently.\r\n\r\n" +
                    "Teacher: \"Gos don orrot?\" (Where do you live?)\r\n\r\n" +
                    "You respond.\r\n\r\n" +
                    "Teacher: \"Maid don háliidat borrat?\" (What do you want to eat?)\r\n\r\n" +
                    "You think carefully and answer.",
                Choices = new[] {
                    new {
                        Text = "Answer all questions in Sámi confidently",
                        NextSceneId = 59,
                        TrustChange = +8,
                        IsCorrect = true,
                        ResponseDialog = "Teacher: \"Fábelaš! (Excellent!) You've mastered the basics!\""
                    },
                    new {
                        Text = "Answer some in Sámi, some mixing languages",
                        NextSceneId = 59,
                        TrustChange = +4,
                        IsCorrect = true,
                        ResponseDialog = "Teacher: \"Buorre! (Good!) You're making progress!\""
                    },
                    new {
                        Text = "Struggle but keep trying",
                        NextSceneId = 59,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Teacher: \"You tried your best! That's what matters!\""
                    }
                }
            },

            // Scene 59 — Converge: Celebration and ending
            new {
                SceneId = 59,
                ActCategory = 3,
                Title = "Celebration",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "The teacher smiles warmly at the whole class.\r\n\r\n" +
                    "Teacher: \"You've all done wonderfully! You've learned greetings, introductions, family words, and food words in Northern Sámi.\"",
                Choices = new[] {
                    new {
                        Text = "Smile and feel proud",
                        NextSceneId = 60,
                        TrustChange = +1,
                        IsCorrect = true,
                        ResponseDialog = "The class applauds. You've come so far!"
                    }
                }
            },

            // Scene 60 — Final scene before ending
            new {
                SceneId = 60,
                ActCategory = 3,
                Title = "End of Week",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "After class, Áilu and Áila come up to you together.\r\n\r\n" +
                    "Áilu: \"Don oahppat buoremusat! (You learned so well!) I'm proud to be your friend!\"\r\n\r\n" +
                    "Áila: \"Jus don vealahit du! (If you help me!) We'll keep practicing together!\"\r\n\r\n" +
                    "You've completed your first week of Northern Sámi lessons. You've made friends, learned a new language, and grown in confidence.\r\n\r\n" +
                    "This is just the beginning of your journey...",
                Choices = new[] {
                    new {
                        Text = "Thank both: \"Giitu, Áilu ja Áila!\"",
                        NextSceneId = 61,
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Both: \"Ipmelattá! (You're welcome!) See you next week!\""
                    }
                }
            },

            // Scene 61 — Final convergence point (triggers ending calculation)
            new {
                SceneId = 61,
                ActCategory = 3,
                Title = "Journey Continues",
                CharacterCode = (string?)null,
                ImageUrl = (string?)"/images/home.png",
                Content =
                    "You walk home, reflecting on everything you've learned.\r\n\r\n" +
                    "Northern Sámi is no longer a foreign language—it's becoming part of who you are.\r\n\r\n" +
                    "Your parent greets you at the door: \"Mo don orrot?\" (How are you?)\r\n\r\n" +
                    "You think about your week—the friends you've made, the words you've learned, and how far you've come...",
                Choices = new[] {
                    new {
                        Text = "Reflect on your journey",
                        NextSceneId = 62, // Will route to 62, 63, or 64 based on trust
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "You've completed your first week of learning!"
                    }
                }
            }
        };
    }
}

