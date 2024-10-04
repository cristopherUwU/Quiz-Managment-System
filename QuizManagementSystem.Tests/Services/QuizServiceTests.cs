using QuizManagementSystem.Models;
using QuizManagementSystem.Services;
using Xunit;
using System.Collections.Generic;

public class QuizServiceTests
{
    private readonly QuizService _quizService;

    public QuizServiceTests()
    {
        _quizService = new QuizService();
    }

    [Fact]
    public void AddQuiz_ShouldAddQuizToList()
    {
        // Arrange
        var quiz = new Quiz(
            "Sample Quiz",
            "This is a sample quiz.",
            new List<Question>(), // Empty list for now
            false,
            true
        );

        // Act
        _quizService.AddQuiz(quiz);

        // Assert
        Assert.Contains(quiz, _quizService.GetAllQuizzes());
    }

    [Fact]
    public void GetQuizByTitle_ShouldReturnCorrectQuiz()
    {
        // Arrange
        var quiz = new Quiz(
            "Unique Quiz Title",
            "This is a unique quiz.",
            new List<Question>(),
            false,
            true
        );

        _quizService.AddQuiz(quiz);

        // Act
        var result = _quizService.GetQuizByTitle("Unique Quiz Title");

        // Assert
        Assert.Equal(quiz, result);
    }

    [Fact]
    public void GetQuizByTitle_ShouldReturnNull_WhenQuizDoesNotExist()
    {
        // Act
        var result = _quizService.GetQuizByTitle("Non-Existent Quiz");

        // Assert
        Assert.Null(result);
    }
}
