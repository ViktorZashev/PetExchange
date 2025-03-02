using BusinessLayer.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests.DataLayer
{
    public class UserDbContextTests : DataLayerTestsManagement
    {
        [Test]
        public void CreateMethod_AddsUserToDatabase()
        {
            // Arrange
            var initialCount = db.Users.Count();
            var newUser = new User
            {
                Name = "NewUser",
                Username = "newuser",
                Password = "password",
                Town = new ("Plovdiv"),
                Pets = []
            };

            // Act
            userContext.Create(newUser);
            var actualCount = db.Users.Count();
            var expectedCount = initialCount + 1;

            // Assert
            Assert.That(expectedCount, Is.EqualTo(actualCount), "The count of users in the database doesn't increment by 1 when adding one user!");
        }

        [Test]
        public void CreateMethod_DoesNotCreateNewTownWhenAlreadyExists()
        {
            // Arrange
            var existingTown = new Town { Name = "ExistingTown" };
            db.Towns.Add(existingTown);
            db.SaveChanges();
            var initialTownCount = db.Towns.Count();
            var newUser = new User { Name = "NewUser", Username = "newuser", Password = "password", Town = existingTown };

            // Act
            userContext.Create(newUser);
            var actualTownCount = db.Towns.Count();

            // Assert
            Assert.That(actualTownCount, Is.EqualTo(initialTownCount), "Create method creates a new town even when the town already exists in the database!");
        }

        [Test]
        public void CreateMethod_ThrowsExceptionWhenTryingToAddDuplicateUser()
        {
            // Arrange
            var existingUser = new User { Name = "ExistingUser", Username = "existinguser", Password = "password" };
            db.Users.Add(existingUser);
            db.SaveChanges();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => userContext.Create(existingUser), "Create method doesn't throw an exception when trying to add a duplicate user!");
        }

        [Test]
        public void ReadMethod_RetrievesAUserFromDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var enteredUser = new User { Id = id, Name = "UserName", Username = "username", Password = "password" };
            db.Users.Add(enteredUser);
            db.SaveChanges();

            // Act
            var actualUser = userContext.Read(id);
            // Assert
            Assert.Multiple(() =>
            {
              
                Assert.That(actualUser.Id, Is.EqualTo(enteredUser.Id), "Read method doesn't return the user entered in the database!");
                Assert.That(actualUser.Name, Is.EqualTo(enteredUser.Name), "Read method doesn't return the correct user name!");
                Assert.That(actualUser.Username, Is.EqualTo(enteredUser.Username), "Read method doesn't return the correct username!");
                Assert.That(actualUser.Password, Is.EqualTo(enteredUser.Password), "Read method doesn't return the correct password!");
            });
        }

        [Test]
        public void UpdateMethod_UpdatesUserInDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var initialUser = new User { Id = id, Name = "InitialUserName", Username = "initialusername", Password = "password" };
            db.Users.Add(initialUser);
            db.SaveChanges();

            var updatedUser = new User { Id = id, Name = "UpdatedUserName", Username = "updatedusername", Password = "updatedpassword" };

            // Act
            userContext.Update(updatedUser);
            var actualUser = db.Users.FirstOrDefault(u => u.Id == id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualUser.Name, Is.EqualTo(updatedUser.Name), "Update method doesn't update the user name in the database!");
                Assert.That(actualUser.Username, Is.EqualTo(updatedUser.Username), "Update method doesn't update the username in the database!");
                Assert.That(actualUser.Password, Is.EqualTo(updatedUser.Password), "Update method doesn't update the password in the database!");
            });
        }

        [Test]
        public void UpdateMethod_ThrowsExceptionWhenUserDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            var userToUpdate = new User { Id = nonExistentId, Name = "UserName", Username = "username", Password = "password" };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => userContext.Update(userToUpdate), "Update method doesn't throw an exception when the user does not exist in the database!");
        }

        [Test]
        public void DeleteMethod_RemovesUserFromDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userToDelete = new User { Id = id, Name = "UserName", Username = "username", Password = "password" };
            db.Users.Add(userToDelete);
            db.SaveChanges();

            // Act
            userContext.Delete(id);
            var actualUser = db.Users.FirstOrDefault(u => u.Id == id);

            // Assert
            Assert.IsNull(actualUser, "Delete method doesn't remove the user from the database!");
        }

        [Test]
        public void ReadAllMethod_RetrievesAllUsersFromDatabase()
        {
            // Arrange
            var enteredUser1 = new User { Name = "UserName1", Username = "username1", Password = "password1" };
            var enteredUser2 = new User { Name = "UserName2", Username = "username2", Password = "password2" };
            db.Users.Add(enteredUser1);
            db.Users.Add(enteredUser2);
            db.SaveChanges();

            // Act
            var outputedUsers = userContext.ReadAll();

            // Assert
            Assert.That(outputedUsers, Has.Count.EqualTo(2), "ReadAll method doesn't return all entries found in the database!");
            Assert.That(outputedUsers.Any(u => u.Name == "UserName1"), Is.True, "ReadAll method doesn't return the correct user models from the database!");
            Assert.That(outputedUsers.Any(u => u.Name == "UserName2"), Is.True, "ReadAll method doesn't return the correct user models from the database!");
        }

        [Test]
        public void ReadAllMethod_ReturnsEmptyListWhenThereAreNoUsersInDatabase()
        {
            // Arrange
            // Ensure database is empty

            // Act
            var outputedUsers = userContext.ReadAll();

            // Assert
            Assert.That(outputedUsers, Is.Empty, "ReadAll method doesn't return an empty list when no entries exist in the database!");
        }

        [Test]
        public void ReadMethod_ReturnsNullWhenUserDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            Assert.That(userContext.Read(nonExistentId), Is.EqualTo(null), "Read method doesn't return null when the user does not exist in the database!");
        }

        [Test]
        public void DeleteMethod_ThrowsExceptionWhenUserDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => userContext.Delete(nonExistentId), "Delete method doesn't throw an exception when the user does not exist in the database!");
        }

        [Test]
        public void CheckUsernameExistsMethod_ReturnsTrueIfUsernameExists()
        {
            // Arrange
            var username = "ExistingUsername";
            var existingUser = new User { Id = Guid.NewGuid(), Username = username };
            db.Users.Add(existingUser);
            db.SaveChanges();

            // Act
            var usernameExists = userContext.CheckUsernameExists(username);

            // Assert
            Assert.That(usernameExists, Is.True, "CheckUsernameExists method doesn't return true for an existing username in the database!");
        }

        [Test]
        public void CheckUsernameExistsMethod_ReturnsFalseIfUsernameDoesNotExist()
        {
            // Arrange
            var nonExistentUsername = "NonExistentUsername";

            // Act
            var usernameExists = userContext.CheckUsernameExists(nonExistentUsername);

            // Assert
            Assert.That(usernameExists, Is.False, "CheckUsernameExists method doesn't return false for a non-existent username in the database!");
        }

        [Test]
        public void CheckPasswordCorrectMethod_ReturnsTrueIfUsernameAndPasswordMatch()
        {
            // Arrange
            var username = "ExistingUsername";
            var password = "Password123";
            var existingUser = new User { Id = Guid.NewGuid(), Username = username, Password = password };
            db.Users.Add(existingUser);
            db.SaveChanges();

            // Act
            var passwordCorrect = userContext.CheckPasswordCorrect(username, password);

            // Assert
            Assert.That(passwordCorrect, Is.True, "CheckPasswordCorrect method doesn't return true for correct username and password combination!");
        }

        [Test]
        public void CheckPasswordCorrectMethod_ReturnsFalseIfUsernameAndPasswordDoNotMatch()
        {
            // Arrange
            var username = "ExistingUsername";
            var correctPassword = "Password123";
            var incorrectPassword = "WrongPassword";
            var existingUser = new User { Id = Guid.NewGuid(), Username = username, Password = correctPassword };
            db.Users.Add(existingUser);
            db.SaveChanges();

            // Act
            var passwordCorrect = userContext.CheckPasswordCorrect(username, incorrectPassword);

            // Assert
            Assert.That(passwordCorrect, Is.False, "CheckPasswordCorrect method doesn't return false for incorrect username and password combination!");
        }
    }
}
