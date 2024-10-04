namespace QuizManagementSystem.Models
{
    public class QuizAttempt
    {
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }

        public double Accuracy => (TotalQuestions > 0) ? (double)CorrectAnswers / TotalQuestions * 100 : 0;
    }

}