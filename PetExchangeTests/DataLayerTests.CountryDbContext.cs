using BusinessLayer.Models;
using DataLayer.ModelsDbContext;
using DataLayer.ProjectDbContext;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests
{
    public partial class DataLayerTests
    {
        public static CountryDbContext countryDbContext;
        public static Mock<PetExchangeDbContext> mockDbContext;

        [SetUp]
        public void SetupCountryDbContext()
        {
            mockDbContext = new Mock<PetExchangeDbContext>();
            countryDbContext = new CountryDbContext(mockDbContext.Object);
        }

        [Test]
        public void Create_AddsCountryToDatabase()
        {
            // Arrange
            var country = new Country(Guid.NewGuid(), "Default Name");

            // Set up behavior for the mock DbSet to return the collection of countries
            var countries = new List<Country> { country };
            var mockSet = new Mock<DbSet<Country>>();
            mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(countries.AsQueryable().Provider);
            mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(countries.AsQueryable().Expression);
            mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(countries.AsQueryable().ElementType);
            mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(countries.AsQueryable().GetEnumerator());

            // Set up behavior for the mock DbContext's Countries property to return the mock DbSet
            mockDbContext.Setup(db => db.Countries).Returns(mockSet.Object);

            // Act
            countryDbContext.Create(country);

            // Assert
            mockDbContext.Verify(db => db.Countries.Add(country), Times.Once);
            mockDbContext.Verify(db => db.SaveChanges(), Times.Once);

            // Additional assertions (if needed)
            var addedCountry = mockDbContext.Object.Countries.FirstOrDefault(c => c.Id == country.Id);
            Assert.IsNotNull(addedCountry, "Country should have been added to the database");
            Assert.AreEqual(country.Name, addedCountry.Name, "Added country should have the correct name");
        }

        [Test]
        public void Read_ReturnsCountryById()
        {
            // Arrange
            var countryId = Guid.NewGuid();
            var expectedCountry = new Country(countryId, "Default Name");
            var countries = new List<Country> { expectedCountry };
            var mockSet = new Mock<DbSet<Country>>();

            // Set up behavior for the mock DbSet to return the collection of countries
            mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(countries.AsQueryable().Provider);
            mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(countries.AsQueryable().Expression);
            mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(countries.AsQueryable().ElementType);
            mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(countries.AsQueryable().GetEnumerator());

            // Set up behavior for the mock DbContext's Countries property to return the mock DbSet
            mockDbContext.Setup(db => db.Countries).Returns(mockSet.Object);

            // Act
            var result = countryDbContext.Read(countryId);

            // Assert
            Assert.AreEqual(expectedCountry, result, "Read method doesn't return correct country by id query!");
        }

        [Test]
        public void ReadAll_ReturnsCountries()
        {
            // Arrange
            var expectedCountries = new List<Country> { new Country("Default Name 1"), new Country("Default Name 2") };

            // Set up behavior for the mock DbSet to return the collection of countries
            var mockSet = new Mock<DbSet<Country>>();
            mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(expectedCountries.AsQueryable().Provider);
            mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(expectedCountries.AsQueryable().Expression);
            mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(expectedCountries.AsQueryable().ElementType);
            mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(expectedCountries.AsQueryable().GetEnumerator());

            // Set up behavior for the mock DbContext's Countries property to return the mock DbSet
            mockDbContext.Setup(db => db.Countries).Returns(mockSet.Object);

            // Act
            var result = countryDbContext.ReadAll();

            // Assert
            Assert.IsTrue(expectedCountries.SequenceEqual(result), "Read method doesn't return correct countries!");
        }

        [Test]
        public void Update_EntityDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            var countryId = Guid.NewGuid();
            var nonExistingCountry = new Country(countryId, "NonExistingCountry");
            mockDbContext.Setup(db => db.Countries.Find(countryId)).Returns((Country)null);
            var countries = new List<Country> { new Country("Country1"), new Country("Country2") };
            var mockSet = new Mock<DbSet<Country>>();

            // Set up behavior for the mock DbSet to return the collection of countries
            mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(countries.AsQueryable().Provider);
            mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(countries.AsQueryable().Expression);
            mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(countries.AsQueryable().ElementType);
            mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(countries.AsQueryable().GetEnumerator());

            // Set up behavior for the mock DbContext's Countries property to return the mock DbSet
            mockDbContext.Setup(db => db.Countries).Returns(mockSet.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => countryDbContext.Update(nonExistingCountry));
            Assert.AreEqual("Entity with id:" + countryId + " doesn't exist in the database!", ex.Message, "Exception message is incorrect");
        }


        [Test]
        public void Delete_EntityExists_DeletesEntity()
        {
            // Arrange
            var countryId = Guid.NewGuid();
            var ExistingCountry = new Country(countryId, "ExistingCountry");
            mockDbContext.Setup(db => db.Countries.Find(countryId)).Returns((Country)null);
            var countries = new List<Country> { new Country("Country1"), ExistingCountry,new Country("Country2") };
            var mockSet = new Mock<DbSet<Country>>();

            // Set up behavior for the mock DbSet to return the collection of countries
            mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(countries.AsQueryable().Provider);
            mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(countries.AsQueryable().Expression);
            mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(countries.AsQueryable().ElementType);
            mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(countries.AsQueryable().GetEnumerator());
            
            // Set up behavior for the mock DbContext's Countries property to return the mock DbSet
            mockDbContext.Setup(db => db.Countries).Returns(mockSet.Object);

            // Act

            countryDbContext.Delete(countryId);

            // Assert
            mockDbContext.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Test]
        public void Delete_EntityDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            var countryId = Guid.NewGuid();
            var ExistingCountry = new Country(countryId, "NonExistingCountry");
            mockDbContext.Setup(db => db.Countries.Find(countryId)).Returns((Country)null);

            var countries = new List<Country> { new Country("Country1"), new Country("Country2") };
            var mockSet = new Mock<DbSet<Country>>();

            // Set up behavior for the mock DbSet to return the collection of countries
            mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(countries.AsQueryable().Provider);
            mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(countries.AsQueryable().Expression);
            mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(countries.AsQueryable().ElementType);
            mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(countries.AsQueryable().GetEnumerator());

            // Set up behavior for the mock DbContext's Countries property to return the mock DbSet
            mockDbContext.Setup(db => db.Countries).Returns(mockSet.Object);
            // Act & Assert
            Assert.Throws<ArgumentException>(() => countryDbContext.Delete(countryId));
        }

        [Test]
        public void RetrieveCountry_ReturnsCountryByName()
        {
            // Arrange
            var expectedName = "TestCountry";
            var expectedCountry = new Country(expectedName);
            var countries = new List<Country> { new Country("Country1"), expectedCountry,new Country("Country2") };
            var mockSet = new Mock<DbSet<Country>>();

            // Create a mock DbSet with the list of countries and setup the behavior
            mockSet.As<IQueryable<Country>>().Setup(m => m.Provider).Returns(countries.AsQueryable().Provider);
            mockSet.As<IQueryable<Country>>().Setup(m => m.Expression).Returns(countries.AsQueryable().Expression);
            mockSet.As<IQueryable<Country>>().Setup(m => m.ElementType).Returns(countries.AsQueryable().ElementType);
            mockSet.As<IQueryable<Country>>().Setup(m => m.GetEnumerator()).Returns(countries.AsQueryable().GetEnumerator());


            // Set up behavior for the mock DbContext's Countries property to return the mock DbSet
            mockDbContext.Setup(db => db.Countries).Returns(mockSet.Object);

            // Act
            var result = countryDbContext.RetrieveCountry(expectedName);

            // Assert
            Assert.AreEqual(expectedCountry, result, "Incorrect country model for query country Name");
        }
    }
}
