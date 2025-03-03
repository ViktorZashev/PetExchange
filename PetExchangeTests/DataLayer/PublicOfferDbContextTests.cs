using BusinessLayer.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests.DataLayer
{
    public class PublicOfferDbContextTests : DataLayerTestsManagement
    {
        [Test]
        public void CreateMethod_AddsPublicOfferToDatabase()
        {
            // Arrange
            var newUser = new User { Id = Guid.NewGuid(), TownId = Guid.NewGuid() };
            var newPet = new Pet { Id = Guid.NewGuid(), Name = "Buddy", UserId = newUser.Id, User = newUser };
            db.Pets.Add(newPet);
            db.Users.Add(newUser);
            db.SaveChanges();

            var newOffer = new PublicOffer(newPet);
            var initialCount = db.PublicOffers.Count();

            // Act
            publicOfferContext.Create(newOffer);
            var actualCount = db.PublicOffers.Count();
            var expectedCount = initialCount + 1;

            // Assert
            Assert.That(expectedCount, Is.EqualTo(actualCount), "The count of public offers in the database doesn't increment by 1 when adding one public offer!");
        }

        [Test]
        public void CreateMethod_ThrowsExceptionWhenTryingToAddDuplicateKey()
        {
            // Arrange
            var newUser = new User { Id = Guid.NewGuid(), TownId = Guid.NewGuid() };
            var newPet = new Pet { Id = Guid.NewGuid(), Name = "Buddy", UserId = newUser.Id, User = newUser };
            db.Pets.Add(newPet);
            db.Users.Add(newUser);
            db.SaveChanges();

            var matchingId = Guid.NewGuid();
            var newOffer = new PublicOffer(newPet) { Id = matchingId };
            var duplicateOffer = new PublicOffer(newPet) { Id = matchingId };

            // Act & Assert
            publicOfferContext.Create(newOffer);
            Assert.Throws<InvalidOperationException>(() => publicOfferContext.Create(duplicateOffer));
        }

        [Test]
        public void ReadMethod_RetrievesAPublicOfferFromDatabase()
        {
            // Arrange
            var newUser = new User { Id = Guid.NewGuid(), TownId = Guid.NewGuid() };
            var newPet = new Pet { Id = Guid.NewGuid(), Name = "Buddy", UserId = newUser.Id, User = newUser };
            db.Pets.Add(newPet);
            db.Users.Add(newUser);
            db.SaveChanges();

            var id = Guid.NewGuid();
            var enteredOffer = new PublicOffer(newPet) { Id = id };
            db.PublicOffers.Add(enteredOffer);
            db.SaveChanges();

            // Act
            var actualOffer = publicOfferContext.Read(id);

            // Assert
            Assert.That(actualOffer.Id, Is.EqualTo(enteredOffer.Id), "Read method doesn't return the public offer entered in the database!");
            Assert.That(actualOffer.PetId, Is.EqualTo(enteredOffer.PetId), "Read method doesn't return the correct PetId!");
            Assert.That(actualOffer.TownId, Is.EqualTo(enteredOffer.TownId) , "Read method doesn't return the correct TownId!");
        }

        [Test]
        public void UpdateMethod_UpdatesPublicOfferInDatabase()
        {
            // Arrange
            var newUser = new User { Id = Guid.NewGuid(), TownId = Guid.NewGuid() };
            var newPet = new Pet { Id = Guid.NewGuid(), Name = "Buddy", UserId = newUser.Id, User = newUser };
            db.Pets.Add(newPet);
            db.Users.Add(newUser);
            db.SaveChanges();

            var id = Guid.NewGuid();
            var initialOffer = new PublicOffer(newPet) { Id = id };
            db.PublicOffers.Add(initialOffer);
            db.SaveChanges();

            var newPetUpdated = new Pet { Id = Guid.NewGuid(), Name = "Buddy", UserId = newUser.Id, User = newUser };
            db.Pets.Add(newPetUpdated);
            db.SaveChanges();

            var updatedOffer = new PublicOffer(newPetUpdated) { Id = id };

            // Act
            publicOfferContext.Update(updatedOffer);
            var actualOffer = db.PublicOffers.FirstOrDefault(po => po.Id == id);

            // Assert
            Assert.That(actualOffer.PetId, Is.EqualTo(updatedOffer.PetId), "Update method doesn't update the PetId in the public offer in the database!");
            Assert.That(actualOffer.TownId, Is.EqualTo(updatedOffer.TownId), "Update method doesn't update the TownId in the public offer in the database!");
        }

        [Test]
        public void UpdateMethod_ThrowsExceptionWhenPublicOfferDoesNotExist()
        {
            // Arrange
            var newUser = new User { Id = Guid.NewGuid(), TownId = Guid.NewGuid() };
            var newPet = new Pet { Id = Guid.NewGuid(), Name = "Buddy", UserId = newUser.Id, User = newUser };
            var nonExistentId = Guid.NewGuid();
            var offerToUpdate = new PublicOffer(newPet) { Id = nonExistentId };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => publicOfferContext.Update(offerToUpdate), "Update method doesn't throw an exception when the public offer does not exist in the database!");
        }

        [Test]
        public void DeleteMethod_RemovesPublicOfferFromDatabase()
        {
            // Arrange
            var newUser = new User { Id = Guid.NewGuid(), TownId = Guid.NewGuid() };
            var newPet = new Pet { Id = Guid.NewGuid(), Name = "Buddy", UserId = newUser.Id, User = newUser };
            db.Pets.Add(newPet);
            db.Users.Add(newUser);
            db.SaveChanges();

            var id = Guid.NewGuid();
            var offerToDelete = new PublicOffer(newPet) { Id = id };
            db.PublicOffers.Add(offerToDelete);
            db.SaveChanges();

            // Act
            publicOfferContext.Delete(id);
            var actualOffer = db.PublicOffers.FirstOrDefault(po => po.Id == id);

            // Assert
            Assert.IsNull(actualOffer, "Delete method doesn't remove the public offer from the database!");
        }

        [Test]
        public void ReadAllMethod_RetrievesAllPublicOffersFromDatabase()
        {
            // Arrange
            var newUser = new User { Id = Guid.NewGuid(), TownId = Guid.NewGuid() };
            var newPet1 = new Pet { Id = Guid.NewGuid(), Name = "Buddy", UserId = newUser.Id, User = newUser };
            var newPet2 = new Pet { Id = Guid.NewGuid(), Name = "Max", UserId = newUser.Id, User = newUser };
            db.Pets.Add(newPet1);
            db.Pets.Add(newPet2);
            db.Users.Add(newUser);
            db.SaveChanges();

            var enteredOffer1 = new PublicOffer(newPet1);
            var enteredOffer2 = new PublicOffer(newPet2);
            db.PublicOffers.Add(enteredOffer1);
            db.PublicOffers.Add(enteredOffer2);
            db.SaveChanges();

            // Act
            var outputedOffers = publicOfferContext.ReadAll();

            // Assert
            Assert.That(outputedOffers.Count, Is.EqualTo(2), "ReadAll method doesn't return all entries found in the database!");
            Assert.IsTrue(outputedOffers.Any(po => po.Pet.Name == "Buddy"), "ReadAll method doesn't return the correct public offers from the database!");
            Assert.IsTrue(outputedOffers.Any(po => po.Pet.Name == "Max"), "ReadAll method doesn't return the correct public offers from the database!");
        }

        [Test]
        public void ReadAllMethod_ReturnsEmptyListWhenThereAreNoPublicOffersInDatabase()
        {
            // Arrange
            // Database is already wiped due to setup function

            // Act
            var outputedOffers = publicOfferContext.ReadAll();

            // Assert
            Assert.That(outputedOffers, Is.Empty, "ReadAll method doesn't return an empty list when no entries exist in the database!");
        }

        [Test]
        public void ReadAllMethod_IncludesNavigationalPropertiesWhenSpecified()
        {
            // Arrange
            var newUser = new User { Id = Guid.NewGuid(), TownId = Guid.NewGuid() };
            var newPet = new Pet { Id = Guid.NewGuid(), Name = "Buddy", UserId = newUser.Id, User = newUser };
            db.Pets.Add(newPet);
            db.Users.Add(newUser);
            db.SaveChanges();

            var enteredOffer = new PublicOffer(newPet);
            db.PublicOffers.Add(enteredOffer);
            db.SaveChanges();

            // Act
            var outputedOffers = publicOfferContext.ReadAll(useNavigationalProperties: true);

            // Assert
            Assert.IsNotNull(outputedOffers.First().Pet, "ReadAll method doesn't include navigational properties when specified!");
        }

        [Test]
        public void ReadMethod_ReturnsNullWhenPublicOfferDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            Assert.That(publicOfferContext.Read(nonExistentId), Is.EqualTo(null), "Read method doesn't return null when the public offer does not exist in the database!");
        }

        [Test]
        public void DeleteMethod_ThrowsExceptionWhenPublicOfferDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => publicOfferContext.Delete(nonExistentId), "Delete method doesn't throw an exception when the public offer does not exist in the database!");
        }
    }
}
