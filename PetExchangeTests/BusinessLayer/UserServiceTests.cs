using BusinessLayer.Functions;
using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests.BusinessLayer
{
	public class UserServiceTests : BusinessLayerTestsManagement
	{
		private StringWriter consoleOutput;
		private TextWriter originalOutput;

		[SetUp]
		public void RedirectConsoleOutput()
		{
			// Redirect Console.Out to a StringWriter
			consoleOutput = new StringWriter();
			originalOutput = Console.Out;
			Console.SetOut(consoleOutput);
		}

		[TearDown]
		public void RestoreConsoleOutput()
		{
			// Restore Console.Out to its original stream
			Console.SetOut(originalOutput);
			consoleOutput.Dispose();
		}

		[Test]
		public void Create_Adds_New_User_To_Database()
		{
			// Arrange
			var initialUsersCount = db.Users.Count();
			var town = new Town("Plovidv");
			var user = new User { Id = Guid.NewGuid(), Name = "Test User", Town = town, TownId = town.Id };

			// Act
			UserService.Create(user);
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
		public void Create_List_Adds_New_Users_To_Database()
		{
			// Arrange
			var newTown = new Town("Plovdiv");

			var initialUsersCount = db.Users.Count();
			var users = new List<User>
				{
					new() { Id = Guid.NewGuid(), Name = "Test User 1", Town = newTown, TownId = newTown.Id},
					new() { Id = Guid.NewGuid(), Name = "Test User 2", Town = newTown, TownId = newTown.Id},
					new() { Id = Guid.NewGuid(), Name = "Test User 3", Town = newTown, TownId = newTown.Id}
				};

			// Act
			UserService.Create(users);
			var newUsersCount = db.Users.Count();

			// Assert
			Assert.That(newUsersCount, Is.EqualTo(initialUsersCount + users.Count),
				"The count of users should increment by the number of users added.");
		}

		[Test]
		public void Read_Returns_Correct_User()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "Test User" };
			db.Users.Add(user);
			db.SaveChanges();

			// Act
			var result = UserService.Read(user.Id);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.Id, Is.EqualTo(user.Id));
		}

		[Test]
		public void ReadAll_Returns_All_Users()
		{
			// Arrange
			var initialCount = db.Users.Count();

			// Act
			var result = UserService.ReadAll();

			// Assert
			Assert.That(result, Has.Count.EqualTo(initialCount));
		}

		[Test]
		public void Update_Modifies_Existing_User()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "Test User" };
			db.Users.Add(user);
			db.SaveChanges();

			// Act
			user.Name = "Updated User";
			UserService.Update(user);
			var updatedUser = db.Users.Find(user.Id);

			// Assert
			Assert.That(updatedUser.Name, Is.EqualTo("Updated User"));
		}

		[Test]
		public void Delete_Removes_User_From_Database()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "Test User" };
			db.Users.Add(user);
			db.SaveChanges();
			var initialUsersCount = db.Users.Count();

			// Act
			UserService.Delete(user.Id);
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

		[Test]
		public void DeleteAll_Removes_All_Users_From_Database()
		{
			// Arrange
			var users = new List<User>
		{
			new User { Id = Guid.NewGuid(), Name = "Test User 1" },
			new User { Id = Guid.NewGuid(), Name = "Test User 2" }
		};
			db.Users.AddRange(users);
			db.SaveChanges();

			// Act
			UserService.DeleteAll();
			var newUsersCount = db.Users.Count();

			// Assert
			Assert.That(newUsersCount, Is.EqualTo(0),
				"All users should be removed from the database.");
		}

		[Test]
		public void AuthenticateUserReturnsCode_Returns_Correct_Code()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Username = "testuser", Password = "password" };
			db.Users.Add(user);
			db.SaveChanges();
            // Act & Assert
            Assert.Multiple(() =>
            {
                
                Assert.That(UserService.AuthenticateUserReturnsCode("testuser", "password"), Is.EqualTo(2),
                    "Authentication should be successful with correct username and password.");
                Assert.That(UserService.AuthenticateUserReturnsCode("testuser", "wrongpassword"), Is.EqualTo(1),
                    "Authentication should fail with incorrect password.");
                Assert.That(UserService.AuthenticateUserReturnsCode("nonexistentuser", "password"), Is.EqualTo(0),
                    "Authentication should fail with non-existent username.");
            });
        }

		[Test]
		public void ReturnUser_Returns_Correct_User()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Username = "testuser", Password = "password" };
			db.Users.Add(user);
			db.SaveChanges();

			// Act
			var result = UserService.ReturnUser("testuser", "password");

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.Id, Is.EqualTo(user.Id));
		}
	}
}
