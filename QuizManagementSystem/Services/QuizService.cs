using QuizManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuizManagementSystem.Services
{
    public class QuizService
    {
        private List<Quiz> _quizzes = new List<Quiz>();

        public void AddQuiz(Quiz quiz)
        {
            _quizzes.Add(quiz);
        }

        public List<Quiz> GetAllQuizzes()
        {
            return _quizzes;
        }
        
        public Quiz GetQuizByTitle(string title)
        {
            return _quizzes.FirstOrDefault(q => q.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }
        
        public void AssignQuizToUser(User user, Quiz quiz)
        {
            if (!_quizzes.Contains(quiz))
            {
                Console.WriteLine("Quiz does not exist.");
                return;
            }

            user.AssignedQuizzes.Add(quiz);
            Console.WriteLine($"Quiz '{quiz.Title}' assigned to {user.Nickname}.");
        }
    }
}