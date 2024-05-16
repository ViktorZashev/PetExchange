using BusinessLayer.Functions;
using BusinessLayer.Models;
using DataLayer.ModelsDbContext;
using NUnit.Framework.Internal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests.BusinessLayer
{
    public class CountryServiceTests : BusinessLayerTestsManagement
    {
        [Test]
        public void CreateMethod_SingleCountryCreatesSuccessfully()
        {
            // Arrange
            var country = new Country("TestCountry");
            var initialCount = db.Countries.Count();
            // Act
            CountryService.Create(country);
            var expectedCount = initialCount + 1;
            var actualCount = db.Countries.Count();

            // Assert
            Assert.That(expectedCount, Is.EqualTo(actualCount), "Create method doesn't add one unique country to database!");
        }

        [Test]
        public void CreateMethod_ManyCountriesCreateSuccessfully()
        {
            // Arrange
            var Countries = new List<Country>
            {
                new Country("Country1"),
                new Country("Country2")
            };
            var initialCount = db.Countries.Count();

            // Act
            CountryService.Create(Countries);
            var expectedCount = initialCount + 2;
            var actualCount = db.Countries.Count();

            // Assert
            Assert.That(expectedCount, Is.EqualTo(actualCount), "Create method doesn't add many unique countries to database!");
        }

		[Test]
		public void CreateListMethod_AddsMultiplePetsToDatabase()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pets = new List<Pet>
			{
				new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false),
				new Pet(user, "Max", "", 3, "Dog", "A playful dog", true)
			};
			db.Users.Add(user);
			db.SaveChanges();

			// Act
			PetService.Create(pets);

			// Assert
			foreach (var pet in pets)
			{
				var createdPet = db.Pets.FirstOrDefault(p => p.Name == pet.Name);
				Assert.IsNotNull(createdPet, $"Pet '{pet.Name}' was not created.");
			}
		}

		[Test]
		public void Read_ReturnsCorrectCountryById()
		{
            // Arrange
            var id = Guid.NewGuid();
			var country = new Country(id,"Country");
			db.Countries.Add(country);
			db.SaveChanges();
			// Act
			var actualCountry = CountryService.Read(id);
			// Assert
			Assert.That(actualCountry, Is.EqualTo(country), "Read method doesn't return the correct country by it's id!");
		}

		[Test]
		public void Read_ReturnsNullWhenIdIsUnique()
		{
            // Arrange
            var uniqueId = Guid.NewGuid();

			// Act
			var nullCountry = CountryService.Read(uniqueId);
			// Assert
			Assert.IsNull(nullCountry, "Read method doesn't return null when inputed an unique id!");
		}

		[Test]
        public void ReadAll_ReturnsAllCountries()
        {
            // Arrange
            DeleteAllEntriesInDb();
            var country1 = new Country("Country 1");
            var country2 = new Country("Country 2");
            var expectedList = new List<Country> { country1, country2 };
            db.Countries.Add(country1);
            db.Countries.Add(country2);
            db.SaveChanges();

            // Act
            var actualCountries = CountryService.ReadAll(false);

            // Assert
            Assert.That(expectedList, Is.EquivalentTo(actualCountries),"Read All method doesn't return all entries in database!");
        }

		[Test]
		public void UpdateMethod_UpdatesCountryInDatabase()
		{
			// Arrange
			var country = new Country("Original Country");
			db.Countries.Add(country);
			db.SaveChanges();
			var updatedCountry = new Country(country.Id, "Updated Country");

			// Act
			CountryService.Update(updatedCountry);
			var result = db.Countries.Find(country.Id);

			// Assert
			Assert.IsNotNull(result, "Country not found in database after update.");
			Assert.That(result.Name, Is.EqualTo("Updated Country"), "Country name was not updated in the database.");
		}

		[Test]
		public void UpdateMethod_ThrowsExceptionWhenCountryDoesNotExist()
		{
			// Arrange
			var nonExistentId = Guid.NewGuid();
			var updatedCountry = new Country(nonExistentId, "Nonexistent Country");

			// Act & Assert
			Assert.Throws<ArgumentException>(() => CountryService.Update(updatedCountry), "Update method does not throw an exception when the country does not exist in the database.");
		}

		[Test]
		public void DeleteMethod_DeletesCountryFromDatabase()
		{
			// Arrange
			var country = new Country("Country to Delete");
			db.Countries.Add(country);
			db.SaveChanges();

			// Act
			CountryService.Delete(country.Id);
			var result = db.Countries.Find(country.Id);

			// Assert
			Assert.IsNull(result, "Country was not deleted from the database.");
		}

		[Test]
		public void DeleteMethod_ThrowsExceptionWhenCountryDoesNotExist()
		{
			// Arrange
			var nonExistentId = Guid.NewGuid();

			// Act & Assert
			Assert.Throws<ArgumentException>(() => CountryService.Delete(nonExistentId), "Delete method does not throw an exception when the country does not exist in the database.");
		}

		[Test]
		public void DeleteMethod_DeletesExistingPetForUser()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();

			// Act
			PetService.Delete("Fluffy", user);

			// Assert
			var deletedPet = db.Pets.FirstOrDefault(p => p.Name == "Fluffy");
			Assert.IsNull(deletedPet, "Pet 'Fluffy' should have been deleted.");
		}

		[Test]
		public void ReturnAllPetsMethod_ReturnsEmptyListWhenNoPetsBelongToUser()
		{
			// Arrange
			// Database is already empty because of setup function

			// Act
			var userPets = PetService.ReturnAllPets(new User { Id = Guid.NewGuid(), Name = "Bob" });

			// Assert
			Assert.IsNotNull(userPets, "ReturnAllPets method should return a list.");
			Assert.IsEmpty(userPets, "ReturnAllPets method should return an empty list when no pets belong to the provided user.");
		}

		[Test]
		public void DeleteMethod_ThrowsExceptionWhenPetDoesNotExistForUser()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();

			// Act & Assert
			Assert.Throws<Exception>(() => PetService.Delete("Max", user), "Delete method should throw an exception when pet 'Max' does not exist for user 'John'.");
		}


		[Test]
		public void DeleteAllMethod_DeletesAllCountriesFromDatabase()
		{
			// Arrange
			var country1 = new Country("Country1");
			var country2 = new Country("Country2");
			db.Countries.Add(country1);
			db.Countries.Add(country2);
			db.SaveChanges();

			// Act
			CountryService.DeleteAll();
			var result = db.Countries.ToList();

			// Assert
			Assert.IsEmpty(result, "DeleteAll method does not delete all countries from the database.");
		}

		[Test]
		public void RetrieveCountryMethod_ReturnsCorrectCountry()
		{
			// Arrange
			var country = new Country("Country to Retrieve");
			db.Countries.Add(country);
			db.SaveChanges();

			// Act
			var result = CountryService.RetrieveCountry("Country to Retrieve");

			// Assert
			Assert.IsNotNull(result, "RetrieveCountry method does not return the country from the database.");
			Assert.That(result.Name, Is.EqualTo(country.Name), "RetrieveCountry method returns incorrect country name.");
		}

		[Test]
		public void CheckPetExistsMethod_ReturnsFalseWhenNoPetsExist()
		{
			// Act
			var result = PetService.CheckPetExists("NonExistentPetName");

			// Assert
			Assert.IsFalse(result, "CheckPetExists method should return false when no pets exist.");
		}

		[Test]
		public void RetrieveCountryMethod_ThrowsExceptionWhenDuplicateCountriesExist()
		{
			// Arrange
			var country1 = new Country("Duplicate Country");
			var country2 = new Country("Duplicate Country");
			db.Countries.Add(country1);
			db.Countries.Add(country2);
			db.SaveChanges();

			// Act & Assert
			Assert.Throws<Exception>(() => CountryService.RetrieveCountry("Duplicate Country"), "RetrieveCountry method does not throw an exception when duplicate country names exist in the database.");
		}
		[Test]
		public void ReturnAllPetsMethod_ReturnsEmptyListWhenNoPetsExist()
		{
			// Arrange
			// Database is already empty because of setup functions

			// Act
			var result = PetService.ReturnAllPets();

			// Assert
			Assert.IsNotNull(result, "ReturnAllPets method should return a list.");
			Assert.IsEmpty(result, "ReturnAllPets method should return an empty list when no pets exist.");
		}
	}
}
