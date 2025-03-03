using BusinessLayer.Functions;
using BusinessLayer.Models;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests.BusinessLayer
{
	public class UserRequestsServiceTests : BusinessLayerTestsManagement
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
		public void Create_Adds_New_UserRequest_To_Database()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var offer = new PublicOffer { Id = Guid.NewGuid() };
			db.Users.Add(user);
			db.PublicOffers.Add(offer);
			db.SaveChanges();
			var initialRequestsCount = db.Requests.Count();

			// Act
			var request = new UserRequest(offer, false);
			UserRequestsService.Create(request);
			var newRequestsCount = db.Requests.Count();

			// Assert
			Assert.That(newRequestsCount, Is.EqualTo(initialRequestsCount + 1), "The count of user requests should increment by 1 after creating a new request.");
		}

		[Test]
		public void Create_List_Adds_New_UserRequests_To_Database()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var offers = new List<PublicOffer>
			{
				new() { Id = Guid.NewGuid() },
				new() { Id = Guid.NewGuid() }
			};
			db.Users.Add(user);
			db.PublicOffers.AddRange(offers);
			db.SaveChanges();
			var initialRequestsCount = db.Requests.Count();

			// Act
			var requests = offers.Select(offer => new UserRequest(offer, false)).ToList();
			UserRequestsService.Create(requests);
			var newRequestsCount = db.Requests.Count();

			// Assert
			Assert.That(newRequestsCount, Is.EqualTo(initialRequestsCount + requests.Count), "The count of user requests should increment by the number of requests added.");
		}

		[Test]
		public void Read_Returns_Correct_UserRequest()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var offer = new PublicOffer { Id = Guid.NewGuid() };
			var request = new UserRequest(offer, false) { Id = Guid.NewGuid() };
			db.Users.Add(user);
			db.PublicOffers.Add(offer);
			db.Requests.Add(request);
			db.SaveChanges();

			// Act
			var result = UserRequestsService.Read(request.Id);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.Id, Is.EqualTo(request.Id));
		}

		[Test]
		public void ReadAll_Returns_All_UserRequests()
		{
			// Arrange
			var initialCount = db.Requests.Count();

			// Act
			var result = UserRequestsService.ReadAll();

			// Assert
			Assert.That(result, Has.Count.EqualTo(initialCount));
		}

		[Test]
		public void ReadAll_ReturnsEmptyListWhenUserIsNull()
		{
			// Arrange
			

			// Act & Assert
			Assert.That(UserRequestsService.ReadAll(user: null), Is.Empty, "ReadAll method doesn't throw an exception when the user is null!");
		}

		[Test]
		public void Update_Modifies_Existing_UserRequest()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var offer = new PublicOffer { Id = Guid.NewGuid() };
			var request = new UserRequest(offer, false);
			db.Users.Add(user);
			db.PublicOffers.Add(offer);
			db.Requests.Add(request);
			db.SaveChanges();

			// Act
			request.IsAccepted = true;
			UserRequestsService.Update(request);
			var updatedRequest = db.Requests.Find(request.Id);

			// Assert
			Assert.That(updatedRequest.IsAccepted, Is.True);
		}

		[Test]
		public void Delete_Removes_UserRequest_From_Database()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var offer = new PublicOffer { Id = Guid.NewGuid() };
			var request = new UserRequest(offer, false);
			db.Users.Add(user);
			db.PublicOffers.Add(offer);
			db.Requests.Add(request);
			db.SaveChanges();
			var initialRequestsCount = db.Requests.Count();

			// Act
			UserRequestsService.Delete(request.Id);
			var newRequestsCount = db.Requests.Count();

			// Assert
			Assert.That(newRequestsCount, Is.EqualTo(initialRequestsCount - 1), "The count of user requests should decrement by 1 after deleting the request.");
		}

		[Test]
		public void DeleteAll_Removes_All_UserRequests_From_Database()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var offers = new List<PublicOffer>
			{
				new() { Id = Guid.NewGuid() },
				new() { Id = Guid.NewGuid() }
			};
			var requests = offers.Select(offer => new UserRequest(offer, false)).ToList();
			db.Users.Add(user);
			db.PublicOffers.AddRange(offers);
			db.Requests.AddRange(requests);
			db.SaveChanges();
			var initialRequestsCount = db.Requests.Count();

			// Act
			UserRequestsService.DeleteAll();
			var newRequestsCount = db.Requests.Count();

			// Assert
			Assert.That(newRequestsCount, Is.EqualTo(0), "All user requests should be removed from the database.");
		}

		[Test]
		public void ReadAll_ByUser_Returns_Correct_UserRequests()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var otherUser = new User { Id = Guid.NewGuid() };
			var offer1 = new PublicOffer { Id = Guid.NewGuid() };
			var offer2 = new PublicOffer { Id = Guid.NewGuid() };
			var request1 = new UserRequest(offer1, false);
			var request2 = new UserRequest(offer2, false);
			db.Users.Add(user);
			db.Users.Add(otherUser);
			db.PublicOffers.Add(offer1);
			db.PublicOffers.Add(offer2);
			db.Requests.Add(request1);
			db.Requests.Add(request2);
			db.SaveChanges();

			// Act
			var result = UserRequestsService.ReadAll(user);

			// Assert
			Assert.That(result, Has.Count.EqualTo(1));
		}

		[Test]
		public void DeleteRequest_Removes_Correct_UserRequest()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new User { Id = Guid.NewGuid(), Name = "Pet To Delete" };
			var offer = new PublicOffer { Id = Guid.NewGuid(), Pet = pet, PetId = pet.Id };
			var request = new UserRequest(offer, false);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.PublicOffers.Add(offer);
			db.Requests.Add(request);
			db.SaveChanges();
			var initialRequestsCount = db.Requests.Count();

			// Act
			UserRequestsService.DeleteRequest(user, pet.Name);
			var newRequestsCount = db.Requests.Count();

			// Assert
			Assert.That(newRequestsCount, Is.EqualTo(initialRequestsCount - 1), "The count of user requests should decrement by 1 after deleting the request.");
		}

		[Test]
		public void DeleteRequest_Throws_Exception_When_No_Request_Exists_For_User()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new User { Id = Guid.NewGuid(), Name = "Test Pet" };
			var offer = new PublicOffer { Id = Guid.NewGuid(), Pet = pet, PetId = pet.Id };
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.PublicOffers.Add(offer);
			db.SaveChanges();

			// Act & Assert
			var ex = Assert.Throws<Exception>(() => UserRequestsService.DeleteRequest(user, pet.Name));
			Assert.That(ex.Message, Is.EqualTo("No such request exists for this user!"));
		}

		[Test]
		public void CreateRequest_Throws_Exception_When_Public_Offer_Does_Not_Exist()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), TownId = Guid.NewGuid() };
			var petName = "Non-Existent Pet";

			// Act & Assert
			var ex = Assert.Throws<Exception>(() => UserRequestsService.CreateRequest(user, petName));
			Assert.That(ex.Message, Is.EqualTo("No such public offer exists for petName"));
		}
	}
}
