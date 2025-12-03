namespace FirstMVC.StoryContent.Act1;

public static class Act1_02_SchoolInfo
{
    public static IEnumerable<dynamic> GetScenes()
    {
        return new[]
        {
            // Scene 6 — Teacher introduces you, explains class will be in Northern Sámi
            new {
                SceneId = 6,
                ActCategory = 1,
                Title = "Class Introduction",
                CharacterCode = "ID_TEACHER",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Good morning everyone! We have a new student with us today.\r\n\r\n" +
                    "(English) Welcome to the course. We'll be learning together this term.\r\n\r\n" +
                    "From now on, this class is conducted in Northern Sámi. Try to speak Sámi with one another as much as you can. " +
                    "Don't worry if you're new to it—everyone helps each other here.\r\n\r\n" +
                    "[The teacher gestures toward the student next to you.] Please introduce yourselves.",
                Choices = new[] { // add Choices wrapper
                    new {
                        Text = "Nod and look to the classmates",
                        NextSceneId = 7,
                        TrustChange = 0,
                        IsCorrect = true,
                        ResponseDialog = "Good! Start by saying hello."
                    }
                }
            },

            // Scene 6 — Friend greets you in Sámi
            new {
                SceneId = 7,
                ActCategory = 1,
                Title = "A Classmate Greets You",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Bures! Mo don orrot? Mun lean Áilu. \r\n\r\n" +
                    "(Hello! How are you? I'm Áilu.)",
                Choices = new[] {
                    new {
                        Text = "Answer in perfect Sámi",
                        NextSceneId = 8,
                        TrustChange = +6,
                        IsCorrect = true,
                        ResponseDialog = "Oho! Don hállat bures! (Wow, you speak well!)"
                    },
                    new {
                        Text = "Stare at him blankly",
                        NextSceneId = 9,
                        TrustChange = -4,
                        IsCorrect = false,
                        ResponseDialog = "Uh… lea okei. Sáhtán ságastit buoremusat ja languidit. (It’s okay. I can speak slower and help.)"
                    },
                    new {
                        Text = "I don't understand",
                        NextSceneId = 10,
                        TrustChange = +1,
                        IsCorrect = true,
                        ResponseDialog = "Ii leat váttis. Mun vealahin du! (No problem. I’ll help you!)"
                    }
                }
            },

            // Scene 8 — Branch for "perfect Sámi"
            new {
                SceneId = 8,
                ActCategory = 1,
                Title = "Impressed Friend",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Oho! Don hállat bures! (Wow, you speak well!)",
                Choices = new[] {
                    new {
                        Text = "Smile and keep talking",
                        NextSceneId = 11,
                        TrustChange = +1,
                        IsCorrect = true,
                        ResponseDialog = "Boađán, vázzit ovttas! (Come on, let’s walk together!)"
                    }
                }
            },

            // Scene 9 — Branch for "stare blankly"
            new {
                SceneId = 9,
                ActCategory = 1,
                Title = "Awkward Moment",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Uh… lea okei. Sáhtán ságastit buoremusat ja languidit. (It's okay. I can speak slower and help.)",
                Choices = new[] {
                    new {
                        Text = "Nod gratefully",
                        NextSceneId = 11,
                        TrustChange = +1,
                        IsCorrect = true,
                        ResponseDialog = "Iige váttis — buot oahpásmuvvat! (No worries — everyone learns!)"
                    }
                }
            },

            // Scene 10 — Branch for "I don't understand"
            new {
                SceneId = 10,
                ActCategory = 1,
                Title = "Kind Encouragement",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Ii leat váttis. Mun vealahin du! (No problem. I'll help you!)",
                Choices = new[] {
                    new {
                        Text = "Thank them for helping",
                        NextSceneId = 11,
                        TrustChange = +1,
                        IsCorrect = true,
                        ResponseDialog = "Ipmelattá! (You’re welcome!)"
                    }
                }
            },

            // Scene 11 — Converge: Friend introduces themself properly
            new {
                SceneId = 11,
                ActCategory = 1,
                Title = "Proper Introductions",
                CharacterCode = "ID_FRIEND1",
                ImageUrl = (string?)"/images/classroom.png",
                Content =
                    "Bures! Mun lean Áilu, muhto sáhttet čuožžut Áilu. Mun lean du oahppis. (Hi! I'm Áilu. I'm your classmate.)\r\n\r\n" +
                    "Gulahallat Sámegiela ovttas, ja mun vealahin du go dárbbašat. (We'll speak Sámi together, and I'll help when you need.)",
                Choices = new[] {
                    new {
                        Text = "Nice to meet you, Áilu!",
                        NextSceneId = 12, // Next act starts at 12
                        TrustChange = +2,
                        IsCorrect = true,
                        ResponseDialog = "Soma beassat diehtit du! (Nice to meet you too!)"
                    }
                } //
            }
        };
    }
}

