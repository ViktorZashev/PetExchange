using DataLayer;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace PetExchangeTests
{
    public class TownDbContextTests : DataLayerTestsManagement
    {
        // Example Town creation helper method
        public Town GetExampleTown(bool saveTown = true)
        {
            var town = new Town
            {
                Name = "ExampleTown"
            };

            if (saveTown)
            {
                db.Towns.Add(town); // Save town to the database
                db.SaveChanges(); // Ensure it's saved and has a valid Id
            }
            else
            {
                town.Id = Guid.NewGuid(); // If not saving to DB, simulate a valid Id
            }

            return town;
        }

        [Test]
        public async Task CreateAsync_SingleTown_Succeeds()
        {
            // Arrange
            var town = GetExampleTown(saveTown: false); // Simulate Town creation without DB persistence

            // Act
            await townContext.CreateAsync(town);

            // Assert
            var createdTown = await db.Towns.FirstOrDefaultAsync(t => t.Name == "ExampleTown");
            Assert.NotNull(createdTown);
        }

        [Test]
        public async Task CreateAsync_DuplicateTown_ThrowsDuplicateNameException()
        {
            // Arrange
            var town = GetExampleTown(saveTown: true); // Save town to DB first
            var duplicateTown = new Town { Name = town.Name };

            // Act & Assert
            var ex = Assert.ThrowsAsync<DuplicateNameException>(async () => await townContext.CreateAsync(duplicateTown));
            Assert.AreEqual("There is already a town with the same name.", ex.Message);
        }

        [Test]
        public async Task ReadAsync_ExistingTown_ReturnsTown()
        {
            // Arrange
            var town = GetExampleTown(saveTown: true); // Ensure Town is saved to DB

            // Act
            var retrievedTown = await townContext.ReadAsync(town.Id);

            // Assert
            Assert.NotNull(retrievedTown);
            Assert.AreEqual(town.Name, retrievedTown.Name);
        }

        [Test]
        public async Task ReadAsync_NonExistentTown_ReturnsNull()
        {
            // Act
            var retrievedTown = await townContext.ReadAsync(Guid.NewGuid());

            // Assert
            Assert.IsNull(retrievedTown);
        }

        [Test]
        public async Task CreateAsync_ListOfTowns_AddsAllToDatabase()
        {
            // Arrange: Create a list of towns
            var towns = new List<Town>
            {
                new Town { Id = Guid.NewGuid(), Name = "New York" },
                new Town { Id = Guid.NewGuid(), Name = "Los Angeles" },
                new Town { Id = Guid.NewGuid(), Name = "Chicago" }
            };

            // Act: Call the CreateAsync(List<Town>) method
            await townContext.CreateAsync(towns);

            // Assert: Ensure all towns are added to the database
            var townsInDb = await db.Towns.ToListAsync();
            Assert.AreEqual(3, townsInDb.Count, "All towns should be added to the database");

            // Verify each town exists in the database
            foreach (var town in towns)
            {
                Assert.IsTrue(townsInDb.Any(t => t.Id == town.Id && t.Name == town.Name),
                    $"Town {town.Name} should be in the database");
            }
        }

        [Test]
        public async Task ReadAllAsync_WhenCalled_ReturnsAllTowns()
        {
            // Arrange: Add towns to the database
            var towns = new List<Town>
            {
                new Town { Id = Guid.NewGuid(), Name = "Miami" },
                new Town { Id = Guid.NewGuid(), Name = "Seattle" }
            };

            await db.Towns.AddRangeAsync(towns);
            await db.SaveChangesAsync();

            // Act: Call ReadAllAsync
            var result = await townContext.ReadAllAsync();

            // Assert: Verify the correct number of towns is returned
            Assert.AreEqual(2, result.Count, "Should return all towns in the database");

            // Verify the towns returned match what was inserted
            foreach (var town in towns)
            {
                Assert.IsTrue(result.Any(t => t.Id == town.Id && t.Name == town.Name),
                    $"Town {town.Name} should be in the returned list");
            }
        }

        [Test]
        public async Task UpdateAsync_ExistingTown_UpdatesSuccessfully()
        {
            // Arrange
            var town = GetExampleTown(saveTown: true); // Ensure Town is saved
            town.Name = "UpdatedTown";

            // Act
            await townContext.UpdateAsync(town);

            // Assert
            var updatedTown = await db.Towns.FirstOrDefaultAsync(t => t.Id == town.Id);
            Assert.NotNull(updatedTown);
            Assert.AreEqual("UpdatedTown", updatedTown.Name);
        }

        [Test]
        public async Task DeleteAsync_ExistingTown_Succeeds()
        {
            // Arrange
            var town = GetExampleTown(saveTown: true); // Ensure Town is saved

            // Act
            await townContext.DeleteAsync(town.Id);

            // Assert
            var deletedTown = await db.Towns.FirstOrDefaultAsync(t => t.Id == town.Id);
            Assert.IsNull(deletedTown);
        }

        [Test]
        public async Task DeleteAsync_NonExistentTown_ThrowsArgumentException()
        {
            // Arrange
            var nonExistentTownId = Guid.NewGuid(); // Generate a GUID that does not exist in DB

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await townContext.DeleteAsync(nonExistentTownId));
            Assert.AreEqual("Town with id = " + nonExistentTownId + " does not exist!", ex.Message);
        }
   
    }
}
