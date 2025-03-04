using DataLayer;

namespace PetExchangeTests
{
    public class PetDbContextTests : DataLayerTestsManagement
	{
		[Test]
		public async Task CreateMethod_AddsPetModelToDatabase()
		{
			// Arrange
			var newUser = new User();
			var newPet = new Pet(newUser, "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false);

			var initialCount = db.Pets.Count();

			// Act
			await petContext.CreateAsync(newPet);
			var actualCount = db.Pets.Count();
			var expectedCount = initialCount + 1;

			// Assert
			Assert.That(expectedCount, Is.EqualTo(actualCount), "The count of existing pets in database doesn't increment by 1 when adding one pet model!");
		}

		[Test]
		public async Task CreateMethod_ThrowsExceptionWhenTryingToAddDuplicateKey()
		{
			// Arrange
			var user = new User();
			var matchingId = Guid.NewGuid();
			var newPet = new Pet(user, "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false);
			var duplicatePet = new Pet(user, "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false);
			newPet.Id = matchingId;
			duplicatePet.Id = matchingId;

			// Act & Assert
			await petContext.CreateAsync(newPet);
			Assert.Throws<InvalidOperationException>(() =>  petContext.CreateAsync(duplicatePet));
		}

		[Test]
		public async Task ReadMethod_RetrievesAPetModelFromDatabase()
		{
			// Arrange
			var id = Guid.NewGuid();
			var enteredPet = new Pet(new User(), "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false) { Id = id };
			enteredPet.Id = id;
			// Act
			db.Pets.Add(enteredPet);
			db.SaveChanges();
			var actualPet = await petContext.ReadAsync(id);

			// Assert
			Assert.That(actualPet, Is.EqualTo(enteredPet), "Read method doesn't return the pet model entered in database!");
		}

		[Test]
		public async Task UpdateMethod_UpdatesPetModelInDatabase()
		{
			// Arrange
			var id = Guid.NewGuid();
			var initialPet = new Pet(new User(), "Initial Name", "", 2, PetTypeEnum.Cat, "A cute cat", false) { Id = id };
			var updatedPet = new Pet(new User(), "Updated Name", "", 2, PetTypeEnum.Cat, "A cute cat", false) { Id = id };

			db.Pets.Add(initialPet);
			db.SaveChanges();

			// Act
			await petContext.UpdateAsync(updatedPet);
			var actualPet = db.Pets.FirstOrDefault(p => p.Id == id);

			// Assert
			Assert.That(actualPet.Name, Is.EqualTo(updatedPet.Name), "Update method doesn't update the pet model in the database!");
		}

		[Test]
		public async Task UpdateMethod_ThrowsExceptionWhenPetDoesNotExist()
		{
			// Arrange
			var nonExistentId = Guid.NewGuid();
			var petToUpdate = new Pet(new User(), "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false) ;

			// Act & Assert
			Assert.Throws<ArgumentException>(() => petContext.UpdateAsync(petToUpdate), "Update method doesn't throw an exception when the pet does not exist in the database!");
		}

		[Test]
		public async Task DeleteMethod_RemovesPetModelFromDatabase()
		{
			// Arrange
			var id = Guid.NewGuid();
			var petToDelete = new Pet(new User(), "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false) { Id = id };

			db.Pets.Add(petToDelete);
			db.SaveChanges();

			// Act
			await petContext.DeleteAsync(id);
			var actualPet = db.Pets.FirstOrDefault(p => p.Id == id);

			// Assert
			Assert.IsNull(actualPet, "Delete method doesn't remove the pet model from the database!");
		}

		[Test]
		public async Task ReadAllMethod_RetrievesAllPetModelsFromDatabase()
		{
			// Arrange
			var user = new User();
			var enteredPet1 = new Pet(user, "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false);
			var enteredPet2 = new Pet(user, "Buddy", "", 3, PetTypeEnum.Dog, "A friendly dog", true);
			db.Pets.Add(enteredPet1);
			db.Pets.Add(enteredPet2);
			db.SaveChanges();

			// Act
			var outputedPets = await petContext.ReadAllAsync();

			// Assert
			Assert.That(outputedPets.Count, Is.EqualTo(2), "ReadAll method doesn't return all entries found in the database!");
			Assert.IsTrue(outputedPets.Any(p => p.Name == "Fluffy"), "ReadAll method doesn't return the correct pet models from the database!");
			Assert.IsTrue(outputedPets.Any(p => p.Name == "Buddy"), "ReadAll method doesn't return the correct pet models from the database!");
		}

		[Test]
		public async Task ReadAllMethod_ReturnsEmptyListWhenThereAreNoPetModelsInDatabase()
		{
			// Arrange
			// Ensure database is empty

			// Act
			var outputedPets = await petContext.ReadAllAsync();

			// Assert
			Assert.That(outputedPets, Is.Empty, "ReadAll method doesn't return an empty list when no entries exist in the database!");
		}

		[Test]
		public async Task ReadAllMethod_IncludesNavigationalPropertiesWhenSpecified()
		{
			// Arrange
			var user = new User();
			var enteredPet = new Pet(user, "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false);
			db.Pets.Add(enteredPet);
			db.SaveChanges();

			// Act
			var outputedPets = await petContext.ReadAllAsync(useNavigationalProperties: true);

			// Assert
			Assert.IsNotNull(outputedPets.First().User, "ReadAll method doesn't include navigational properties when specified!");
		}

		

		[Test]
		public async Task ReadMethod_ReturnsNullWhenPetDoesNotExist()
		{
			// Arrange
			var nonExistentId = Guid.NewGuid();

			// Act & Assert
			Assert.That(petContext.ReadAsync(nonExistentId),Is.EqualTo(null), "Read method doesn't throw an exception when the pet does not exist in the database!");
		}


		[Test]
		public async Task DeleteMethod_ThrowsExceptionWhenPetDoesNotExist()
		{
			// Arrange
			var nonExistentId = Guid.NewGuid();

			// Act & Assert
			Assert.Throws<ArgumentException>(() => petContext.DeleteAsync(nonExistentId), "Delete method doesn't throw an exception when the pet does not exist in the database!");
		}
	}
}
