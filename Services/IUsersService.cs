using Entities;

namespace Services
{
    public interface IUsersService
    {
        Task<User> checkLogin(string email, string password);
        Task<User> getUserById(int id);
        int CheckPasswordStregth(string pass);
        Task<User> createUser(User user);
        Task<User> updateUser(int id, User user);
    }
}