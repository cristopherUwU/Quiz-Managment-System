using QuizManagementSystem.Models;

namespace QuizManagementSystem.Repositories
{
    public class UserRepository
    {
        private List<User> users = new List<User>();

        public void AddUser(User user)
        {
            users.Add(user);
        }

        public User GetUserByNickname(string nickname)
        {
            return users.FirstOrDefault(u => u.Nickname.Equals(nickname, StringComparison.OrdinalIgnoreCase));
        }

        public List<User> GetAllUsers()
        {
            return users;
        }
    }
}