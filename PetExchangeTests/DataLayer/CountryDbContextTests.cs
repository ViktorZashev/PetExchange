using BusinessLayer.Models;
using DataLayer.ModelsDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests.DataLayer
{
    public class CountryDbContextTests : DataLayerTestsManagement
    {
        [Test]
        public void CreateMethod_AddsCountryModelToDatabase()
        {
            // Arrange
            var newCountry = new Country("New Country");
            var initialCount = db.Countries.Count();

            // Act
            countryContext.Create(newCountry);
            var actualCount = db.Countries.Count();
            var expectedCount = initialCount + 1;

            // Assert
            Assert.That(expectedCount, Is.EqualTo(actualCount), "The count of existing countries in database doesn't increment by 1 when adding one country model!");
        }

		[Test]
		public void CreateMethod_ThrowsExceptionIfAddingDuplicateCountryModel()
		{
			// Arrange
			var newCountry = new Country("New Country");
			var duplicateCountry = newCountry;

			// Act & Assert
			countryContext.Create(newCountry);
            Assert.Throws<ArgumentException>(() => countryContext.Create(duplicateCountry), "Create method doesn't throw exception when entering a duplicate key!");
		}

		[Test]
		public void ReadMethod_RetrievesACountryModelFromDatabase()
		{
			// Arrange
			var id = Guid.NewGuid();
			var enteredCountry = new Country(id,"New Country");

			// Act
			db.Countries.Add(enteredCountry);
			db.SaveChanges();
			var actualCountry = countryContext.Read(id);

			// Assert
			Assert.IsTrue(AreCountriesEqual(enteredCountry, actualCountry), "Read method doesn't return the country model entered in database!");
		}

		[Test]
		public void ReadMethod_ReturnsNullWhenIdDoesntMatchAnyEntryFromDatabase()
		{
			// Arrange
			var uniqueId = Guid.NewGuid();
			var enteredCountry1 = new Country("New Country1");
			var enteredCountry2 = new Country("New Country2");

			// Act
			db.Countries.Add(enteredCountry1);
			db.Countries.Add(enteredCountry2);
			var outputedCountry = countryContext.Read(uniqueId);
			db.SaveChanges();

			// Assert
			Assert.That(outputedCountry,Is.EqualTo(null),"Read method doesn't throw exception when no matching entry exists in database!");
		}

		[Test]
		public void ReadAllMethod_RetrievesAllCountryModelsFromDatabase()
		{
			// Arrange
			var enteredCountry1 = new Country("New Country1");
			var enteredCountry2 = new Country("New Country2");
			var countriesList = new List<Country> { enteredCountry1, enteredCountry2 };

			// Act
			db.Countries.Add(enteredCountry1);
			db.Countries.Add(enteredCountry2);
			db.SaveChanges();
			var outputedCountries = countryContext.ReadAll();

			// Assert
			Assert.That(countriesList, Is.EqualTo(outputedCountries), "ReadAll method doesn't return all entries found database!");
		}

		[Test]
		public void ReadAllMethod_ReturnsEmptyListWhenThereAreNoCountryModelsInDatabase()
		{
			// Arrange
			// Database should be null when starting every unit test
			
			// Act
			var outputedCountries = countryContext.ReadAll();

			// Assert
			Assert.That(outputedCountries, Is.EqualTo(new List<Country>()), "ReadAll method doesn't return an empty List when no entries exists in database!");
		}

		[Test]
		public void UpdateMethod_ThrowsExceptionWhenTheresNoCountryWithTheSameIdAsUpdatedObjectInDatabase()
		{
			// Arrange
			var uniqueId = Guid.NewGuid();
			var countryOld = new Country("New Country");
			var countryUpdatedUniqueId = new Country(uniqueId,"New Country2");
			db.Countries.Add(countryOld);

			// Act & Assert
			Assert.Throws<ArgumentException>(() => countryContext.Update(countryUpdatedUniqueId), "Update method doesn't throw exception when no matching id as parameter object id exist in database!");
		}
		[Test]
		public void UpdateMethod_UpdatesCountryModelWhenUpdateParameterMatchesRegisteredId()
		{
			// Arrange
			var matchingId = Guid.NewGuid();
			var countryOld = new Country(matchingId,"Old Country");
			var countryNew = new Country(matchingId,"New Country");
			db.Countries.Add(countryOld);
			db.SaveChanges();

			// Act
			countryContext.Update(countryNew);
			Country outputedCountry = db.Countries.Where(x => x.Id == matchingId).FirstOrDefault();

			// Assert
			Assert.IsTrue(AreCountriesEqual(outputedCountry, countryNew), "Update method doesn't update values of model with matching id as parameter!");
		}

		[Test]
		public void DeleteMethod_ThrowsExceptionWhenIdDoesntExistInDatabase()
		{
			// Arrange
			// Database should be null, and uniqueId should be unique anyway
			var uniqueId = Guid.NewGuid();

			// Act & Assert
			Assert.Throws<ArgumentException>(() => countryContext.Delete(uniqueId), "Delete method doesn't throw exception when trying to delete a non-existing key!");
		}

		[Test]
		public void DeleteMethod_DeletesExistingEntryInDatabase()
		{
			// Arrange
			var id = Guid.NewGuid();
			var countryToBeDeleted = new Country(id, "Country");
			db.Countries.Add(countryToBeDeleted);
			db.SaveChanges();

			// Act
			countryContext.Delete(id);
			Country outputedResult = db.Countries.Where(x => x.Id == id).FirstOrDefault();

			// Assert
			Assert.IsTrue(outputedResult == null, "Delete method doesn't delete an existing entry by it's id!");
		}

		[Test]
		public void RetrieveCountryMethod_ThrowsExceptionWhenMatchingCountriesByNameExist()
		{
			// Arrange
			var matchingName = "New Country";
			var country1 = new Country(matchingName);
			var country2 = new Country(matchingName);
			db.Countries.Add(country1);
			db.Countries.Add(country2);
			db.SaveChanges();

			// Act & Assert
			Assert.Throws<Exception>(() => countryContext.RetrieveCountry(matchingName), "Retrieve country method doesn't throw exception when there are duplicate names in database!");
		}

		[Test]
		public void RetrieveCountryMethod_ReturnsCorrectCountryModelByName()
		{
			// Arrange
			var name = "New Country";
			var initialCountry = new Country(name);
			db.Countries.Add(initialCountry);
			db.SaveChanges();

			// Act
			var foundCountry = countryContext.RetrieveCountry(name);

			// Assert
			Assert.IsTrue(AreCountriesEqual(initialCountry,foundCountry), "Retrieve country method doesn't return unique country model by name that exists in database!");
		}
	}
}
