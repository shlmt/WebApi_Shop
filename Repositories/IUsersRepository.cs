using Entities;
//using project;

namespace Repositories
{
    public interface IUsersRepository
    {

        Task<User> getUserById(int id);
        public  Task<User> isAuth(string email, string password);
        public  Task<User> addUser(User user);
        public  Task<User> update(int id, User updatedUserDetails);

 
    }
}