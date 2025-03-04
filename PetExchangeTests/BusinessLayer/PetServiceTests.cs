using BusinessLayer.Functions;
using BusinessLayer.Models;
using DataLayer.Models;
using DataLayer.ModelsDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests.BusinessLayer
{
    public class PetServiceTests : BusinessLayerTestsManagement
    {
		[Test]
		public async Task CreateMethod_CreatesPetInDatabase()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false);
			db.Users.Add(user);
			db.SaveChanges();

			// Act
			await _petService.CreateAsync(pet);
			var result = db.Pets.Find(pet.Id);

			// Assert
			Assert.IsNotNull(result, "Pet not found in database after creation.");
		}


		[Test]
		public async Task ReadMethod_ReturnsCorrectPet()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();

			// Act
			var result = await _petService.ReadAsync(pet.Id);

			// Assert
			Assert.That(result, Is.Not.Null, "Read method does not return the pet from the database.");
			Assert.That(result.Name, Is.EqualTo(pet.Name), "Read method returns incorrect pet name.");
		}

		[Test]
		public async Task ReadMethod_ReturnsNullWhenPetDoesNotExist()
		{
			// Act
			var result = await _petService.ReadAsync(Guid.NewGuid());

			// Assert
			Assert.That(result, Is.Null, "Read method does not return null when the pet does not exist in the database.");
		}

		[Test]
		public async Task ReadAllMethod_ReturnsAllPets()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet1 = new Pet(user, "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false);
			var pet2 = new Pet(user, "Buddy", "", 3, PetTypeEnum.Dog, "A friendly dog", false);
			db.Users.Add(user);
			db.Pets.Add(pet1);
			db.Pets.Add(pet2);
			db.SaveChanges();

			// Act
			var result = await _petService.ReadAllAsync();

			// Assert
			Assert.That(result, Has.Count.EqualTo(2), "ReadAll method does not return all pets from the database.");
		}

		[Test]
		public async Task UpdateMethod_UpdatesPetInDatabase()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();
			pet.Name = "Updated Fluffy";

			// Act
			await _petService.UpdateAsync(pet);
			var result = db.Pets.Find(pet.Id);

			// Assert
			Assert.IsNotNull(result, "Pet not found in database after update.");
			Assert.That(result.Name, Is.EqualTo("Updated Fluffy"), "Pet name was not updated in the database.");
		}
		

		[Test]
		public async Task UpdateMethod_ThrowsExceptionWhenPetDoesNotExist()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Nonexistent Pet", "", 2, PetTypeEnum.Cat, "A cute cat", false);

			// Act & Assert
			Assert.ThrowsAsync<ArgumentException>(() => _petService.UpdateAsync(pet), "Update method does not throw an exception when the pet does not exist in the database.");
		}

		[Test]
		public async Task DeleteMethod_DeletesPetFromDatabase()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();

			// Act
			await _petService.DeleteAsync(pet.Id);
			var result = db.Pets.Find(pet.Id);

			// Assert
			Assert.IsNull(result, "Pet was not deleted from database!");
		}
	}
}
