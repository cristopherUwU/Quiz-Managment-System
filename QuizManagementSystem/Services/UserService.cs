using QuizManagementSystem.Models;

namespace QuizManagementSystem.Services
{
    public class UserService
    {
        private List<User> _users = new List<User>();

        public void RegisterUser(string nickname, string name, string password, bool isAdmin)
        {
            var user = new User(nickname, name, password, isAdmin);
            _users.Add(user);
        }

        public User Login(string nickname, string password)
        {
            return _users.FirstOrDefault(u => u.Nickname == nickname && u.Password == password);
        }

        public User GetUserByNickname(string nickname)
        {
            return _users.FirstOrDefault(u => u.Nickname == nickname);
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }
    }
}