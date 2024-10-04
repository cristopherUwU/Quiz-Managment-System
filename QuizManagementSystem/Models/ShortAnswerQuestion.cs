namespace QuizManagementSystem.Models
{
    public class ShortAnswerQuestion : Question
    {
        public List<string> Keywords { get; set; }

        public ShortAnswerQuestion()
        {
            Keywords = new List<string>();
        }

        public override bool CheckAnswer(string userAnswer)
        {
            var lowerCaseAnswer = userAnswer.ToLower();
            
            foreach (var keyword in Keywords)
            {
                if (!lowerCaseAnswer.Contains(keyword.ToLower()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}