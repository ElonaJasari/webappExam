namespace FirstMVC.StoryContent.Act3;

public static class Act3_05_Endings
{
    // will choose ending based on trust score
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            //Bad Ending (Trust < 50)
            new {
                SceneId = 62,
                ActCategory = 3,
                Title = "A Difficult Week",
                CharacterCode = (string?)null,
                ImageUrl = (string?)"/images/bedroom.png",
                Content =
                    "You walk home slowly, feeling uncertain.\r\n\r\n" +
                    "Your parent greets you: \"Mo don orrot?\" (How are you?)\r\n\r\n" +
                    "You struggle to answer in Sámi, mixing languages or staying mostly silent.\r\n\r\n" +
                    "Your parent notices your hesitation: \"It's okay. Learning a language takes time. Don't give up.\"\r\n\r\n" +
                    "You realize you haven't practiced as much as you could have. Áilu and Áila were patient, but you didn't engage as fully as you might have.\r\n\r\n" +
                    "Still, you've learned some words. You know 'bures' (hello), 'giitu' (thanks), and a few others.\r\n\r\n" +
                    "Maybe next week, you'll try harder. Maybe you'll practice more. The journey isn't over—it's just beginning.\r\n\r\n" +
                    "Northern Sámi is still waiting for you to embrace it fully...",
                Choices = new[] {
                    new {
                        Text = "Accept that there's still more to learn",
                        NextSceneId = 65, // Triggers ending screen
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "You resolve to try harder next time."
                    }
                }
            },

            // Good Ending (Trust 50-99)
            new {
                SceneId = 63,
                ActCategory = 3,
                Title = "A Promising Start",
                CharacterCode = (string?)null,
                ImageUrl = (string?)"/images/bedroom.png",
                Content =
                    "You walk home with a smile, feeling proud of your progress.\r\n\r\n" +
                    "Your parent greets you: \"Mo don orrot?\" (How are you?)\r\n\r\n" +
                    "You answer confidently: \"Mun lean buorre, giitu!\" (I'm good, thanks!)\r\n\r\n" +
                    "Your parent's eyes light up: \"Don hállat buoremusat! (You speak so well!) I'm so proud of you!\"\r\n\r\n" +
                    "You think about your week:\r\n" +
                    "- You made friends with Áilu and Áila\r\n" +
                    "- You learned greetings, introductions, family words, and food words\r\n" +
                    "- You had real conversations in Northern Sámi\r\n" +
                    "- You're building confidence every day\r\n\r\n" +
                    "Áilu and Áila said they'll keep practicing with you. The teacher praised your progress.\r\n\r\n" +
                    "You're not fluent yet, but you're on the right path. Northern Sámi is becoming part of your life, one word at a time.\r\n\r\n" +
                    "Next week, you'll learn even more. The journey continues, and you're excited to see where it leads...",
                Choices = new[] {
                    new {
                        Text = "Feel proud of your progress",
                        NextSceneId = 65, // Triggers ending screen
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "You're ready to continue learning!"
                    }
                }
            },

            // True Ending (Trust 100+)
            new {
                SceneId = 64,
                ActCategory = 3,
                Title = "A True Connection",
                CharacterCode = (string?)null,
                ImageUrl = (string?)"/images/bedroom.png",
                Content =
                    "You walk home with your head held high, feeling a deep sense of accomplishment.\r\n\r\n" +
                    "Your parent greets you: \"Mo don orrot?\" (How are you?)\r\n\r\n" +
                    "You answer fluently: \"Mun lean hui buorre! Mun oahppan buoremusat dán vahku!\" (I'm very good! I learned so well this week!)\r\n\r\n" +
                    "Your parent is amazed: \"Oho! Don hállat bures! (Wow! You speak well!) You've truly embraced Northern Sámi!\"\r\n\r\n" +
                    "You reflect on your incredible week:\r\n" +
                    "- You formed strong friendships with Áilu and Áila\r\n" +
                    "- You mastered greetings, introductions, family words, and food vocabulary\r\n" +
                    "- You had fluent conversations entirely in Northern Sámi\r\n" +
                    "- You helped other students and became a role model\r\n" +
                    "- The teacher said you're one of the most dedicated students\r\n\r\n" +
                    "Áilu and Áila consider you a true friend and language partner. They're excited to continue learning with you.\r\n\r\n" +
                    "But more than that—you've discovered something deeper. Northern Sámi isn't just a language you're learning. It's a connection to culture, to people, to a way of life.\r\n\r\n" +
                    "You've found your voice in Northern Sámi. You've found your place in this community.\r\n\r\n" +
                    "As you sit at home, you realize: this is just the beginning. You're not just learning words—you're becoming part of something greater.\r\n\r\n" +
                    "Northern Sámi has opened its heart to you, and you've opened yours to it.\r\n\r\n" +
                    "The journey ahead is bright, and you're ready to walk it with confidence, friendship, and a true love for the language...",
                Choices = new[] {
                    new {
                        Text = "Embrace your new connection to Northern Sámi",
                        NextSceneId = 65, // Triggers ending screen
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "You've found your path in Northern Sámi."
                    }
                }
            }
        };
    }
}