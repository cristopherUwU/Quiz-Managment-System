using System.Collections.Generic;

namespace QuizManagementSystem.Models
{
    public class User
    {
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<Quiz> AssignedQuizzes { get; set; } = new List<Quiz>();

        public User(string nickname, string name, string password, bool isAdmin)
        {
            Nickname = nickname;
            Name = name;
            Password = password;
            IsAdmin = isAdmin;
        }
    }
}