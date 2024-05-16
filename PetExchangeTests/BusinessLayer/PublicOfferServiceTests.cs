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
    internal class PublicOfferServiceTests : BusinessLayerTestsManagement
    {
		[Test]
		public void Create_Adds_New_PublicOffer_To_Database()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid(), Name = "New Pet", User = user, UserId = user.Id };
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();
			var initialOffersCount = db.PublicOffers.Count();

			// Act
			PublicOfferService.Create(new PublicOffer(pet));
			var newOffersCount = db.PublicOffers.Count();

			// Assert
			Assert.That(newOffersCount, Is.EqualTo(initialOffersCount + 1), "The count of public offers should increment by 1 after creating a new offer.");
		}

		[Test]
		public void Create_List_Adds_New_PublicOffers_To_Database()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pets = new List<Pet>
			{
				new Pet { Id = Guid.NewGuid(), Name = "New Pet 1", User = user, UserId = user.Id },
				new Pet { Id = Guid.NewGuid(), Name = "New Pet 2", User = user, UserId = user.Id }
			};
			db.Users.Add(user);
			db.Pets.AddRange(pets);
			db.SaveChanges();
			var initialOffersCount = db.PublicOffers.Count();

			// Act
			var newOffers = pets.Select(pet => new PublicOffer(pet)).ToList();
			PublicOfferService.Create(newOffers);
			var newOffersCount = db.PublicOffers.Count();

			// Assert
			Assert.That(newOffersCount, Is.EqualTo(initialOffersCount + pets.Count), "The count of public offers should increment by the number of offers added.");
		}

		[Test]
		public void Read_Returns_Correct_PublicOffer()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid(), Name = "Test Pet", User = user, UserId = user.Id };
			var offer = new PublicOffer(pet) { Id = Guid.NewGuid() };
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.PublicOffers.Add(offer);
			db.SaveChanges();

			// Act
			var result = PublicOfferService.Read(offer.Id);

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(offer.Id, result.Id);
		}

		[Test]
		public void ReadAll_Returns_All_PublicOffers()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pets = new List<Pet>
			{
				new Pet { Id = Guid.NewGuid(), Name = "Test Pet 1", User = user, UserId = user.Id },
				new Pet { Id = Guid.NewGuid(), Name = "Test Pet 2", User = user, UserId = user.Id }
			};
			var offers = pets.Select(pet => new PublicOffer(pet) { Id = Guid.NewGuid() }).ToList();
			db.Users.Add(user);
			db.Pets.AddRange(pets);
			db.PublicOffers.AddRange(offers);
			db.SaveChanges();

			// Act
			var result = PublicOfferService.ReadAll();

			// Assert
			Assert.AreEqual(offers.Count, result.Count);
		}

		[Test]
		public void Update_Modifies_Existing_PublicOffer()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid(), Name = "Old Name", User = user, UserId = user.Id };
			var offer = new PublicOffer(pet) { Id = Guid.NewGuid() };
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.PublicOffers.Add(offer);
			db.SaveChanges();

			// Act
			offer.Pet.Name = "New Name";
			PublicOfferService.Update(offer);
			var updatedOffer = db.PublicOffers.Include(o => o.Pet).FirstOrDefault(o => o.Id == offer.Id);

			// Assert
			Assert.AreEqual("New Name", updatedOffer.Pet.Name);
		}

		[Test]
		public void Delete_Removes_PublicOffer_From_Database()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid(), Name = "Test Pet", User = user, UserId = user.Id };
			var offer = new PublicOffer(pet) { Id = Guid.NewGuid() };
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.PublicOffers.Add(offer);
			db.SaveChanges();
			var initialOffersCount = db.PublicOffers.Count();

			// Act
			PublicOfferService.Delete(offer.Id);
			var newOffersCount = db.PublicOffers.Count();

			// Assert
			Assert.That(newOffersCount, Is.EqualTo(initialOffersCount - 1), "The count of public offers should decrement by 1 after deleting the offer.");
		}

		[Test]
		public void DeleteByPetName_Removes_Correct_PublicOffer()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid(), Name = "Pet To Delete", User = user, UserId = user.Id };
			var offer = new PublicOffer(pet) { Id = Guid.NewGuid() };
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.PublicOffers.Add(offer);
			db.SaveChanges();

			// Act
			PublicOfferService.DeleteByPetName(pet.Name, user);
			var result = db.PublicOffers.FirstOrDefault(o => o.Id == offer.Id);

			// Assert
			Assert.IsNull(result, "The public offer should be null after deletion.");
		}

		[Test]
		public void DeleteByPetName_Throws_Exception_When_Pet_Name_Does_Not_Match_Any_Users_Pets()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var anotherUser = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid(), Name = "Test Pet", User = anotherUser, UserId = anotherUser.Id };
			var offer = new PublicOffer(pet) { Id = Guid.NewGuid() };
			db.Users.Add(user);
			db.Users.Add(anotherUser);
			db.Pets.Add(pet);
			db.PublicOffers.Add(offer);
			db.SaveChanges();

			// Act & Assert
			var ex = Assert.Throws<Exception>(() => PublicOfferService.DeleteByPetName(pet.Name, user));
			Assert.That(ex.Message, Is.EqualTo("Name doesn't match any of users pets!"));
		}

		[Test]
		public void DeleteAll_Removes_All_PublicOffers_From_Database()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pets = new List<Pet>
			{
				new Pet { Id = Guid.NewGuid(), Name = "Pet 1", User = user, UserId = user.Id },
				new Pet { Id = Guid.NewGuid(), Name = "Pet 2", User = user, UserId = user.Id }
			};
			var offers = pets.Select(pet => new PublicOffer(pet) { Id = Guid.NewGuid() }).ToList();
			db.Users.Add(user);
			db.Pets.AddRange(pets);
			db.PublicOffers.AddRange(offers);
			db.SaveChanges();
			var initialOffersCount = db.PublicOffers.Count();

			// Act
			PublicOfferService.DeleteAll();
			var newOffersCount = db.PublicOffers.Count();

			// Assert
			Assert.That(newOffersCount, Is.EqualTo(0), "All public offers should be removed from the database.");
		}

		[Test]
		public void RegisterPet_Adds_New_PublicOffer_For_Pet()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid(), Name = "Register Pet", User = user, UserId = user.Id };
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();
			var initialOffersCount = db.PublicOffers.Count();

			// Act
			PublicOfferService.RegisterPet(pet.Name, user);
			var newOffersCount = db.PublicOffers.Count();

			// Assert
			Assert.That(newOffersCount, Is.EqualTo(initialOffersCount + 1), "A new public offer should be added for the registered pet.");
		}

		[Test]
		public void RegisterPet_Creates_New_Offer_When_Pet_Exists_In_User_Database()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid(), Name = "Test Pet", User = user, UserId = user.Id };
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();

			// Act
			PublicOfferService.RegisterPet(pet.Name, user);
			var offers = db.PublicOffers.Where(o => o.PetId == pet.Id).ToList();

			// Assert
			Assert.That(offers.Count, Is.EqualTo(1), "A new public offer should be created for the pet.");
			Assert.That(offers.First().PetId, Is.EqualTo(pet.Id), "The pet ID should match the new offer's pet ID.");
		}

		[Test]
		public void RegisterPet_Throws_Exception_If_Pet_Already_Registered()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid(), Name = "Already Registered Pet", User = user, UserId = user.Id };
			var offer = new PublicOffer(pet) { Id = Guid.NewGuid() };
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.PublicOffers.Add(offer);
			db.SaveChanges();

			// Act & Assert
			var ex = Assert.Throws<Exception>(() => PublicOfferService.RegisterPet(pet.Name, user));
			Assert.That(ex.Message, Is.EqualTo("This pet is already registered!"));
		}

		[Test]
		public void RegisterPet_Throws_Exception_If_Pet_Does_Not_Exist()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var nonExistentPetName = "Non-Existent Pet";

			// Act & Assert
			var ex = Assert.Throws<Exception>(() => PublicOfferService.RegisterPet(nonExistentPetName, user));
			Assert.That(ex.Message, Is.EqualTo("This pet doesn't exist in user's database!"));
		}
	}
}
