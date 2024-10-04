using QuizManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizManagementSystem.Services
{
    public class QuizService
    {
        private List<Quiz> _quizzes = new List<Quiz>();
        private List<Question> _questionPool;

        public QuizService()
        {
            _questionPool = new List<Question>
            {
                new FillInBlankQuestion { Description = "What's your name?"},
                new FillInBlankQuestion { Description = "What's your lastname?"},
                new FillInBlankQuestion { Description = "What's your email?"},
                new FillInBlankQuestion { Description = "What's your number?"}
            };
        }

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

        public List<Question> GetQuestionPool()
        {
            return _questionPool;
        }
    }
}