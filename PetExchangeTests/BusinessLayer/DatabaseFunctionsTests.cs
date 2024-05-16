using BusinessLayer.Database_Functions;
using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests.BusinessLayer
{
    public class DatabaseFunctionsTests : BusinessLayerTestsManagement
    {
        [Test]
        public void DeleteAllEntries_RemovesCountries()
        {
            // Arrange
            DatabaseFunctions.SeedDatabase();

            // Act
            DatabaseFunctions.DeleteAllEntries();

            // Assert
            Assert.IsEmpty(db.Countries.ToList(), "Countries should be empty.");
        }

        [Test]
        public void DeleteAllEntries_RemovesTowns()
        {
            // Arrange
            DatabaseFunctions.SeedDatabase();

            // Act
            DatabaseFunctions.DeleteAllEntries();

            // Assert
            Assert.IsEmpty(db.Towns.ToList(), "Towns should be empty.");
        }

        [Test]
        public void DeleteAllEntries_RemovesUsers()
        {
            // Arrange
            DatabaseFunctions.SeedDatabase();

            // Act
            DatabaseFunctions.DeleteAllEntries();

            // Assert
            Assert.IsEmpty(db.Users.ToList(), "Users should be empty.");
        }

        [Test]
        public void DeleteAllEntries_RemovesPets()
        {
            // Arrange
            DatabaseFunctions.SeedDatabase();

            // Act
            DatabaseFunctions.DeleteAllEntries();

            // Assert
            Assert.IsEmpty(db.Pets.ToList(), "Pets should be empty.");
        }

        [Test]
        public void DeleteAllEntries_RemovesPublicOffers()
        {
            // Arrange
            DatabaseFunctions.SeedDatabase();

            // Act
            DatabaseFunctions.DeleteAllEntries();

            // Assert
            Assert.IsEmpty(db.PublicOffers.ToList(), "PublicOffers should be empty.");
        }

        [Test]
        public void DeleteAllEntries_RemovesUserRequests()
        {
            // Arrange
            DatabaseFunctions.SeedDatabase();

            // Act
            DatabaseFunctions.DeleteAllEntries();

            // Assert
            Assert.IsEmpty(db.Requests.ToList(), "UserRequests should be empty.");
        }

        [Test]
        public void SeedDatabase_CreatesCountries()
        {
            // Act
            DatabaseFunctions.SeedDatabase();

            // Assert
            Assert.That(db.Countries.Count(), Is.EqualTo(6), "There should be 6 countries.");
        }

        [Test]
        public void SeedDatabase_CreatesTowns()
        {
            // Act
            DatabaseFunctions.SeedDatabase();

            // Assert
            Assert.That(db.Towns.Count(), Is.EqualTo(6), "There should be 6 towns.");
        }

        [Test]
        public void SeedDatabase_CreatesUsers()
        {
            // Act
            DatabaseFunctions.SeedDatabase();

            // Assert
            Assert.That(db.Users.Count(), Is.EqualTo(6), "There should be 6 users.");
        }

        [Test]
        public void SeedDatabase_CreatesPets()
        {
            // Act
            DatabaseFunctions.SeedDatabase();

            // Assert
            Assert.That(db.Pets.Count(), Is.EqualTo(6), "There should be 6 pets.");
        }

        [Test]
        public void SeedDatabase_CreatesPublicOffers()
        {
            // Act
            DatabaseFunctions.SeedDatabase();

            // Assert
            Assert.That(db.PublicOffers.Count(), Is.EqualTo(6), "There should be 6 public offers.");
        }

        [Test]
        public void SeedDatabase_CreatesUserRequests()
        {
            // Act
            DatabaseFunctions.SeedDatabase();

            // Assert
            Assert.That(db.Requests.Count(), Is.EqualTo(6), "There should be 6 user requests.");
        }

        [Test]
        public void CheckUserReturnsCode_CorrectCredentials_ReturnsSuccessCode()
        {
            // Arrange
            DatabaseFunctions.SeedDatabase();
            var correctUsername = db.Users.First().Username;
            var correctPassword = db.Users.First().Password;

            // Act
            var result = DatabaseFunctions.CheckUserReturnsCode(correctUsername, correctPassword);

            // Assert
            Assert.That(result, Is.EqualTo(2), "Should return 2 for successful authentication.");
        }

        [Test]
        public void CheckUserReturnsCode_IncorrectPassword_ReturnsFailureCode()
        {
            // Arrange
            DatabaseFunctions.SeedDatabase();
            var correctUsername = db.Users.First().Username;
            var incorrectPassword = "wrongpassword";

            // Act
            var result = DatabaseFunctions.CheckUserReturnsCode(correctUsername, incorrectPassword);

            // Assert
            Assert.That(result, Is.EqualTo(1), "Should return 1 for incorrect password.");
        }

        [Test]
        public void CheckUserReturnsCode_NonExistentUsername_ReturnsFailureCode()
        {
            // Arrange
            DatabaseFunctions.SeedDatabase();
            var nonExistentUsername = "nonexistent";
            var correctPassword = "password";

            // Act
            var result = DatabaseFunctions.CheckUserReturnsCode(nonExistentUsername, correctPassword);

            // Assert
            Assert.That(result, Is.EqualTo(0), "Should return 0 for non-existent username.");
        }

    }
}
