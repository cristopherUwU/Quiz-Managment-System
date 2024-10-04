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

            Console.WriteLine("Should the quiz allow retakes? (yes/no):");
            bool allowRetake = Console.ReadLine().Equals("yes", StringComparison.OrdinalIgnoreCase);

            Console.WriteLine("Should the quiz randomize questions? (yes/no):");
            bool randomizeQuestions = Console.ReadLine().Equals("yes", StringComparison.OrdinalIgnoreCase);
            
            List<Question> questions = new List<Question>();

            while (true)
            {
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Add new question");
                Console.WriteLine("2. Use a question from the pool");
                Console.WriteLine("3. Finish quiz creation");
        
                string option = Console.ReadLine();

                if (option == "3") break;

                Question question = null;

                switch (option)
                {
                    case "1":
                        question = AddNewQuestion();
                        break;
                    case "2":
                        question = SelectQuestionFromPool();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please select a valid choice.");
                        continue;
                }

                if (question != null)
                {
                    questions.Add(question);
                    Console.WriteLine("Question added!");
                }
            }

            var quiz = new Quiz(title, description, questions, allowRetake, randomizeQuestions);
            _quizService.AddQuiz(quiz);
            Console.WriteLine("Quiz created successfully!");
        }

        private Question AddNewQuestion()
        {
            Console.WriteLine("Select question type:");
            Console.WriteLine("1. Multiple Choice");
            Console.WriteLine("2. True/False");
            Console.WriteLine("3. Short Answer");
            Console.WriteLine("4. Fill in the Blank");

            string typeChoice = Console.ReadLine();
            Question question = null;

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
                case "4":
                    question = CreateFillInTheBlankQuestion();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            return question;
        }
        
        private FillInBlankQuestion CreateFillInTheBlankQuestion()
        {
            Console.WriteLine("Enter the question (with a blank where the user will fill in):");
            string questionText = Console.ReadLine();

            Console.WriteLine("Enter the correct answer:");
            string correctAnswer = Console.ReadLine();

            return new FillInBlankQuestion
            {
                Description = questionText,
                CorrectAnswer = correctAnswer
            };
        }
        
        private Question SelectQuestionFromPool()
        {
            var poolQuestions = _quizService.GetQuestionPool();

            if (poolQuestions.Count == 0)
            {
                Console.WriteLine("The question pool is empty.");
                return null;
            }

            Console.WriteLine("Select a question from the pool:");

            for (int i = 0; i < poolQuestions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {poolQuestions[i].Description}");
            }

            Console.Write("Enter the number of the question you want to use: ");
            if (int.TryParse(Console.ReadLine(), out int selectedIndex) && selectedIndex > 0 && selectedIndex <= poolQuestions.Count)
            {
                return poolQuestions[selectedIndex - 1];
            }

            Console.WriteLine("Invalid selection.");
            return null;
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

            Console.WriteLine("Enter keywords for the correct answer (comma separated):");
            string keywordInput = Console.ReadLine();
            var keywords = keywordInput.Split(',').Select(k => k.Trim()).ToList();

            return new ShortAnswerQuestion
            {
                Description = questionText,
                Keywords = keywords
            };
        }
        
        public void TakeAssignedQuiz(User user)
        {
            if (user.AssignedQuizzes.Count == 0)
            {
                Console.WriteLine("You have no assigned quizzes.");
                return;
            }

            Console.WriteLine("Select a quiz to take:");
            for (int i = 0; i < user.AssignedQuizzes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {user.AssignedQuizzes[i].Title}");
            }

            int quizChoice;
            if (!int.TryParse(Console.ReadLine(), out quizChoice) || quizChoice < 1 || quizChoice > user.AssignedQuizzes.Count)
            {
                Console.WriteLine("Invalid selection. Please try again.");
                return;
            }

            var selectedQuiz = user.AssignedQuizzes[quizChoice - 1];
            TakeQuiz(selectedQuiz, user);
        }

        private void TakeQuiz(Quiz quiz, User user)
        {
            if (!quiz.AllowRetake && quiz.UserAttempts.ContainsKey(user.Nickname) && quiz.UserAttempts[user.Nickname] > 0)
            {
                Console.WriteLine("You cannot retake this quiz.");
                return;
            }

            if (!quiz.UserAttempts.ContainsKey(user.Nickname))
            {
                quiz.UserAttempts[user.Nickname] = 0;
            }
            quiz.UserAttempts[user.Nickname]++;

            Console.WriteLine($"Taking quiz: {quiz.Title}");

            if (quiz.RandomizeQuestions)
            {
                quiz.Questions = quiz.Questions.OrderBy(q => Guid.NewGuid()).ToList(); // Shuffle questions
            }

            var attempt = new QuizAttempt();

            foreach (var question in quiz.Questions)
            {
                Console.WriteLine(question.Description);

                if (question is MultipleChoiceQuestion mcQuestion)
                {
                    for (int i = 0; i < mcQuestion.Options.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {mcQuestion.Options[i]}");
                    }
                    Console.WriteLine("Choose the number of your answer:");
                }

                string userAnswer = Console.ReadLine();

                if (question is MultipleChoiceQuestion multipleChoice)
                {
                    if (int.TryParse(userAnswer, out int userChoice) && userChoice - 1 == multipleChoice.CorrectOption)
                    {
                        attempt.CorrectAnswers++;
                    }
                }
                else if (question.CheckAnswer(userAnswer))
                {
                    attempt.CorrectAnswers++;
                }

                attempt.TotalQuestions++;
            }

            attempt.Score = (int)((double)attempt.CorrectAnswers / attempt.TotalQuestions * 100);
            quiz.Attempts.Add(attempt);

            Console.WriteLine($"Quiz finished! Score: {attempt.Score}, Correct Answers: {attempt.CorrectAnswers}/{attempt.TotalQuestions}, Accuracy: {(attempt.Score / 100.0) * 100}%");
        }
    }
}
