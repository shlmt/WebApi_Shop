using Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject;

namespace Test
{
    public class UserRepositoryIntegrationTesting : IClassFixture<DatabaseFixture>
    {
        private readonly WebApiProjectContext _dbContext;
        private readonly UsersRepository _usersRepository;

        public UserRepositoryIntegrationTesting(DatabaseFixture databaseFixture)
        {
            _dbContext = databaseFixture.Context;
            _usersRepository = new UsersRepository(_dbContext);
        }

        [Fact]
        public async Task GetUser_ValidCredentials_ReturnUser()
        {
            //Arrange
            var email = "test@example.com";
            var password = "password";
            var user = new User { Email = email, Password = password, FirstName = "firstname", LastName = "lastName" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _usersRepository.isAuth(email, password);

            //Assert
            Assert.NotNull(result);
        }

    }
    

}
