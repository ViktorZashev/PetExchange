using DataLayer;

namespace PetExchangeTests
{
    public class TownDbContextTests : DataLayerTestsManagement
    {
        [Test]
        public async Task CreateMethod_AddsTownToDatabase()
        {
            // Arrange
            var newTown = new Town("TownName");
            var initialCount = db.Towns.Count();

            // Act
            await townContext.CreateAsync(newTown);
            var actualCount = db.Towns.Count();
            var expectedCount = initialCount + 1;

            // Assert
            Assert.That(expectedCount, Is.EqualTo(actualCount), "The count of towns in the database doesn't increment by 1 when adding one town!");
        }

        [Test]
        public async Task CreateMethod_ThrowsExceptionWhenTryingToAddDuplicateTown()
        {
            // Arrange
            var newTown = new Town("TownName");
            await townContext.CreateAsync(newTown);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => townContext.CreateAsync(newTown), "The Create method allows adding duplicate town names!");
        }

        [Test]
        public async Task ReadMethod_RetrievesATownFromDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var enteredTown = new Town("TownName") { Id = id };
            db.Towns.Add(enteredTown);
            db.SaveChanges();

            // Act
            var actualTown = await townContext.ReadAsync(id);
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actualTown.Id, Is.EqualTo(enteredTown.Id), "Read method doesn't return the town entered in the database!");
                Assert.That(actualTown.Name, Is.EqualTo(enteredTown.Name), "Read method doesn't return the correct town name!");
            });
        }

        [Test]
        public async Task UpdateMethod_UpdatesTownInDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var initialTown = new Town("InitialTownName") { Id = id };
            db.Towns.Add(initialTown);
            db.SaveChanges();

            var updatedTown = new Town("UpdatedTownName") { Id = id };

            // Act
            await townContext.UpdateAsync(updatedTown);
            var actualTown = db.Towns.FirstOrDefault(t => t.Id == id);

            // Assert
            Assert.That(actualTown.Name, Is.EqualTo(updatedTown.Name), "Update method doesn't update the town name in the database!");
        }

        [Test]
        public async Task UpdateMethod_ThrowsExceptionWhenTownDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            var townToUpdate = new Town("TownName") { Id = nonExistentId };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => townContext.UpdateAsync(townToUpdate), "Update method doesn't throw an exception when the town does not exist in the database!");
        }

        [Test]
        public async Task DeleteMethod_RemovesTownFromDatabase()
        {
            // Arrange
            var id = Guid.NewGuid();
            var townToDelete = new Town("TownName") { Id = id };
            db.Towns.Add(townToDelete);
            db.SaveChanges();

            // Act
            await townContext.DeleteAsync(id);
            var actualTown = db.Towns.FirstOrDefault(t => t.Id == id);

            // Assert
            Assert.IsNull(actualTown, "Delete method doesn't remove the town from the database!");
        }

        [Test]
        public async Task ReadAllMethod_RetrievesAllTownsFromDatabase()
        {
            // Arrange
            var enteredTown1 = new Town("TownName1");
            var enteredTown2 = new Town("TownName2");
            db.Towns.Add(enteredTown1);
            db.Towns.Add(enteredTown2);
            db.SaveChanges();

            // Act
            var outputedTowns = await townContext.ReadAllAsync();

            // Assert
            Assert.That(outputedTowns, Has.Count.EqualTo(2), "ReadAll method doesn't return all entries found in the database!");
            Assert.That(outputedTowns.Any(t => t.Name == "TownName1"), Is.True, "ReadAll method doesn't return the correct town models from the database!");
            Assert.That(outputedTowns.Any(t => t.Name == "TownName2"), Is.True, "ReadAll method doesn't return the correct town models from the database!");
        }

        [Test]
        public async Task ReadAllMethod_ReturnsEmptyListWhenThereAreNoTownsInDatabase()
        {
            // Arrange
            // Database is empty because of setup function

            // Act
            var outputedTowns = await townContext.ReadAllAsync();

            // Assert
            Assert.That(outputedTowns, Is.Empty, "ReadAll method doesn't return an empty list when no entries exist in the database!");
        }

        [Test]
        public async Task ReadMethod_ReturnsNullWhenTownDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            Assert.That(townContext.ReadAsync(nonExistentId), Is.EqualTo(null), "Read method doesn't return null when the town does not exist in the database!");
        }

        [Test]
        public async Task DeleteMethod_ThrowsExceptionWhenTownDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => townContext.DeleteAsync(nonExistentId), "Delete method doesn't throw an exception when the town does not exist in the database!");
        }
    }
}
