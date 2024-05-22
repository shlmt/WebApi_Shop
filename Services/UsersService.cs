using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Repositories;
using Zxcvbn;

namespace Services
{
    public class UsersService : IUsersService
    {
        
        private IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            this._usersRepository = usersRepository;
        }
        public async Task<User> getUserById(int id)
        {
            return await _usersRepository.getUserById(id);
        }

        public async Task<User> checkLogin(LoginUser loginUser)
        {
            return await _usersRepository.isAuth(loginUser);
        }
        public async Task<User> createUser(User user)
        {
            if (CheckPasswordStregth(user.Password) <= 2)
                return null;
            return  await _usersRepository.addUser(user);
        }

        public async Task<User> updateUser(int id, User user)
        {
            if (CheckPasswordStregth(user.Password) <= 2)
                return null;
            return await _usersRepository.update(id,user); ;
        }

        public int CheckPasswordStregth(string pass)
        {
            var result = Zxcvbn.Core.EvaluatePassword(pass);
            return result.Score;
        }

    }
}
