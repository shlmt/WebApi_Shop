using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

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
        public async Task isAuth_ValidCredentials_ReturnUser()
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

        [Fact]
        public async Task GetUserById_ShouldReturnCorrectUser()
        {
            // Arrange: הוספת נתונים למסד הנתונים מתוך ה-Fixture
            var testUser = new User { FirstName = "FirstName", LastName = "lastName", Email = "email@example.com", Password = "pass" };
            await _dbContext.Users.AddAsync(testUser);
            await _dbContext.SaveChangesAsync();
            var userId = testUser.Id;

            // Act: קריאה לפונקציה getUserById
            var user = await _usersRepository.getUserById(userId);

            // Assert: בדיקה אם התוצאה נכונה
            Assert.NotNull(user);
            Assert.NotEqual(0,userId);
            Assert.Equal(userId, user.Id);
            Assert.Equal(user.Email,testUser.Email);
        }

        [Fact]
        public async Task AddUser_ShouldAddAndReturnCorrectUser()
        {
            // Arrange
            var newUser = new User { FirstName = "FirstName", LastName = "lastName", Email = "email@example.com", Password = "pass" };

            // Act
            var addedUser = await _usersRepository.addUser(newUser);

            // Assert
            Assert.NotNull(addedUser);
            Assert.Equal(newUser.Id, addedUser.Id);
            Assert.Equal("FirstName", addedUser.FirstName);

            var userFromDb = await _dbContext.Users.FindAsync(newUser.Id);
            Assert.NotNull(userFromDb);
            Assert.Equal("lastName", userFromDb.LastName);
        }

        [Fact]
        public async Task UpdateUser_ValidUser_UpdatesUser()
        {
            //Arrange
            var user = new User { Email = "email@example.com", Password = "password", FirstName = "FirstName", LastName = "LastName" };
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            var updatedUser = new User { Email = "updated@example.com", Password = "newpassword", FirstName = "UpdatedName", LastName = "UpdatedLastName" };
            // Attach the existing user to the context before updating
            _dbContext.Entry(user).State = EntityState.Detached;
            updatedUser.Id = user.Id; // Ensure the IDs match
            //Act
            var result = await _usersRepository.update(user.Id, updatedUser);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("updated@example.com", result.Email);
        }
    }

}



    


