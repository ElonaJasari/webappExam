using System.ComponentModel.DataAnnotations;

namespace Bures.Models
{
    /// <summary>
    /// Represents a character entity in the story system
    /// Used for character-based storytelling and language learning
    /// </summary>
    public class Characters
    {
        [Key]    
        public int CharacterID { get; set; } 

        [Required(ErrorMessage = "Character name is required")]
        [StringLength(66, ErrorMessage = "Name cannot exceed 66 characters")]
        [Display(Name = "Character Name")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        [Display(Name = "Character Description")]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Character role is required")]
        [StringLength(50, ErrorMessage = "Role cannot exceed 50 characters")]
        [Display(Name = "Character Role")]
        public string Role { get; set; } = string.Empty;
        
        // Hidden field - internal identifier for code logic (ID_FRIEND1, ID_TEACHER, etc.)
        [StringLength(50)]
        public string? CharacterCode { get; set; }
        
        // Didnt do Errormessage fsince the dialog amount is unknown
        public string? Dialog { get; set; }
        
        [Url(ErrorMessage = "Please enter a valid URL")]
        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }
        
        // Didnt do Errormessage fsince the dialog amount is unknown
        public string? Translate { get; set; }

    public List<StoryAct>? StoryActs { get; set; }
}

}