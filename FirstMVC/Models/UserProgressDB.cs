using System.ComponentModel.DataAnnotations;

using FirstMVC.Models;

public class UserProgressDB
{
    [Key]
    public int UserProgressID { get; set; }
    public string UserID { get; set; } = string.Empty;
    public int CurrentStoryActId { get; set; }

    public StoryAct? CurrentStoryAct { get; set; } 
    public DateTime LastUpdated { get; set; }

    public int? SelectedCharacterId { get; set; }
    public string SelectedCharacterName { get; set;}
    
}