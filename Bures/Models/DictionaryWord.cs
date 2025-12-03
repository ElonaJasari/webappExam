using System.ComponentModel.DataAnnotations;

namespace Bures.Models
{
    public class DictionaryWord
    {
        [Key]
        public int Id { get; set; }


        public string Text { get; set; } = string.Empty;
        // verbs, nouns, adjectives
        public string Type { get; set; } = string.Empty;

        // which StoryAct the word is associated with (may remove later?)   
        public int StoryActId { get; set; }
        
        // Nork translation / description              
        public string Description { get; set; } = string.Empty;
    }
}
