using BusinessLayer.Models;
using DataLayer.ModelsDbContext;
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
        public void CanAddCountryModel()
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
    }
}
