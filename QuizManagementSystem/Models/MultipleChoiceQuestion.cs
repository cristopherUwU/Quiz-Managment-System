namespace QuizManagementSystem.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectOption { get; set; }

        public override bool CheckAnswer(string answer)
        {
            if (int.TryParse(answer, out int answerIndex))
            {
                return answerIndex == CorrectOption;
            }
            return false;
        }
    }
}