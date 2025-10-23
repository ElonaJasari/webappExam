using System.ComponentModel.DataAnnotations;

namespace FirstMVC.Models
{
    public class DictionaryWord
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;   // Verb, Substantiv, Adjektiv
        public int StoryActId { get; set; }               // Hvilken StoryAct ordet er koblet til
        public string Description { get; set; } = string.Empty; // Norsk oversettelse
    }
}
