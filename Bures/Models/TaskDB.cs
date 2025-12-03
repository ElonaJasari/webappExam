using System.ComponentModel.DataAnnotations;

namespace Bures.Models
{
    /// <summary>
    /// Represents a test task in the language learning game.
    /// Each task is a word or sentence the teacher wants to test the student on,
    /// including its Northern Sami text and Norwegian translation/description.
    /// </summary>
    public class TaskDB
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [Display(Name = "Text (Northern Sami)")]
        public string Text { get; set; } = string.Empty;

        // Word, sentence, verb, noun etc.
        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; } = string.Empty;

        // Optional link to the story act where this task belongs.
        [Display(Name = "Story Act Id")]
        public int? StoryActId { get; set; }

        // Norwegian translation or explanation used in the test.
        [Required]
        [Display(Name = "Translation / Description (Norwegian)")]
        public string Description { get; set; } = string.Empty;
    }
}


