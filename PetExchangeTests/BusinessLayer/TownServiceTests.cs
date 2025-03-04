using DataLayer;

namespace PetExchangeTests
{
    public class TownServiceTests : BusinessLayerTestsManagement
    {
		[Test]
		public async Task Create_Adds_New_Town()
		{
			// Arrange

			var town = new Town { Id = Guid.NewGuid(), Name = "Test Town"};

			// Act
			await _townService.CreateAsync(town);

			// Assert
			var result = db.Towns.FirstOrDefault(t => t.Id == town.Id);
			Assert.IsNotNull(result, "The new town should be added.");
			Assert.That(result.Id, Is.EqualTo(town.Id), "The IDs should match.");
		}

		[Test]
		public async Task Read_Returns_Town_By_Id()
		{
			// Arrange
			var town = new Town { Id = Guid.NewGuid(), Name = "Test Town"};
			db.Towns.Add(town);
			db.SaveChanges();

			// Act
			var result = await _townService.ReadAsync(town.Id);

			// Assert
			Assert.That(result, Is.Not.Null, "The town should be found.");
			Assert.That(result.Id, Is.EqualTo(town.Id), "The IDs should match.");
		}

		[Test]
		public async Task ReadAll_Returns_All_Towns()
		{
			// Arrange
			
			var town1 = new Town { Id = Guid.NewGuid(), Name = "Test Town 1"};
			var town2 = new Town { Id = Guid.NewGuid(), Name = "Test Town 2"};
			db.Towns.AddRange(town1, town2);
			db.SaveChanges();

			// Act
			var results = await _townService.ReadAllAsync();

            // Assert
            Assert.That(results, Has.Count.EqualTo(2), "Both towns should be returned.");
		}

		[Test]
		public async Task Update_Modifies_Existing_Town()
		{
			// Arrange
			
			var town = new Town {Id = Guid.NewGuid(), Name = "Test Town"};
			db.Towns.Add(town);
			db.SaveChanges();

			var updatedName = "Updated Test Town";
			town.Name = updatedName;

            // Act
            await _townService.UpdateAsync(town);

			// Assert
			var result = db.Towns.FirstOrDefault(t => t.Id == town.Id);
			Assert.That(result, Is.Not.Null, "The town should be found.");
			Assert.That(result.Name, Is.EqualTo(updatedName), "The name should be updated.");
		}

		[Test]
		public async Task Delete_Removes_Town()
		{
			// Arrange
			var town = new Town {Id = Guid.NewGuid(), Name = "Test Town"};
			db.Towns.Add(town);
			db.SaveChanges();

            // Act
            await _townService.DeleteAsync(town.Id);

			// Assert
			var result = db.Towns.FirstOrDefault(t => t.Id == town.Id);
			Assert.IsNull(result, "The town should be deleted.");
		}
	}
}
