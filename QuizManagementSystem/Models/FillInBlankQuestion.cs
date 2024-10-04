namespace QuizManagementSystem.Models
{
    public class FillInBlankQuestion : Question
    {
        public string CorrectAnswer { get; set; }

        public override bool CheckAnswer(string userAnswer)
        {
            var similarity = CalculateSimilarity(userAnswer.ToLower(), CorrectAnswer.ToLower());
            return similarity >= 0.90;
        }

        private double CalculateSimilarity(string userAnswer, string correctAnswer)
        {
            var userWords = userAnswer.Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            var correctWords = correctAnswer.Split(new char[] { ' ', '.', ',', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            var matchCount = 0;

            foreach (var word in userWords)
            {
                if (correctWords.Contains(word))
                {
                    matchCount++;
                }
            }
            
            // To avoid division by zero, ensure correctWords has at least one element
            var similarity = correctWords.Length > 0 ? (double)matchCount / correctWords.Length : 0.0;
            return similarity;
        }
    }
}