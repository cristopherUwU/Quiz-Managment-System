using System.Collections.Generic;

namespace QuizManagementSystem.Models
{
    public class Quiz
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}