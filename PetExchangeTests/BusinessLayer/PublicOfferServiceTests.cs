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
		public async Task Create_Adds_New_PublicOffer_To_Database()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid(), Name = "New Pet", User = user, UserId = user.Id };
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();
			var initialOffersCount = db.PublicOffers.Count();

			// Act
			await _publicOfferService.CreateAsync(new PublicOffer(pet));
			var newOffersCount = db.PublicOffers.Count();

			// Assert
			Assert.That(newOffersCount, Is.EqualTo(initialOffersCount + 1), "The count of public offers should increment by 1 after creating a new offer.");
		}


		[Test]
		public async Task Read_Returns_Correct_PublicOffer()
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
			var result = await _publicOfferService.ReadAsync(offer.Id);

			// Assert
			Assert.IsNotNull(result);
			Assert.That(result.Id, Is.EqualTo(offer.Id));
		}

		[Test]
		public async Task ReadAll_Returns_All_PublicOffers()
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
			var result = await _publicOfferService.ReadAllAsync();

			// Assert
			Assert.That(result.Count, Is.EqualTo(offers.Count));
		}

		[Test]
		public async Task Update_Modifies_Existing_PublicOffer()
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
			await _publicOfferService.UpdateAsync(offer);
			var updatedOffer = db.PublicOffers.Include(o => o.Pet).FirstOrDefault(o => o.Id == offer.Id);

            // Assert
            Assert.That(updatedOffer.Pet.Name, Is.EqualTo("New Name"));
		}

		[Test]
		public async Task Delete_Removes_PublicOffer_From_Database()
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
			await _publicOfferService.DeleteAsync(offer.Id);
			var newOffersCount = db.PublicOffers.Count();

			// Assert
			Assert.That(newOffersCount, Is.EqualTo(initialOffersCount - 1), "The count of public offers should decrement by 1 after deleting the offer.");
		}
	}
}
