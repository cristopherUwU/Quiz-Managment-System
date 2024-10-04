using QuizManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuizManagementSystem.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private List<Quiz> quizzes = new List<Quiz>();

        public void AddQuiz(Quiz quiz)
        {
            quizzes.Add(quiz);
        }

        public List<Quiz> GetAllQuizzes()
        {
            return quizzes;
        }

        public Quiz GetQuizByTitle(string title)
        {
            return quizzes.FirstOrDefault(q => q.Title == title);
        }
    }
}