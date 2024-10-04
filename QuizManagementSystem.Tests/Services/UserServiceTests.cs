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
        var user = new User(
            "cristo",
            "cristopher@example.com",
            "123", 
            false
        );

        var quiz = new Quiz(
            "Sample Quiz",
            "This is a sample quiz.",
            new List<Question>(),
            false,
            true                              
        );

        user.AssignedQuizzes.Add(quiz);

        Assert.Contains(quiz, user.AssignedQuizzes);
    }

    [Fact]
    public void User_ShouldBeAbleToRetakeQuiz_WhenRetakeAllowedIsTrue()
    {
        var user = new User(
            "cristo",
            "cristopher@example.com",
            "123",
            true
        );

        var quiz = new Quiz(
            "Sample Quiz",
            "This is a sample quiz.",
            new List<Question>(),
            true,
            true
        );

        user.AssignedQuizzes.Add(quiz);

        Assert.True(quiz.AllowRetake);
    }

    [Fact]
    public void User_ShouldNotBeAbleToRetakeQuiz_WhenRetakeAllowedIsFalse()
    {
        var user = new User(
            "cristo",
            "cristopher@example.com",
            "123",
            false
        );

        var quiz = new Quiz(
            "Sample Quiz",
            "This is a sample quiz.",
            new List<Question>(),
            false,
            true
        );

        user.AssignedQuizzes.Add(quiz);

        Assert.False(quiz.AllowRetake);
    }

    [Fact]
    public void Quiz_ShouldRandomizeQuestions_WhenRandomizeIsTrue()
    {
        var quiz = new Quiz(
            "Sample Quiz",
            "This is a sample quiz.",
            new List<Question>(),
            true,
            true
        );
        
        Assert.True(quiz.RandomizeQuestions);
    }

    [Fact]
    public void Quiz_ShouldNotRandomizeQuestions_WhenRandomizeIsFalse()
    {
        var quiz = new Quiz(
            "Sample Quiz",
            "This is a sample quiz.",
            new List<Question>(),
            false,
            true
        );

        Assert.False(quiz.RandomizeQuestions);
    }
}
