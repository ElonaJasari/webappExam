using System.ComponentModel.DataAnnotations;

namespace FirstMVC.Models
{
    public class DictionaryWord
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;   // verbs, substantives, adjectives
        public int StoryActId { get; set; }               // which StoryAct the word is associated with (may remove later?)
        public string Description { get; set; } = string.Empty; // Nork translation / description
    }
}
