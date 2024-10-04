using System.Collections.Generic;

namespace QuizManagementSystem.Models
{
    public class FillInBlankQuestion : Question
    {
        public List<string> Keywords { get; set; }

        public FillInBlankQuestion()
        {
            Keywords = new List<string>();
        }

        public override bool CheckAnswer(string userAnswer)
        {
            foreach (var keyword in Keywords)
            {
                if (!userAnswer.ToLower().Contains(keyword.ToLower()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}