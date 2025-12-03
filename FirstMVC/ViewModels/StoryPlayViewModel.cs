using FirstMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace FirstMVC.ViewModels
{
    public class StoryPlayViewModel
    {
        public StoryAct CurrentAct { get; set; } = null!;
        public IEnumerable<Choice> Choices { get; set; } = Enumerable.Empty<Choice>();
        public Characters? Character { get; set; }
        public Characters? PlayerCharacter { get; set; }
        public string? CurrentViewName { get; set; }

        // User progress information
        public int CurrentStoryActId { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int Trust { get; set; }
        public string? SelectedCharacterName { get; set; }

        // simple error messaging when a choice/reset fails
        public string? ErrorMessage { get; set; }
        
        // For form submission
        [Display(Name = "Selected Choice")]
        public int? SelectedChoiceId { get; set; }

        // Display helpers
        public bool HasCharacter => Character != null;
        public bool HasChoices => Choices.Any();
        public string DisplayTitle => CurrentAct?.Title ?? "Story";
        
        public StoryPlayViewModel() { }

        public StoryPlayViewModel(StoryAct storyAct, IEnumerable<Choice> choices, Characters? character, string? currentViewName, DateTime? lastUpdated = null)
        {
            CurrentAct = storyAct ?? throw new ArgumentNullException(nameof(storyAct));
            Choices = choices ?? Enumerable.Empty<Choice>();
            Character = character;
            CurrentViewName = currentViewName;
            CurrentStoryActId = storyAct.StoryActId;
            LastUpdated = lastUpdated;
        }

        // Helper method to create from UserProgress
        public static StoryPlayViewModel FromUserProgress(UserProgressDB progress)
        {
            if (progress?.CurrentStoryAct == null)
                throw new ArgumentException("Invalid user progress data", nameof(progress));

            var vm = new StoryPlayViewModel(
                progress.CurrentStoryAct,
                progress.CurrentStoryAct.Choices,
                progress.CurrentStoryAct.Character,
                null,
                progress.LastUpdated
            );
            vm.SelectedCharacterName = progress.SelectedCharacterName;
            return vm;
        }
    }
}