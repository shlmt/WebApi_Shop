using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


//using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Repositories
{
    public class UsersRepository : IUsersRepository
    {        
        private WebApiProjectContext _webApiProjectContext;
        private ILogger<UsersRepository> _logger;

        public UsersRepository(WebApiProjectContext webApiProjectContext, ILogger<UsersRepository> logger)
        {
            this._webApiProjectContext = webApiProjectContext;
            _logger = logger;
        }

        public async Task<User> getUserById(int id)
        {
            return await _webApiProjectContext.Users.FindAsync(id);
        }

        public async Task<User> isAuth(LoginUser loginUser)
        {
            _logger.LogInformation("login");
            return await _webApiProjectContext.Users.Where(u => u.Email == loginUser.Email && u.Password == loginUser.Password).FirstOrDefaultAsync();
        }

        public async Task<User> addUser(User user)
        {
            await _webApiProjectContext.Users.AddAsync(user);
            await _webApiProjectContext.SaveChangesAsync();
            return await getUserById(user.Id);
        }


        public async Task<User> update(int id, User updatedUserDetails)
        {
            var user = await getUserById(id);
            if (user == null)
            {
                return null;
            }
            _webApiProjectContext.Entry(user).CurrentValues.SetValues(updatedUserDetails);
            await _webApiProjectContext.SaveChangesAsync();
            return updatedUserDetails;
        }
    }
}
