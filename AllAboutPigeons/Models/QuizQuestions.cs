namespace AllAboutPigeons.Models
{
    public class QuizQuestions

    {
        public Dictionary<int, String> Questions { get; set; }
        public Dictionary<int, String> Answers { get; set; }
        public Dictionary<int, String> UserAnswers { get; set; }  
        public Dictionary <int, bool> Results { get; set; } // result of checking the answers
    }
}
