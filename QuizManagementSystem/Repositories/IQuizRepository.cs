using QuizManagementSystem.Models;
using System.Collections.Generic;

namespace QuizManagementSystem.Repositories
{
    public interface IQuizRepository
    {
        void AddQuiz(Quiz quiz);
        List<Quiz> GetAllQuizzes();
        Quiz GetQuizByTitle(string title);
    }
}