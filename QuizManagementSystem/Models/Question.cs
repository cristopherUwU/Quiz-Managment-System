namespace QuizManagementSystem.Models
{
    public abstract class Question
    {
        public string Description { get; set; }
        public abstract bool CheckAnswer(string answer);
    }
}