using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests.DataLayer
{
    public class TownDbContextTests : DataLayerTestsManagement
    {
        [Test]
        public void CreateMethod_AddsTownToDatabase()
        {
            // Arrange
            var newTown = new Town("TownName");
            var initialCount = db.Towns.Count();

            // Act
            townContext.Create(newTown);
            var actualCount = db.Towns.Count();
            var expectedCount = initialCount + 1;

            // Assert
            Assert.That(expectedCount, Is.EqualTo(actualCount), "The count of towns in the database doesn't increment by 1 when adding one town!");
        }

        [Test]
        public void CreateMethod_ThrowsExceptionWhenTryingToAddDuplicateTown()
        {
            // Arrange
            var newTown = new Town("TownName");
            townContext.Create(newTown);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => townContext.Create(newTown), "The Create method allows adding duplicate town names!");
        }

        [Test]
        public void ReadMethod_RetrievesATownFromDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var enteredTown = new Town("TownName") { Id = id };
            db.Towns.Add(enteredTown);
            db.SaveChanges();

            // Act
            var actualTown = townContext.Read(id);
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualTown.Id, Is.EqualTo(enteredTown.Id), "Read method doesn't return the town entered in the database!");
                Assert.That(actualTown.Name, Is.EqualTo(enteredTown.Name), "Read method doesn't return the correct town name!");
            });
        }

        [Test]
        public void UpdateMethod_UpdatesTownInDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var initialTown = new Town("InitialTownName") { Id = id };
            db.Towns.Add(initialTown);
            db.SaveChanges();

            var updatedTown = new Town("UpdatedTownName") { Id = id };

            // Act
            townContext.Update(updatedTown);
            var actualTown = db.Towns.FirstOrDefault(t => t.Id == id);

            // Assert
            Assert.That(actualTown.Name, Is.EqualTo(updatedTown.Name), "Update method doesn't update the town name in the database!");
        }

        [Test]
        public void UpdateMethod_ThrowsExceptionWhenTownDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            var townToUpdate = new Town("TownName") { Id = nonExistentId };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => townContext.Update(townToUpdate), "Update method doesn't throw an exception when the town does not exist in the database!");
        }

        [Test]
        public void DeleteMethod_RemovesTownFromDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var townToDelete = new Town("TownName") { Id = id };
            db.Towns.Add(townToDelete);
            db.SaveChanges();

            // Act
            townContext.Delete(id);
            var actualTown = db.Towns.FirstOrDefault(t => t.Id == id);

            // Assert
            Assert.IsNull(actualTown, "Delete method doesn't remove the town from the database!");
        }

        [Test]
        public void ReadAllMethod_RetrievesAllTownsFromDatabase()
        {
            // Arrange
            var enteredTown1 = new Town("TownName1");
            var enteredTown2 = new Town("TownName2");
            db.Towns.Add(enteredTown1);
            db.Towns.Add(enteredTown2);
            db.SaveChanges();

            // Act
            var outputedTowns = townContext.ReadAll();

            // Assert
            Assert.That(outputedTowns, Has.Count.EqualTo(2), "ReadAll method doesn't return all entries found in the database!");
            Assert.That(outputedTowns.Any(t => t.Name == "TownName1"), Is.True, "ReadAll method doesn't return the correct town models from the database!");
            Assert.That(outputedTowns.Any(t => t.Name == "TownName2"), Is.True, "ReadAll method doesn't return the correct town models from the database!");
        }

        [Test]
        public void ReadAllMethod_ReturnsEmptyListWhenThereAreNoTownsInDatabase()
        {
            // Arrange
            // Database is empty because of setup function

            // Act
            var outputedTowns = townContext.ReadAll();

            // Assert
            Assert.That(outputedTowns, Is.Empty, "ReadAll method doesn't return an empty list when no entries exist in the database!");
        }

        [Test]
        public void ReadMethod_ReturnsNullWhenTownDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            Assert.That(townContext.Read(nonExistentId), Is.EqualTo(null), "Read method doesn't return null when the town does not exist in the database!");
        }

        [Test]
        public void DeleteMethod_ThrowsExceptionWhenTownDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => townContext.Delete(nonExistentId), "Delete method doesn't throw an exception when the town does not exist in the database!");
        }

        [Test]
        public void CheckExistsMethod_ReturnsTrueIfTownExists()
        {
            // Arrange
          
            var townName = "ExistingTown";
            var newTown = new Town(townName);
            db.Towns.Add(newTown);
            db.SaveChanges();

            // Act
            var townExists = townContext.CheckExists(townName);

            // Assert
            Assert.That(townExists, Is.True, "CheckExists method doesn't return true for a town that exists in the database!");
        }

        [Test]
        public void CheckExistsMethod_ReturnsFalseIfTownDoesNotExist()
        {
            // Arrange
            var nonExistentTownName = "NonExistentTown";

            // Act
            var townExists = townContext.CheckExists(nonExistentTownName);

            // Assert
            Assert.That(townExists, Is.False, "CheckExists method doesn't return false for a town that does not exist in the database!");
        }
    }
}
