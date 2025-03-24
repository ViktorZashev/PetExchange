using DataLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace PetExchangeTests
{
	public class UserServiceTests : BusinessLayerTestsManagement
	{
        [Test]
        public async Task ReadAllWithFilterAsync_FiltersAndPagesCorrectly()
        {
            // Arrange: Create test users
            var user1 = new User { UserName = "user1", Name = "John Doe", PhoneNumber = "123", Email = "john@example.com", Town = new Town { Name = "TownA" }, Role = RoleEnum.User };
            var user2 = new User { UserName = "user2", Name = "Jane Doe", PhoneNumber = "123", Email = "jane@example.com", Town = new Town { Name = "TownB" }, Role = RoleEnum.Admin };
            var user3 = new User { UserName = "user3", Name = "John Smith", PhoneNumber = "123", Email = "johnsmith@example.com", Town = new Town { Name = "TownA" }, Role = RoleEnum.User };
            var user4 = new User { UserName = "user4", Name = "Jane Smith", PhoneNumber = "123", Email = "janesmith@example.com", Town = new Town { Name = "TownB" }, Role = RoleEnum.Admin };

            // Add users to the in-memory database
            db.Users.Add(user1);
            db.Users.Add(user2);
            db.Users.Add(user3);
            db.Users.Add(user4);
            db.SaveChanges();

            // Act: Call the ReadAllWithFilterAsync method with different filters
            var filteredUsers1 = await _userService.ReadAllWithFilterAsync(username: "user", name: "John", email: "", town: "", role: RoleEnum.User.ToDescriptionString(), page: 1, pageSize: 10);
            var filteredUsers2 = await _userService.ReadAllWithFilterAsync(username: "", name: "", email: "example", town: "TownA", role: "", page: 1, pageSize: 10);
            var filteredUsers3 = await _userService.ReadAllWithFilterAsync(username: "", name: "", email: "", town: "", role: "", page: 2, pageSize: 2); // Test pagination

            // Assert: Check the results
            // filteredUsers1 should contain only user1 and user3 (users with "User" role and name "John")
            Assert.AreEqual(2, filteredUsers1.Count);

            // filteredUsers2 should contain user1, user2, user3, and user4 (users with "example" in email and "TownA" in town)
            Assert.AreEqual(2, filteredUsers2.Count);

            // filteredUsers3 should contain user3 and user4 (pagination should return the second page of results)
            Assert.AreEqual(2, filteredUsers3.Count);
        }

        [Test]
        public async Task UpdateAsync_UpdatesUser()
        {
            // Arrange
            var user = await GetExampleUser();

            var updatedUser = new User { Id = user.Id, UserName = "updateduser", Role = RoleEnum.Admin };

            // Act
            await _userService.UpdateAsync(updatedUser, false);

            // Assert: Check if the user was updated in the database
            var dbUser = await db.Users.SingleOrDefaultAsync(u => u.Id == user.Id);
            Assert.IsNotNull(dbUser);
            Assert.AreEqual("updateduser", dbUser.UserName);
        }

        [Test]
        public async Task ReadAsync_ReturnsCorrectUserById()
        {
            // Arrange: Create a test user
            var user = await GetExampleUser();

            // Act: Retrieve the user by ID
            var dbUser = await _userService.ReadAsync(user.Id);

            // Assert: Check if the retrieved user matches the expected user
            Assert.IsNotNull(dbUser);
            Assert.AreEqual(user.UserName, dbUser.UserName);
        }

        [Test]
        public async Task ReadAllAsync_ReturnsAllUsers()
        {
            // Arrange: Create some test users
            var user1 = await GetExampleUser();
            var user2 = await GetExampleUser();

            // Act: Retrieve all users
            var allUsers = await _userService.ReadAllAsync(false);

            // Assert: Check if all users are returned
            Assert.AreEqual(2, allUsers.Count);
        }
    }
}
