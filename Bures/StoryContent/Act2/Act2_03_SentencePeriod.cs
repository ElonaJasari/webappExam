namespace Bures.StoryContent.Act2;

public static class Act2_03_SentencePeriod
{
    // Note: This file contains an alternative learning period scene
    // However, scene 38 already exists in Act2_02_SchoolGreeting.cs as "Family Words Practice"
    // This scene is currently disabled to avoid SceneId conflicts
    // If you want to use this, change the SceneId to a number that doesn't conflict (e.g., 43 or integrate it differently)
    public static IEnumerable<dynamic> GetScenes()
    {
        return Array.Empty<object>();
        // Disabled scene to avoid conflict with Act2_02 scene 38:
        /*
        new {
            SceneId = 38, // CONFLICTS with Act2_02 scene 38 - change this number if you want to use it
            ActCategory = 2,
            Title = "Sentence Learning Period",
            CharacterCode = "ID_TEACHER",
            ImageUrl = (string?)"/images/teacher.png",
            Content = "Teacher: 'Today we will advance a bit more for our sentence learning. Let's practice sentences on the blackboard.'",
            Choices = new[] {
                new {
                    Text = "Continue to practice",
                    NextSceneId = 39,
                    TrustChange = +2,
                    IsCorrect = true,
                    ResponseDialog = "You finish the sentence session and get ready for the rest of the day."
                }
            }
        }
        */
    }
}
