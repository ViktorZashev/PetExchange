using DataLayer;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;

namespace PetExchangeTests
{
    public class PetDbContextTests : DataLayerTestsManagement
	{
        [Test]
        public async Task CreateAsync_SinglePet_Succeeds()
        {
            // Arrange
            var user = await GetExampleUser(true);

            var pet = new Pet
            {
                Name = "Buddy",
                Breed = "Golden Retriever",
                PetType = PetTypeEnum.Dog,
                Gender = GenderEnum.Male,
                UserId = user.Id,
                IsActive = true
            };

            // Act
            await petContext.CreateAsync(pet);

            // Assert
            var createdPet = db.Pets.FirstOrDefault(p => p.Name == "Buddy");
            Assert.NotNull(createdPet);
        }

        [Test]
        public async Task CreateAsync_ListOfPets_AddsAllToDatabase()
        {
            // Arrange: Create a list of pets
            var user = await GetExampleUser(true);


            var pet1 = new Pet { Name = "Buddy", Breed = "Golden Retriever", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male, UserId = user.Id, IsActive = true };
            var pet2 = new Pet { Name = "Buddy", Breed = "Golden Retriever", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male, UserId = user.Id, IsActive = true };
            var pet3 = new Pet { Name = "Buddy", Breed = "Golden Retriever", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male, UserId = user.Id, IsActive = true };

            var pets = new List<Pet>
            {
               pet1,pet2,pet3
            };

            // Act: Call the CreateAsync(List<Pet>) method
            await petContext.CreateAsync(pets);

            // Assert: Ensure all pets are added to the database
            var petsInDb = await db.Pets.ToListAsync();
            Assert.AreEqual(3, petsInDb.Count, "All pets should be added to the database");

            // Verify each pet exists in the database
            foreach (var pet in pets)
            {
                Assert.IsTrue(petsInDb.Any(p => p.Id == pet.Id && p.Name == pet.Name),
                    $"Pet {pet.Name} should be in the database");
            }
        }
        [Test]
        public async Task ReadAllWithFilterAsync_FiltersByName_Succeeds()
        {
            // Arrange
            var user = await GetExampleUser(true);

            var pet1 = new Pet { Name = "Buddy", Breed = "Golden Retriever", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male, UserId = user.Id, IsActive = true };
            var pet2 = new Pet { Name = "Max", Breed = "Bulldog", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male, UserId = user.Id, IsActive = true };

            await petContext.CreateAsync(pet1);
            await petContext.CreateAsync(pet2);

            // Act
            var result = await petContext.ReadAllWithFilterAsync("Buddy", "", "", "", "", 1, 10, useNavigationalProperties: false);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Buddy", result.First().Name);
        }

        [Test]
        public async Task ReadAsync_ExistingPet_ReturnsPet()
        {
            // Arrange
            var user = await GetExampleUser(true);


            var pet = new Pet { Name = "Buddy", Breed = "Golden Retriever", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male, UserId = user.Id, IsActive = true };
            await petContext.CreateAsync(pet);

            // Act
            var retrievedPet = await petContext.ReadAsync(pet.Id, useNavigationalProperties: false);

            // Assert
            Assert.NotNull(retrievedPet);
            Assert.AreEqual("Buddy", retrievedPet.Name);
        }

        [Test]
        public async Task ReadAsync_NonExistentPet_ReturnsNull()
        {
            // Act
            var retrievedPet = await petContext.ReadAsync(Guid.NewGuid(), useNavigationalProperties: false);

            // Assert
            Assert.IsNull(retrievedPet);
        }

        [Test]
        public async Task UpdateAsync_ExistingPet_UpdatesSuccessfully()
        {
            // Arrange
            var user = await GetExampleUser(true);

            var pet = new Pet { Name = "Buddy", Breed = "Golden Retriever", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male, UserId = user.Id, IsActive = true };
            await petContext.CreateAsync(pet);

            pet.Name = "UpdatedBuddy";

            // Act
            await petContext.UpdateAsync(pet, useNavigationalProperties: false);

            // Assert
            var updatedPet = db.Pets.FirstOrDefault(p => p.Id == pet.Id);
            Assert.NotNull(updatedPet);
            Assert.AreEqual("UpdatedBuddy", updatedPet.Name);
        }

        [Test]
        public async Task DeleteAsync_ExistingPet_SetsInactive()
        {
            // Arrange
            var user = await GetExampleUser(true);

            var pet = new Pet { Name = "Buddy", Breed = "Golden Retriever", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male, UserId = user.Id, IsActive = true };
            await petContext.CreateAsync(pet);

            // Act
            await petContext.DeleteAsync(pet.Id);

            // Assert
            var deletedPet = db.Pets.FirstOrDefault(p => p.Id == pet.Id);
            Assert.NotNull(deletedPet);
            Assert.IsFalse(deletedPet.IsActive);
        }

        [Test]
        public void DeleteAsync_NonExistentPet_ThrowsArgumentException()
        {
            // Arrange
            var nonExistentPetId = Guid.NewGuid(); // Generate a GUID that does not exist in the database

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await petContext.DeleteAsync(nonExistentPetId));

            // Assert
            Assert.AreEqual("Pet with id = " + nonExistentPetId + " does not exist!", ex.Message);
        }

        [Test]
        public async Task ReadAllWithFilterAsyncOfUser_FiltersByUserId_Succeeds()
        {
            // Arrange
            var user1 = await GetExampleUser(true);

            var user2 = await GetExampleUser(true);

            var pet1 = new Pet { Name = "Buddy", Breed = "Golden Retriever", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male, UserId = user1.Id, IsActive = true };
            var pet2 = new Pet { Name = "Max", Breed = "Bulldog", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male, UserId = user2.Id, IsActive = true };

            await petContext.CreateAsync(pet1);
            await petContext.CreateAsync(pet2);

            // Act
            var result = await petContext.ReadAllWithFilterAsyncOfUser(user1.Id, "", "", "", "", 1, 10, useNavigationalProperties: true);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Buddy", result.First().Name);
            Assert.AreEqual(user1.Id, result.First().UserId);
        }

        [Test]
        public async Task ReadWithFiltersAsync_FiltersByPetType_Succeeds()
        {
            // Arrange
            var user = await GetExampleUser(true);

            var pet1 = new Pet { Name = "Buddy", Breed = "Golden Retriever", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male, UserId = user.Id, IsActive = true };
            var pet2 = new Pet { Name = "Max", Breed = "Bulldog", PetType = PetTypeEnum.Cat, Gender = GenderEnum.Male, UserId = user.Id, IsActive = true };

            await petContext.CreateAsync(pet1);
            await petContext.CreateAsync(pet2);

            // Act
            var result = await petContext.ReadWithFiltersAsync(new List<PetTypeEnum> { PetTypeEnum.Dog }, new List<GenderEnum>(), new List<PetAgeEnum>(), null, 1, 10);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Buddy", result.First().Name);
            Assert.AreEqual(PetTypeEnum.Dog, result.First().PetType);
        }

        [Test]
        public async Task Read4NewestAsync_ReturnsNewestPets_Succeeds()
        {
            // Arrange
            var user = await GetExampleUser(true);

            var pet1 = new Pet { Name = "Buddy", Breed = "Golden Retriever", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male,User = user, UserId = user.Id, IsActive = true, AddedOn = DateTime.Now.AddDays(-1) };
            var pet2 = new Pet { Name = "Max", Breed = "Bulldog", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male, User = user, UserId = user.Id, IsActive = true, AddedOn = DateTime.Now.AddDays(-2) };

            await petContext.CreateAsync(pet1);
            await petContext.CreateAsync(pet2);

            // Act
            var result = await petContext.Read4NewestAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Buddy", result.First().Name);
        }

        [Test]
        public async Task Read4OldestAsync_ReturnsOldestPets_Succeeds()
        {
            // Arrange
            var user = await GetExampleUser(true);

            var pet1 = new Pet { Name = "Buddy", Breed = "Golden Retriever", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male, User = user, UserId = user.Id, IsActive = true, AddedOn = DateTime.Now.AddDays(-1) };
            var pet2 = new Pet { Name = "Max", Breed = "Bulldog", PetType = PetTypeEnum.Dog, Gender = GenderEnum.Male, User = user, UserId = user.Id, IsActive = true, AddedOn = DateTime.Now.AddDays(-2) };

            await petContext.CreateAsync(pet1);
            await petContext.CreateAsync(pet2);

            // Act
            var result = await petContext.Read4OldestAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Max", result.First().Name);
        }
    }
}
