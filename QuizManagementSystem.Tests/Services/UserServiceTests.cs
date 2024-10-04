using QuizManagementSystem.Models;
using QuizManagementSystem.Services;

public class UserServiceTests
{
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService();
    }

    [Fact]
    public void AssignQuizToUser_ShouldAddQuizToUserAssignedQuizzes()
    {
        // Arrange
        var user = new User(
            "cristo",                              // username
            "cristopher@example.com",              // email
            "123",                                 // password
            false                                  // allowQuizRetake
        );

        var quiz = new Quiz(
            "Sample Quiz",                          // title
            "This is a sample quiz.",              // description
            new List<Question>(),                   // questions (empty list to start)
            false,                                  // isRetakeAllowed
            true                                    // randomizeQuestions
        );

        // Act
        user.AssignedQuizzes.Add(quiz);

        // Assert
        Assert.Contains(quiz, user.AssignedQuizzes);
    }

    [Fact]
    public void User_ShouldBeAbleToRetakeQuiz_WhenRetakeAllowedIsTrue()
    {
        // Arrange
        var user = new User(
            "cristo",
            "cristopher@example.com",
            "123",
            true // allowQuizRetake set to true
        );

        var quiz = new Quiz(
            "Sample Quiz",
            "This is a sample quiz.",
            new List<Question>(),
            true, // isRetakeAllowed set to true
            true
        );

        user.AssignedQuizzes.Add(quiz);

        // Assert
        Assert.True(quiz.AllowRetake);
    }

    [Fact]
    public void User_ShouldNotBeAbleToRetakeQuiz_WhenRetakeAllowedIsFalse()
    {
        // Arrange
        var user = new User(
            "cristo",
            "cristopher@example.com",
            "123",
            false // allowQuizRetake set to false
        );

        var quiz = new Quiz(
            "Sample Quiz",
            "This is a sample quiz.",
            new List<Question>(),
            false, // isRetakeAllowed set to false
            true
        );

        user.AssignedQuizzes.Add(quiz);

        // Assert
        Assert.False(quiz.AllowRetake);
    }

    [Fact]
    public void Quiz_ShouldRandomizeQuestions_WhenRandomizeIsTrue()
    {
        // Arrange
        var quiz = new Quiz(
            "Sample Quiz",
            "This is a sample quiz.",
            new List<Question>(), // Add some questions if needed
            true, // randomizeQuestions set to true
            true
        );

        // Assert
        Assert.True(quiz.RandomizeQuestions);
    }

    [Fact]
    public void Quiz_ShouldNotRandomizeQuestions_WhenRandomizeIsFalse()
    {
        // Arrange
        var quiz = new Quiz(
            "Sample Quiz",
            "This is a sample quiz.",
            new List<Question>(), // Add some questions if needed
            false, // randomizeQuestions set to false
            true
        );

        // Assert
        Assert.False(quiz.RandomizeQuestions);
    }
}
