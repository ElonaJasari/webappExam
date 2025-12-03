using System.ComponentModel.DataAnnotations;

using FirstMVC.Models;

public class UserProgressDB
{
    [Key]
    public int UserProgressID { get; set; }

    public string UserID { get; set; } = string.Empty;

    /// Which of the three story acts the user in in right now
    public int CurrentStoryActId { get; set; }

    /// Trust Value, starting at zero and progressing thourg the game 
    public int Trust { get; set; } = 0;

    /// Choosing the ending type based on the hidden trust value at the end of act 3 
    public string? EndingType { get; set; }

    public StoryAct? CurrentStoryAct { get; set; }

    public DateTime LastUpdated { get; set; }
    
}