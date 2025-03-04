using DataLayer;

namespace PetExchangeTests
{
	public class UserServiceTests : BusinessLayerTestsManagement
	{
		[Test]
		public async Task Create_Adds_New_User_To_Database()
		{
			// Arrange
			var initialUsersCount = db.Users.Count();
			var town = new Town("Plovidv");
			var user = new User { Id = Guid.NewGuid(), Name = "Test User", Town = town, TownId = town.Id };

			// Act
			await _userService.CreateAsync(user);
			var newUser = db.Users.Find(user.Id);
			var newUsersCount = db.Users.Count();

			// Assert
			
            Assert.Multiple(() =>
            {
                Assert.That(newUser, Is.Not.Null);
                Assert.That(newUser.Id, Is.EqualTo(user.Id));
                Assert.That(newUsersCount, Is.EqualTo(initialUsersCount + 1),
                    "The count of users should increment by 1 after creating a new user.");
            });
        }

		[Test]
		public async Task Read_Returns_Correct_User()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "Test User" };
			db.Users.Add(user);
			db.SaveChanges();

			// Act
			var result = await _userService.ReadAsync(user.Id);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.Id, Is.EqualTo(user.Id));
		}

		[Test]
		public async Task ReadAll_Returns_All_Users()
		{
			// Arrange
			var initialCount = db.Users.Count();

			// Act
			var result = await _userService.ReadAllAsync();

			// Assert
			Assert.That(result, Has.Count.EqualTo(initialCount));
		}

		[Test]
		public async Task Update_Modifies_Existing_User()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "Test User" };
			db.Users.Add(user);
			db.SaveChanges();

			// Act
			user.Name = "Updated User";
            await _userService.UpdateAsync(user);
			var updatedUser = db.Users.Find(user.Id);

			// Assert
			Assert.That(updatedUser.Name, Is.EqualTo("Updated User"));
		}

		[Test]
		public async Task Delete_Removes_User_From_Database()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "Test User" };
			db.Users.Add(user);
			db.SaveChanges();
			var initialUsersCount = db.Users.Count();

            // Act
            await _userService.DeleteAsync(user.Id);
			var newUsersCount = db.Users.Count();
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(newUsersCount, Is.EqualTo(initialUsersCount - 1),
                    "The count of users should decrement by 1 after deleting the user.");
                Assert.That(db.Users.Find(user.Id), Is.Null,
                    "The user should no longer exist in the database after deletion.");
            });
        }
	}
}
