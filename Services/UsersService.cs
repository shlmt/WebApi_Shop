using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zxcvbn;

namespace Services
{
    public class UsersService : IUsersService
    {
        
        private IUsersRepository _usersRepository;
        private IConfiguration _configuration;

        public UsersService(IUsersRepository usersRepository, IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _configuration = configuration;

        }
        public async Task<User> getUserById(int id)
        {
            return await _usersRepository.getUserById(id);
        }

        public async Task<User?> checkLogin(string email, string password)
        {
            var user = await _usersRepository.isAuth(email, password);
            if (user == null)
                return null;
            user.Token = generateJwtToken(user);
            return user;
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

        private string generateJwtToken(User user)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("key").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                   // new Claim("roleId", 7.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
