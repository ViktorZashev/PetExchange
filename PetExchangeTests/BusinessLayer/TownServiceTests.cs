using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace PetExchangeTests
{
    public class TownServiceTests : BusinessLayerTestsManagement
    {
        [Test]
        public async Task CreateAsync_WhenCalled_AddsTownToDatabase()
        {
            // Arrange
            var town = new Town { Id = Guid.NewGuid(), Name = "Sofia" };

            // Act
            await _townService.CreateAsync(town);
            var result = await db.Towns.FirstOrDefaultAsync(t => t.Id == town.Id);

            // Assert
            Assert.IsNotNull(result, "Town should be added to the database");
            Assert.AreEqual("Sofia", result.Name);
        }

        [Test]
        public async Task CreateAsync_WithListOfTowns_AddsAllTowns()
        {
            // Arrange
            var towns = new List<Town>
            {
                new Town { Id = Guid.NewGuid(), Name = "Sofia" },
                new Town { Id = Guid.NewGuid(), Name = "Plovdiv" }
            };

            // Act
            await _townService.CreateAsync(towns);
            var result = await db.Towns.ToListAsync();

            // Assert
            Assert.AreEqual(2, result.Count, "All towns should be added");
        }

        [Test]
        public async Task ReadAsync_WhenTownExists_ReturnsTown()
        {
            // Arrange
            var town = new Town { Id = Guid.NewGuid(), Name = "Sofia" };
            await db.Towns.AddAsync(town);
            await db.SaveChangesAsync();

            // Act
            var result = await _townService.ReadAsync(town.Id);

            // Assert
            Assert.IsNotNull(result, "Town should be retrieved");
            Assert.AreEqual("Sofia", result.Name);
        }

        [Test]
        public async Task ReadAllAsync_ReturnsAllTowns()
        {
            // Arrange
            await db.Towns.AddRangeAsync(
                new Town { Id = Guid.NewGuid(), Name = "Sofia" },
                new Town { Id = Guid.NewGuid(), Name = "Plovdiv" }
            );
            await db.SaveChangesAsync();

            // Act
            var result = await _townService.ReadAllAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task UpdateAsync_WhenTownExists_UpdatesDatabase()
        {
            // Arrange
            var town = new Town { Id = Guid.NewGuid(), Name = "OldTown" };
            await db.Towns.AddAsync(town);
            await db.SaveChangesAsync();

            // Act
            town.Name = "NewTown";
            await _townService.UpdateAsync(town);
            var updatedTown = await db.Towns.FirstOrDefaultAsync(t => t.Id == town.Id);

            // Assert
            Assert.AreEqual("NewTown", updatedTown.Name);
        }

        [Test]
        public async Task DeleteAsync_WhenTownExists_RemovesFromDatabase()
        {
            // Arrange
            var town = new Town { Id = Guid.NewGuid(), Name = "ToDelete" };
            await db.Towns.AddAsync(town);
            await db.SaveChangesAsync();

            // Act
            await _townService.DeleteAsync(town.Id);
            var deletedTown = await db.Towns.FirstOrDefaultAsync(t => t.Id == town.Id);

            // Assert
            Assert.IsNull(deletedTown, "Town should be removed from the database");
        }

        [Test]
        public async Task GetTownOptions_ReturnsSortedTownOptions()
        {
            // Arrange
            var towns = new List<Town>
            {
                new Town { Id = Guid.NewGuid(), Name = "Sofia" },
                new Town { Id = Guid.NewGuid(), Name = "Plovdiv" }
            };
            await db.Towns.AddRangeAsync(towns);
            await db.SaveChangesAsync();

            // Act
            var result = await _townService.GetTownOptions();

            // Assert
            Assert.AreEqual(2, result.Count, "There should be two town options");
            Assert.AreEqual("Plovdiv", result[0].Label);
            Assert.AreEqual("Sofia", result[1].Label);
        }
    }
}
