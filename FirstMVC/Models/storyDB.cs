using System.ComponentModel.DataAnnotations;

namespace FirstMVC.Models
{
    public class StoryAct {
        [Key]
        public int StoryActId { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    public int? CharacterId { get; set; }
    public Characters? Character { get; set; }

    public List<Choice> Choices { get; set; } = new();
}

    public class Choice{
        public int Id { get; set; }
        public string? Text { get; set; }
        public int NextActId { get; set; }
        public bool IsCorrect { get; set; }

        /// Chages in trust value (-5, 0 or +5)
        public int TrustChange { get; set; }

        public int StoryActId { get; set; }
        public StoryAct StoryAct { get; set; } = null!;
    }

}
