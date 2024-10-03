namespace QuizManagementSystem.Models
{
    public class ShortAnswerQuestion : Question
    {
        public string CorrectAnswer { get; set; }

        public override bool CheckAnswer(string answer)
        {
            return string.Equals(CorrectAnswer, answer, System.StringComparison.OrdinalIgnoreCase);
        }
    }
}