﻿using BusinessLayer.Functions;
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
		public void CreateRequest_Prints_CantRequestOwnPetMessage()
		{
			// Arrange
			var townId = Guid.NewGuid();
			var town = new Town(new Country("Bulgaria"), "Plovdiv");
			town.Id = townId;
			var user = new User(town, new List<Pet>(), "Bobo", "", false, "", "bobcho", "123");
			var pet = new Pet { User = user, UserId = user.Id, Id = Guid.NewGuid(), Name = "Register Pet" };
			var offer = new PublicOffer { Id = Guid.NewGuid(), Pet = pet, PetId = pet.Id, TownId = town.Id };
			db.Users.Add(user);
			db.Towns.Add(town);
			db.Pets.Add(pet);
			db.PublicOffers.Add(offer);
			db.SaveChanges();

			// Act & Assert
			Assert.Throws<Exception>(() => UserRequestsService.CreateRequest(user, pet.Name),"Create Request doesn't throw exception when trying to request own pet!");
			StringAssert.Contains("You can't create a request for your own pet! Try again.!", consoleOutput.ToString());
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
			var request = new UserRequests(offer, user, false);
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
				new PublicOffer { Id = Guid.NewGuid() },
				new PublicOffer { Id = Guid.NewGuid() }
			};
			db.Users.Add(user);
			db.PublicOffers.AddRange(offers);
			db.SaveChanges();
			var initialRequestsCount = db.Requests.Count();

			// Act
			var requests = offers.Select(offer => new UserRequests(offer, user, false)).ToList();
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
			var request = new UserRequests(offer, user, false) { Id = Guid.NewGuid() };
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
			Assert.That(result.Count, Is.EqualTo(initialCount));
		}

		[Test]
		public void ReadAll_ReturnsEmptyListWhenUserIsNull()
		{
			// Arrange
			

			// Act & Assert
			Assert.IsEmpty(UserRequestsService.ReadAll(user: null),"ReadAll method doesn't throw an exception when the user is null!");
		}

		[Test]
		public void Update_Modifies_Existing_UserRequest()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var offer = new PublicOffer { Id = Guid.NewGuid() };
			var request = new UserRequests(offer, user, false);
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
			var request = new UserRequests(offer, user, false);
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
				new PublicOffer { Id = Guid.NewGuid() },
				new PublicOffer { Id = Guid.NewGuid() }
			};
			var requests = offers.Select(offer => new UserRequests(offer, user, false)).ToList();
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
			var request1 = new UserRequests(offer1, user, false);
			var request2 = new UserRequests(offer2, otherUser, false);
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
			Assert.That(result.Count, Is.EqualTo(1));
			Assert.That(result[0].UserId, Is.EqualTo(user.Id));
		}

		[Test]
		public void DeleteRequest_Removes_Correct_UserRequest()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid(), Name = "Pet To Delete" };
			var offer = new PublicOffer { Id = Guid.NewGuid(), Pet = pet, PetId = pet.Id };
			var request = new UserRequests(offer, user, false);
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
			var pet = new Pet { Id = Guid.NewGuid(), Name = "Test Pet" };
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
		public void CreateRequest_Adds_New_UserRequest_For_Pet()
		{
			// Arrange
			var townId = Guid.NewGuid();
			var town = new Town(new Country("Bulgaria"), "Plovdiv");
			town.Id = townId;
			var user = new User(town,new List<Pet>(), "Bobo","",false,"","bobcho","123");
			user.TownId = townId;
			var pet = new Pet { User = user,UserId = user.Id, Id = Guid.NewGuid(), Name = "Register Pet" };
			var offer = new PublicOffer { Id = Guid.NewGuid(), Pet = pet, PetId = pet.Id, TownId = town.Id };
			db.Towns.Add(town);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.PublicOffers.Add(offer);
			db.SaveChanges();
			var initialRequestsCount = db.Requests.Count();
			var differentUser = new User();
			differentUser.Town = town;
			differentUser.TownId = townId;
			// Act

			UserRequestsService.CreateRequest(differentUser, pet.Name);
			var newRequestsCount = db.Requests.Count();

			// Assert
			Assert.That(newRequestsCount, Is.EqualTo(initialRequestsCount + 1), "A new user request should be added for the requested pet.");
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
