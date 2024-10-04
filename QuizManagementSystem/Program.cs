using QuizManagementSystem.Services;
using QuizManagementSystem.Controllers;
using QuizManagementSystem.Models;

namespace QuizManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            UserService userService = new UserService();
            QuizService quizService = new QuizService();
            QuizController quizController = new QuizController(quizService, userService);

            bool running = true;

            while (running)
            {
                Console.WriteLine("Welcome to the Quiz Management System!");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        RegisterUser(userService);
                        break;

                    case "2":
                        var loggedInUser = LoginUser(userService);
                        if (loggedInUser != null)
                        {
                            if (loggedInUser.IsAdmin)
                            {
                                AdminMenu(quizController, loggedInUser, userService);
                            }
                            else
                            {
                                UserMenu(quizController, loggedInUser);
                            }
                        }
                        break;

                    case "3":
                        running = false;
                        Console.WriteLine("Exiting the program. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        static void RegisterUser(UserService userService)
        {
            Console.Write("Enter nickname: ");
            string nickname = Console.ReadLine();

            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            Console.Write("Do you want to register as admin? (yes/no): ");
            string isAdminInput = Console.ReadLine();
            bool isAdmin = isAdminInput.Equals("yes", StringComparison.OrdinalIgnoreCase);

            userService.RegisterUser(nickname, name, password, isAdmin);
            Console.WriteLine("User registered successfully!");
        }

        static User LoginUser(UserService userService)
        {
            Console.Write("Enter nickname: ");
            string nickname = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            var user = userService.Login(nickname, password);
            if (user != null)
            {
                Console.WriteLine($"Welcome, {user.Name}!");
                return user;
            }
            else
            {
                Console.WriteLine("Invalid nickname or password.");
                return null;
            }
        }

        static void AdminMenu(QuizController quizController, User loggedInUser, UserService userService)
        {
            bool adminMenuRunning = true;

            while (adminMenuRunning)
            {
                Console.WriteLine("\nAdmin Menu:");
                Console.WriteLine("1. Create Quiz");
                Console.WriteLine("2. View All Quizzes");
                Console.WriteLine("3. Assign Quiz to User");
                Console.WriteLine("4. Logout");
                Console.Write("Select an option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        quizController.CreateQuiz();
                        break;

                    case "2":
                        quizController.ViewAllQuizzes();
                        break;

                    case "3":
                        quizController.AssignQuizToUser();
                        break;

                    case "4":
                        adminMenuRunning = false;
                        Console.WriteLine("Logging out...");
                        break;

                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        static void UserMenu(QuizController quizController, User loggedInUser)
        {
            bool userMenuRunning = true;

            while (userMenuRunning)
            {
                Console.WriteLine("\nUser Menu:");
                Console.WriteLine("1. View Assigned Quizzes");
                Console.WriteLine("2. Take Assigned Quiz");
                Console.WriteLine("3. Logout");
                Console.Write("Select an option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        quizController.ViewAssignedQuizzes(loggedInUser);
                        break;

                    case "2":
                        quizController.TakeAssignedQuiz(loggedInUser);
                        break;

                    case "3":
                        userMenuRunning = false;
                        Console.WriteLine("Logging out...");
                        break;

                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }
    }
}
