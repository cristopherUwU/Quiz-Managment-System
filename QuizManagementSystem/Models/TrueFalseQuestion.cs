namespace QuizManagementSystem.Models
{
    public class TrueFalseQuestion : Question
    {
        public bool CorrectAnswer { get; set; }

        public override bool CheckAnswer(string answer)
        {
            return bool.TryParse(answer, out bool userAnswer) && userAnswer == CorrectAnswer;
        }
    }
}