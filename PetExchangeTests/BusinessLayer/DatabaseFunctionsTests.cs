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
        public void DeleteAllEntries_RemovesTowns()
        {
            // Act
            DatabaseFunctions.DeleteAllEntries();

            // Assert
            Assert.That(db.Towns.ToList(), Is.Empty, "Towns should be empty.");
        }

        [Test]
        public void DeleteAllEntries_RemovesUsers()
        {
            // Act
            DatabaseFunctions.DeleteAllEntries();

            // Assert
            Assert.That(db.Users.ToList(), Is.Empty, "Users should be empty.");
        }

        [Test]
        public void DeleteAllEntries_RemovesPets()
        {
            // Act
            DatabaseFunctions.DeleteAllEntries();

            // Assert
            Assert.That(db.Pets.ToList(), Is.Empty, "Pets should be empty.");
        }

        [Test]
        public void DeleteAllEntries_RemovesPublicOffers()
        {
            // Act
            DatabaseFunctions.DeleteAllEntries();

            // Assert
            Assert.That(db.PublicOffers.ToList(), Is.Empty, "PublicOffers should be empty.");
        }

        [Test]
        public void DeleteAllEntries_RemovesUserRequests()
        {
            // Act
            DatabaseFunctions.DeleteAllEntries();

            // Assert
            Assert.That(db.Requests.ToList(), Is.Empty, "UserRequests should be empty.");
        }
    }
}
