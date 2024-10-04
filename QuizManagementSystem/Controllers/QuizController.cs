using QuizManagementSystem.Models;
using QuizManagementSystem.Services;

namespace QuizManagementSystem.Controllers
{
    public class QuizController
    {
        private readonly QuizService _quizService;
        private readonly UserService _userService;

        public QuizController(QuizService quizService, UserService userService)
        {
            _quizService = quizService;
            _userService = userService;
        }
        
        public void CreateQuiz()
        {
            Console.WriteLine("Enter quiz title:");
            string title = Console.ReadLine();

            Console.WriteLine("Enter quiz description:");
            string description = Console.ReadLine();

            List<Question> questions = new List<Question>();

            while (true)
            {
                Console.WriteLine("Select question type (1. Multiple Choice, 2. True/False, 3. Short Answer, 4. Exit):");
                string typeChoice = Console.ReadLine();

                if (typeChoice == "4") break;
                Question question;
                switch (typeChoice)
                {
                    case "1":
                        question = CreateMultipleChoiceQuestion();
                        break;
                    case "2":
                        question = CreateTrueFalseQuestion();
                        break;
                    case "3":
                        question = CreateShortAnswerQuestion();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        continue;
                }

                if (question != null)
                {
                    questions.Add(question);
                    Console.WriteLine("Question added!");
                }
            }

            var quiz = new Quiz(title, description, questions);
            _quizService.AddQuiz(quiz);
            Console.WriteLine("Quiz created successfully!");
        }

        public void ViewAllQuizzes()
        {
            var quizzes = _quizService.GetAllQuizzes();
            Console.WriteLine("All Quizzes:");

            if (quizzes.Count == 0)
            {
                Console.WriteLine("No quizzes available.");
                return;
            }

            foreach (var quiz in quizzes)
            {
                Console.WriteLine($"Title: {quiz.Title}, Description: {quiz.Description}, Questions: {quiz.Questions.Count}");
            }
        }

        public void AssignQuizToUser()
        {
            Console.WriteLine("Enter the nickname of the user to assign a quiz to:");
            string userNickname = Console.ReadLine();

            User user = _userService.GetUserByNickname(userNickname);
            if (user == null || user.IsAdmin)
            {
                Console.WriteLine("User not found or is an admin.");
                return;
            }

            Console.WriteLine("Enter the title of the quiz to assign:");
            string quizTitle = Console.ReadLine();

            var quiz = _quizService.GetQuizByTitle(quizTitle);
            if (quiz == null)
            {
                Console.WriteLine("Quiz not found.");
                return;
            }

            user.AssignedQuizzes.Add(quiz);
            Console.WriteLine($"Quiz '{quiz.Title}' assigned to user '{user.Nickname}'.");
        }

        public void ViewAssignedQuizzes(User loggedInUser)
        {
            Console.WriteLine("Assigned Quizzes:");
            if (loggedInUser.AssignedQuizzes.Count == 0)
            {
                Console.WriteLine("No quizzes assigned.");
                return;
            }

            foreach (var quiz in loggedInUser.AssignedQuizzes)
            {
                Console.WriteLine($"- {quiz.Title}: {quiz.Description}");
            }
        }

        private MultipleChoiceQuestion CreateMultipleChoiceQuestion()
        {
            Console.WriteLine("Enter the question:");
            string questionText = Console.ReadLine();

            var mcQuestion = new MultipleChoiceQuestion
            {
                Description = questionText,
                Options = new List<string>()
            };

            Console.WriteLine("Enter number of options:");
            int numberOfOptions = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfOptions; i++)
            {
                Console.WriteLine($"Enter option {i + 1}:");
                mcQuestion.Options.Add(Console.ReadLine());
            }

            Console.WriteLine("Enter the index of the correct option (1-based):");
            mcQuestion.CorrectOption = int.Parse(Console.ReadLine()) - 1;

            return mcQuestion;
        }

        private TrueFalseQuestion CreateTrueFalseQuestion()
        {
            Console.WriteLine("Enter the question:");
            string questionText = Console.ReadLine();

            Console.WriteLine("Is the answer True or False?");
            bool correctAnswer = Console.ReadLine().ToLower() == "true";

            return new TrueFalseQuestion
            {
                Description = questionText,
                CorrectAnswer = correctAnswer
            };
        }

        private ShortAnswerQuestion CreateShortAnswerQuestion()
        {
            Console.WriteLine("Enter the question:");
            string questionText = Console.ReadLine();

            Console.WriteLine("Enter the correct answer:");
            string correctAnswer = Console.ReadLine();

            return new ShortAnswerQuestion
            {
                Description = questionText,
                CorrectAnswer = correctAnswer
            };
        }
    }
}
