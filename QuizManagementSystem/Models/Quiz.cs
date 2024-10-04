using System.Collections.Generic;

namespace QuizManagementSystem.Models
{
    public class Quiz
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
        public bool AllowRetake { get; set; }
        public bool RandomizeQuestions { get; set; }
        public List<QuizAttempt> Attempts { get; set; }
        
        public Dictionary<string, int> UserAttempts { get; set; }
        
        public Quiz(string title, string description, List<Question> questions, bool allowRetake, bool randomizeQuestions)
        {
            Title = title;
            Description = description;
            Questions = questions;
            AllowRetake = allowRetake;
            RandomizeQuestions = randomizeQuestions;
            Attempts = new List<QuizAttempt>();
        }
        
    }
}