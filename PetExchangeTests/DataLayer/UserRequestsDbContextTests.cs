using Azure.Core;
using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetExchangeTests.DataLayer
{
    public class UserRequestsDbContextTests : DataLayerTestsManagement
    {
        [Test]
        public void CreateMethod_AddsUserRequestsToDatabase()
        {
            // Arrange
            var publicOffer = new PublicOffer();
            db.PublicOffers.Add(publicOffer);
            db.SaveChanges();
            var user = new User();
            var newUserRequest = new UserRequests(publicOffer, false);
            var initialCount = db.Requests.Count();

            // Act
            userRequestsContext.Create(newUserRequest);
            var actualCount = db.Requests.Count();
            var expectedCount = initialCount + 1;

            // Assert
            Assert.That(expectedCount, Is.EqualTo(actualCount), "The count of existing user requests in the database doesn't increment by 1 when adding one user request!");
        }

        [Test]
        public void CreateMethod_ThrowsExceptionIfPublicOfferDoesNotExist()
        {
            // Arrange
            var newTown = new Town("Plovdiv");
            var newUser = new User()
            {
                Town = newTown,
                TownId = newTown.Id
            };
            var newPet = new Pet(Guid.NewGuid(),newUser);
            var nonExistingPublicOffer = new PublicOffer(newPet);
            var newUserRequest = new UserRequests(nonExistingPublicOffer, false);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => userRequestsContext.Create(newUserRequest), "Create method doesn't throw exception when user request doesn't include a register public offer!");
        }
        [Test]
        public void CreateMethod_ThrowsArgumentNullExceptionWhenUserRequestIsNull()
        {
            // Arrange
            UserRequests? nullUserRequest = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                userRequestsContext.Create(nullUserRequest);
            }, "Create method doesn't throw ArgumentNullException when user request is null!");
        }

        [Test]
        public void CreateMethod_DoesNotAddUserRequestIfAlreadyExists()
        {
            // Arrange
            var publicOffer = new PublicOffer();
            var user = new User();
            var newUserRequest = new UserRequests(publicOffer, false);
            db.Requests.Add(newUserRequest);
            db.SaveChanges();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => userRequestsContext.Create(newUserRequest), "Create method doesn't throw exception when a duplicate entry already exists!");
        }

        [Test]
        public void ReadMethod_RetrievesUserRequestFromDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var publicOffer = new PublicOffer();
            var user = new User();
            var newUserRequest = new UserRequests(publicOffer, false) { Id = id };

            // Act
            db.Requests.Add(newUserRequest);
            db.SaveChanges();
            var actualUserRequest = userRequestsContext.Read(id);

            // Assert
            Assert.That(actualUserRequest, Is.EqualTo(newUserRequest), "Read method doesn't return the user request entered in the database!");
        }

        [Test]
        public void ReadMethod_ReturnsNullWhenUserRequestDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            Assert.That(userRequestsContext.Read(nonExistentId), Is.EqualTo(null), "Read method doesn't return null when the user request does not exist in the database!");
        }


        [Test]
        public void UpdateMethod_UpdatesUserRequestInDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var publicOffer = new PublicOffer();
            var user = new User();
            var initialUserRequest = new UserRequests(publicOffer, false) { Id = id };
            var updatedUserRequest = new UserRequests(publicOffer, true) { Id = id };

            db.Requests.Add(initialUserRequest);
            db.SaveChanges();

            // Act
            userRequestsContext.Update(updatedUserRequest);
            var actualUserRequest = db.Requests.FirstOrDefault(ur => ur.Id == id);

            // Assert
            Assert.That(actualUserRequest.IsAccepted, Is.EqualTo(updatedUserRequest.IsAccepted), "Update method doesn't update the user request in the database!");
        }

        [Test]
        public void UpdateMethod_ThrowsExceptionWhenUserRequestDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            var userRequestToUpdate = new UserRequests { Id = nonExistentId };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => userRequestsContext.Update(userRequestToUpdate), "Update method doesn't throw an exception when the user request does not exist in the database!");
        }

        [Test]
        public void ReadAllMethod_RetrievesAllUserRequestsFromDatabase()
        {
            // Arrange
            var publicOffer = new PublicOffer();
            var user = new User();
            var userRequest1 = new UserRequests(publicOffer, false);
            var userRequest2 = new UserRequests(publicOffer, true);

            db.Requests.Add(userRequest1);
            db.Requests.Add(userRequest2);
            db.SaveChanges();

            // Act
            var outputUserRequests = userRequestsContext.ReadAll();

            // Assert
            Assert.That(outputUserRequests, Has.Count.EqualTo(2), "ReadAll method doesn't return all entries found in the database!");
            Assert.That(outputUserRequests.Any(ur => ur.IsAccepted == false), Is.True, "ReadAll method doesn't return the correct user requests from the database!");
            Assert.That(outputUserRequests.Any(ur => ur.IsAccepted == true), Is.True, "ReadAll method doesn't return the correct user requests from the database!");
        }

        [Test]
        public void ReadAllMethod_ReturnsEmptyListWhenThereAreNoUserRequestsInDatabase()
        {
            // Arrange
            // Ensure database is empty

            // Act
            var outputUserRequests = userRequestsContext.ReadAll();

            // Assert
            Assert.That(outputUserRequests, Is.Empty, "ReadAll method doesn't return an empty list when no entries exist in the database!");
        }

        [Test]
        public void DeleteMethod_RemovesUserRequestFromDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var publicOffer = new PublicOffer();
            var user = new User();
            var userRequestToDelete = new UserRequests(publicOffer, false) { Id = id };

            db.Requests.Add(userRequestToDelete);
            db.SaveChanges();

            // Act
            userRequestsContext.Delete(id);
            var actualUserRequest = db.Requests.FirstOrDefault(ur => ur.Id == id);

            // Assert
            Assert.IsNull(actualUserRequest, "Delete method doesn't remove the user request from the database!");
        }


        [Test]
        public void DeleteMethod_ThrowsExceptionWhenUserRequestDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => userRequestsContext.Delete(nonExistentId), "Delete method doesn't throw an exception when the user request does not exist in the database!");
        }
    }
}
