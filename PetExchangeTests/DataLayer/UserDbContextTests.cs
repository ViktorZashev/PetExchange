﻿using DataLayer;

namespace PetExchangeTests
{
    public class UserDbContextTests : DataLayerTestsManagement
    {
        [Test]
        public async Task CreateMethod_AddsUserToDatabase()
        {
            // Arrange
            var initialCount = db.Users.Count();
            var newUser = new User
            {
                Name = "NewUser",
                UserName = "newuser",
                Town = new ("Plovdiv"),
                Pets = []
            };

            // Act
            await userContext.CreateAsync(newUser);
            var actualCount = db.Users.Count();
            var expectedCount = initialCount + 1;

            // Assert
            Assert.That(expectedCount, Is.EqualTo(actualCount), "The count of users in the database doesn't increment by 1 when adding one user!");
        }

        [Test]
        public async Task CreateMethod_DoesNotCreateNewTownWhenAlreadyExists()
        {
            // Arrange
            var existingTown = new Town { Name = "ExistingTown" };
            db.Towns.Add(existingTown);
            db.SaveChanges();
            var initialTownCount = db.Towns.Count();
            var newUser = new User { Name = "NewUser", UserName = "newuser", Town = existingTown };

            // Act
            await userContext.CreateAsync(newUser);
            var actualTownCount = db.Towns.Count();

            // Assert
            Assert.That(actualTownCount, Is.EqualTo(initialTownCount), "Create method creates a new town even when the town already exists in the database!");
        }

        [Test]
        public async Task CreateMethod_ThrowsExceptionWhenTryingToAddDuplicateUser()
        {
            // Arrange
            var existingUser = new User { Name = "ExistingUser", UserName = "existinguser" };
            db.Users.Add(existingUser);
            db.SaveChanges();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => userContext.CreateAsync(existingUser), "Create method doesn't throw an exception when trying to add a duplicate user!");
        }

        [Test]
        public async Task ReadMethod_RetrievesAUserFromDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var enteredUser = new User { Id = id, Name = "UserName", UserName = "username"};
            db.Users.Add(enteredUser);
            db.SaveChanges();

            // Act
            var actualUser = await userContext.ReadAsync(id);
            // Assert
            Assert.Multiple(() =>
            {
              
                Assert.That(actualUser.Id, Is.EqualTo(enteredUser.Id), "Read method doesn't return the user entered in the database!");
                Assert.That(actualUser.Name, Is.EqualTo(enteredUser.Name), "Read method doesn't return the correct user name!");
                Assert.That(actualUser.UserName, Is.EqualTo(enteredUser.UserName), "Read method doesn't return the correct username!");
            });
        }

        [Test]
        public async Task UpdateMethod_UpdatesUserInDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var initialUser = new User { Id = id, Name = "InitialUserName", UserName = "initialusername" };
            db.Users.Add(initialUser);
            db.SaveChanges();

            var updatedUser = new User { Id = id, Name = "UpdatedUserName", UserName = "updatedusername"};

            // Act
            await userContext.UpdateAsync(updatedUser);
            var actualUser = db.Users.FirstOrDefault(u => u.Id == id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualUser.Name, Is.EqualTo(updatedUser.Name), "Update method doesn't update the user name in the database!");
                Assert.That(actualUser.UserName, Is.EqualTo(updatedUser.UserName), "Update method doesn't update the username in the database!");
            });
        }

        [Test]
        public async Task UpdateMethod_ThrowsExceptionWhenUserDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            var userToUpdate = new User { Id = nonExistentId, Name = "UserName", UserName = "username"};

            // Act & Assert
            Assert.Throws<ArgumentException>(() => userContext.UpdateAsync(userToUpdate), "Update method doesn't throw an exception when the user does not exist in the database!");
        }

        [Test]
        public async Task DeleteMethod_RemovesUserFromDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userToDelete = new User { Id = id, Name = "UserName", UserName = "username"};
            db.Users.Add(userToDelete);
            db.SaveChanges();

            // Act
            await userContext.DeleteAsync(id);
            var actualUser = db.Users.FirstOrDefault(u => u.Id == id);

            // Assert
            Assert.IsNull(actualUser, "Delete method doesn't remove the user from the database!");
        }

        [Test]
        public async Task ReadAllMethod_RetrievesAllUsersFromDatabase()
        {
            // Arrange
            var enteredUser1 = new User { Name = "UserName1", UserName = "username1"};
            var enteredUser2 = new User { Name = "UserName2", UserName = "username2"};
            db.Users.Add(enteredUser1);
            db.Users.Add(enteredUser2);
            db.SaveChanges();

            // Act
            var outputedUsers = await userContext.ReadAllAsync();

            // Assert
            Assert.That(outputedUsers, Has.Count.EqualTo(2), "ReadAll method doesn't return all entries found in the database!");
            Assert.That(outputedUsers.Any(u => u.Name == "UserName1"), Is.True, "ReadAll method doesn't return the correct user models from the database!");
            Assert.That(outputedUsers.Any(u => u.Name == "UserName2"), Is.True, "ReadAll method doesn't return the correct user models from the database!");
        }

        [Test]
        public async Task ReadAllMethod_ReturnsEmptyListWhenThereAreNoUsersInDatabase()
        {
            // Arrange
            // Ensure database is empty

            // Act
            var outputedUsers = await userContext.ReadAllAsync();

            // Assert
            Assert.That(outputedUsers, Is.Empty, "ReadAll method doesn't return an empty list when no entries exist in the database!");
        }

        [Test]
        public async Task ReadMethod_ReturnsNullWhenUserDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            Assert.That(await userContext.ReadAsync(nonExistentId), Is.EqualTo(null), "Read method doesn't return null when the user does not exist in the database!");
        }

        [Test]
        public async Task DeleteMethod_ThrowsExceptionWhenUserDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => userContext.DeleteAsync(nonExistentId), "Delete method doesn't throw an exception when the user does not exist in the database!");
        }
    }
}
