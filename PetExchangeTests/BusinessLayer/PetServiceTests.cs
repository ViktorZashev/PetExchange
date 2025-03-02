using BusinessLayer.Functions;
using BusinessLayer.Models;
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
		public void CreateMethod_CreatesPetInDatabase()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			db.Users.Add(user);
			db.SaveChanges();

			// Act
			PetService.Create(pet);
			var result = db.Pets.Find(pet.Id);

			// Assert
			Assert.IsNotNull(result, "Pet not found in database after creation.");
		}


		[Test]
		public void ReadMethod_ReturnsCorrectPet()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();

			// Act
			var result = PetService.Read(pet.Id);

			// Assert
			Assert.That(result, Is.Not.Null, "Read method does not return the pet from the database.");
			Assert.That(result.Name, Is.EqualTo(pet.Name), "Read method returns incorrect pet name.");
		}

		[Test]
		public void ReadMethod_ReturnsNullWhenPetDoesNotExist()
		{
			// Act
			var result = PetService.Read(Guid.NewGuid());

			// Assert
			Assert.That(result, Is.Null, "Read method does not return null when the pet does not exist in the database.");
		}

		[Test]
		public void ReadAllMethod_ReturnsAllPets()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet1 = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			var pet2 = new Pet(user, "Buddy", "", 3, "Dog", "A friendly dog", false);
			db.Users.Add(user);
			db.Pets.Add(pet1);
			db.Pets.Add(pet2);
			db.SaveChanges();

			// Act
			var result = PetService.ReadAll();

			// Assert
			Assert.That(result, Has.Count.EqualTo(2), "ReadAll method does not return all pets from the database.");
		}

		[Test]
		public void UpdateMethod_UpdatesPetInDatabase()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();
			pet.Name = "Updated Fluffy";

			// Act
			PetService.Update(pet);
			var result = db.Pets.Find(pet.Id);

			// Assert
			Assert.IsNotNull(result, "Pet not found in database after update.");
			Assert.That(result.Name, Is.EqualTo("Updated Fluffy"), "Pet name was not updated in the database.");
		}
		

		[Test]
		public void UpdateMethod_ThrowsExceptionWhenPetDoesNotExist()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(Guid.NewGuid(), user, "Nonexistent Pet", "", 2, "Cat", "A cute cat", false);

			// Act & Assert
			Assert.Throws<ArgumentException>(() => PetService.Update(pet), "Update method does not throw an exception when the pet does not exist in the database.");
		}

		[Test]
		public void DeleteMethod_DeletesPetFromDatabase()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();

			// Act
			PetService.Delete(pet.Id);
			var result = db.Pets.Find(pet.Id);

			// Assert
			Assert.IsNull(result, "Pet was not deleted from database!");
		}

        [Test]
        public void DeleteMethod_RemovesPetForGivenNameAndUser()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();

			// Act
			PetService.Delete("Fluffy", user);

			// Assert
			var deletedPet = db.Pets.FirstOrDefault(p => p.Name == "Fluffy");
			Assert.IsNull(deletedPet, "The pet 'Fluffy' should have been deleted.");
		}
		[Test]
		public void DeleteAllMethod_RemovesAllPetsFromDatabase()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet1 = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			var pet2 = new Pet(user, "Max", "", 3, "Dog", "A playful dog", true);
			db.Users.Add(user);
			db.Pets.AddRange(new[] { pet1, pet2 });
			db.SaveChanges();

			// Act
			PetService.DeleteAll();

			// Assert
			var count = db.Pets.Count();
			Assert.That(count, Is.EqualTo(0), "All pets should have been deleted from the database.");
		}

		[Test]
		public void CheckPetExistsMethod_ReturnsTrueIfPetExists()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();

			// Act
			var result = PetService.CheckPetExists("Fluffy");

			// Assert
			Assert.That(result, Is.True, "Pet 'Fluffy' should exist in the database.");
		}

		[Test]
		public void ReturnAllPetsForUserMethod_ReturnsPetsBelongingToSpecificUser()
		{
			// Arrange
			var user1 = new User { Id = Guid.NewGuid(), Name = "John" };
			var user2 = new User { Id = Guid.NewGuid(), Name = "Alice" };
			var pet1 = new Pet(user1, "Fluffy", "", 2, "Cat", "A cute cat", false);
			var pet2 = new Pet(user1, "Max", "", 3, "Dog", "A playful dog", true);
			var pet3 = new Pet(user2, "Buddy", "", 4, "Dog", "A loyal dog", false);
			db.Users.AddRange([user1, user2]);
			db.Pets.AddRange([pet1, pet2, pet3]);
			db.SaveChanges();

			// Act
			var user1Pets = PetService.ReturnAllPets(user1);
			var user2Pets = PetService.ReturnAllPets(user2);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(user1Pets, Has.Count.EqualTo(2), "There should be 2 pets belonging to user 'John'.");
                Assert.That(user2Pets, Has.Count.EqualTo(1), "There should be 1 pet belonging to user 'Alice'.");
            });
        }

		[Test]
		public void ReturnAllPetsMethod_ReturnsAllPets()
		{
			// Arrange
			var user1 = new User { Id = Guid.NewGuid(), Name = "John" };
			var user2 = new User { Id = Guid.NewGuid(), Name = "Alice" };
			var pet1 = new Pet(user1, "Fluffy", "", 2, "Cat", "A cute cat", false);
			var pet2 = new Pet(user1, "Max", "", 3, "Dog", "A playful dog", true);
			var pet3 = new Pet(user2, "Buddy", "", 4, "Dog", "A loyal dog", false);
			db.Users.AddRange([user1, user2]);
			db.Pets.AddRange([pet1, pet2, pet3]);
			db.SaveChanges();

			// Act
			var pets = PetService.ReturnAllPets();

			// Assert
			Assert.That(pets, Has.Count.EqualTo(3), "There should be 3 pets in total.");
		}

		[Test]
		public void ReturnPetByNameMethod_ReturnsPetWithGivenName()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();

			// Act
			var result = PetService.ReturnPetByname("Fluffy");

			// Assert
			Assert.IsNotNull(result, "Pet 'Fluffy' should exist in the database.");
			Assert.That(result.Name, Is.EqualTo("Fluffy"), "Returned pet should have name 'Fluffy'.");
		}


		[Test]
		public void OutputPetsMethod_OutputsPetInformation()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid(), Name = "John" };
			var pet = new Pet(user, "Fluffy", "", 2, "Cat", "A cute cat", false);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();

			// Redirect console output
			using (var sw = new System.IO.StringWriter())
			{
				Console.SetOut(sw);

				// Act
				PetService.OutputPets();
				var result = sw.ToString().Trim();

				// Assert
				StringAssert.Contains("Name: Fluffy", result, "OutputPets method does not output the pet's information.");
			}
		}

	}
}
