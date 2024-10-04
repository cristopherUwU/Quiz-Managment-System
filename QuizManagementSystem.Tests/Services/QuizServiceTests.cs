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
        var quiz = new Quiz(
            "Sample Quiz",
            "This is a sample quiz.",
            new List<Question>(),
            false,
            true
        );

        _quizService.AddQuiz(quiz);

        Assert.Contains(quiz, _quizService.GetAllQuizzes());
    }

    [Fact]
    public void GetQuizByTitle_ShouldReturnCorrectQuiz()
    {
        var quiz = new Quiz(
            "Unique Quiz Title",
            "This is a unique quiz.",
            new List<Question>(),
            false,
            true
        );

        _quizService.AddQuiz(quiz);

        var result = _quizService.GetQuizByTitle("Unique Quiz Title");

        Assert.Equal(quiz, result);
    }

    [Fact]
    public void GetQuizByTitle_ShouldReturnNull_WhenQuizDoesNotExist()
    {
        var result = _quizService.GetQuizByTitle("Non-Existent Quiz");

        Assert.Null(result);
    }
}
