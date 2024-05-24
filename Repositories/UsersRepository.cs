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

        public UsersRepository(WebApiProjectContext webApiProjectContext)
        {
            this._webApiProjectContext = webApiProjectContext;
        }

        public async Task<User> getUserById(int id)
        {
            return await _webApiProjectContext.Users.FindAsync(id);
        }

        public async Task<User> isAuth(string email,string password)
        {
            return await _webApiProjectContext.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
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
