namespace FirstMVC.ViewModels
{
    public class QuizResultViewModel
    {
        public int Act { get; set; }
        public int CorrectCount { get; set; }
        public int TotalCount { get; set; }
        public bool Passed { get; set; }
    }
}
