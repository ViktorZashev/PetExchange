using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Models;
using DataLayer.ProjectDbContext;
using DataLayer;

namespace PetExchangeTests.DataLayer
{
	public class PetDbContextTests : DataLayerTestsManagement
	{
		[Test]
		public void CreateMethod_AddsPetModelToDatabase()
		{
			// Arrange
			var newUser = new User();
			var newPet = new Pet(newUser, "Fluffy", "", 2, "Cat", "A cute cat", false);

			var initialCount = db.Pets.Count();

			// Act
			petContext.Create(newPet);
			var actualCount = db.Pets.Count();
			var expectedCount = initialCount + 1;

			// Assert
			Assert.That(expectedCount, Is.EqualTo(actualCount), "The count of existing pets in database doesn't increment by 1 when adding one pet model!");
		}

		[Test]
		public void CreateMethod_ThrowsExceptionWhenTryingToAddDuplicateKey()
		{
			// Arrange
			var user = new User();
			var matchingId = Guid.NewGuid();
			var newPet = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			var duplicatePet = new Pet(matchingId,user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			newPet.Id = matchingId;

			// Act & Assert
			petContext.Create(newPet);
			Assert.Throws<InvalidOperationException>(() => petContext.Create(duplicatePet));
		}

		[Test]
		public void ReadMethod_RetrievesAPetModelFromDatabase()
		{
			// Arrange
			var id = Guid.NewGuid();
			var enteredPet = new Pet(new User(), "Fluffy", "", 2, "Cat", "A cute cat", false) { Id = id };

			// Act
			db.Pets.Add(enteredPet);
			db.SaveChanges();
			var actualPet = petContext.Read(id);

			// Assert
			Assert.AreEqual(enteredPet, actualPet, "Read method doesn't return the pet model entered in database!");
		}

		[Test]
		public void UpdateMethod_UpdatesPetModelInDatabase()
		{
			// Arrange
			var id = Guid.NewGuid();
			var initialPet = new Pet(new User(), "Initial Name", "", 2, "Cat", "A cute cat", false) { Id = id };
			var updatedPet = new Pet(new User(), "Updated Name", "", 2, "Cat", "A cute cat", false) { Id = id };

			db.Pets.Add(initialPet);
			db.SaveChanges();

			// Act
			petContext.Update(updatedPet);
			var actualPet = db.Pets.FirstOrDefault(p => p.Id == id);

			// Assert
			Assert.AreEqual(updatedPet.Name, actualPet.Name, "Update method doesn't update the pet model in the database!");
		}

		[Test]
		public void UpdateMethod_ThrowsExceptionWhenPetDoesNotExist()
		{
			// Arrange
			var nonExistentId = Guid.NewGuid();
			var petToUpdate = new Pet(nonExistentId, new User(), "Fluffy", "", 2, "Cat", "A cute cat", false) ;

			// Act & Assert
			Assert.Throws<ArgumentException>(() => petContext.Update(petToUpdate), "Update method doesn't throw an exception when the pet does not exist in the database!");
		}

		[Test]
		public void DeleteMethod_RemovesPetModelFromDatabase()
		{
			// Arrange
			var id = Guid.NewGuid();
			var petToDelete = new Pet(new User(), "Fluffy", "", 2, "Cat", "A cute cat", false) { Id = id };

			db.Pets.Add(petToDelete);
			db.SaveChanges();

			// Act
			petContext.Delete(id);
			var actualPet = db.Pets.FirstOrDefault(p => p.Id == id);

			// Assert
			Assert.IsNull(actualPet, "Delete method doesn't remove the pet model from the database!");
		}

		[Test]
		public void ReadAllMethod_RetrievesAllPetModelsFromDatabase()
		{
			// Arrange
			var user = new User();
			var enteredPet1 = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			var enteredPet2 = new Pet(user, "Buddy", "", 3, "Dog", "A friendly dog", true);
			db.Pets.Add(enteredPet1);
			db.Pets.Add(enteredPet2);
			db.SaveChanges();

			// Act
			var outputedPets = petContext.ReadAll();

			// Assert
			Assert.That(outputedPets.Count, Is.EqualTo(2), "ReadAll method doesn't return all entries found in the database!");
			Assert.IsTrue(outputedPets.Any(p => p.Name == "Fluffy"), "ReadAll method doesn't return the correct pet models from the database!");
			Assert.IsTrue(outputedPets.Any(p => p.Name == "Buddy"), "ReadAll method doesn't return the correct pet models from the database!");
		}

		[Test]
		public void ReadAllMethod_ReturnsEmptyListWhenThereAreNoPetModelsInDatabase()
		{
			// Arrange
			// Ensure database is empty

			// Act
			var outputedPets = petContext.ReadAll();

			// Assert
			Assert.That(outputedPets, Is.Empty, "ReadAll method doesn't return an empty list when no entries exist in the database!");
		}

		[Test]
		public void ReadAllMethod_IncludesNavigationalPropertiesWhenSpecified()
		{
			// Arrange
			var user = new User();
			var enteredPet = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			db.Pets.Add(enteredPet);
			db.SaveChanges();

			// Act
			var outputedPets = petContext.ReadAll(useNavigationalProperties: true);

			// Assert
			Assert.IsNotNull(outputedPets.First().User, "ReadAll method doesn't include navigational properties when specified!");
		}

		

		[Test]
		public void ReadMethod_ReturnsNullWhenPetDoesNotExist()
		{
			// Arrange
			var nonExistentId = Guid.NewGuid();

			// Act & Assert
			Assert.That(petContext.Read(nonExistentId),Is.EqualTo(null), "Read method doesn't throw an exception when the pet does not exist in the database!");
		}


		[Test]
		public void DeleteMethod_ThrowsExceptionWhenPetDoesNotExist()
		{
			// Arrange
			var nonExistentId = Guid.NewGuid();

			// Act & Assert
			Assert.Throws<ArgumentException>(() => petContext.Delete(nonExistentId), "Delete method doesn't throw an exception when the pet does not exist in the database!");
		}
	}
}
