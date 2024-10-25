using Models;
namespace Services
{
    public interface IUseService
    {
        public  User GetUserById(string userName);
        public Task<User> AddUser(User user); //? adding new employee
        public Task<User> UpdateUser(User user);
        public Task<string> DeleteUser(User user);
        public int? SignInQuery(string userEmail, string password);
    }
}