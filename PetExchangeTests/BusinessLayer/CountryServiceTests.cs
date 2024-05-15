using BusinessLayer.Functions;
using BusinessLayer.Models;
using DataLayer.ModelsDbContext;
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
            actualCountries.Sort();
            expectedList.Sort();
            // Assert
            Assert.AreEqual(expectedList, actualCountries,"Read All method doesn't return all entries in database!");
        }
    }
}
