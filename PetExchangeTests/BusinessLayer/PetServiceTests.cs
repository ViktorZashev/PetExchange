using DataLayer;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace PetExchangeTests
{
    public class PetServiceTests : BusinessLayerTestsManagement
    {


        [SetUp]
        public void SetUp()
        {

            // Setup a collection of pets for testing
          
        }

        [Test]
        public async Task CreateAsync_WhenCalled_AddsPetToDatabase()
        {
            // Arrange
            var user = await GetExampleUser();
            var pet = new Pet { Id = Guid.NewGuid(), Name = "Buddy", Breed = "Golden Retriever", PetType = PetTypeEnum.Dog, User = user };

            // Act
            await _petService.CreateAsync(pet);
            var result = await db.Pets.FirstOrDefaultAsync(p => p.Id == pet.Id);

            // Assert
            Assert.IsNotNull(result, "Pet should be added to the database");
            Assert.AreEqual("Buddy", result.Name);
            Assert.AreEqual(user.Id, result.User.Id);
        }

        [Test]
        public async Task CreateAsync_WithListOfPets_AddsAllPets()
        {
            // Arrange
            var user = await GetExampleUser();
            var pets = new List<Pet>
            {
                new Pet { Id = Guid.NewGuid(), Name = "Milo", Breed = "Labrador", PetType = PetTypeEnum.Dog, User = user },
                new Pet { Id = Guid.NewGuid(), Name = "Whiskers", Breed = "Siamese", PetType = PetTypeEnum.Dog, User = user }
            };

            // Act
            await _petService.CreateAsync(pets);
            var result = await db.Pets.ToListAsync();

            // Assert
            Assert.AreEqual(2, result.Count, "All pets should be added");
            Assert.IsTrue(result.All(p => p.User.Id == user.Id), "All pets should be assigned to the user");
        }

        [Test]
        public async Task ReadAsync_WhenPetExists_ReturnsPet()
        {
            // Arrange
            var user = await GetExampleUser();
            var pet = new Pet { Id = Guid.NewGuid(), Name = "Charlie", Breed = "Beagle", PetType = PetTypeEnum.Dog, User = user };
            await db.Pets.AddAsync(pet);
            await db.SaveChangesAsync();

            // Act
            var result = await _petService.ReadAsync(pet.Id);

            // Assert
            Assert.IsNotNull(result, "Pet should be retrieved");
            Assert.AreEqual("Charlie", result.Name);
        }

        [Test]
        public async Task ReadAllAsync_ReturnsAllPets()
        {
            // Arrange
            var user = await GetExampleUser();
            await db.Pets.AddRangeAsync(
                new Pet { Id = Guid.NewGuid(), Name = "Max", User = user },
                new Pet { Id = Guid.NewGuid(), Name = "Luna", User = user }
            );
            await db.SaveChangesAsync();

            // Act
            var result = await _petService.ReadAllAsync();

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task ReadWithFiltersAsync_ShouldReturnFilteredPets()
        {
            // Arrange: Define the filters for the test
            var user = await GetExampleUser();
            var pets = new List<Pet>
            {
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = "Bella",
                    PetType = PetTypeEnum.Dog,
                    Gender = GenderEnum.Female,
                    Description = "Friendly dog",
                    IncludesCage = false,
                    Birthday = DateTime.Now.AddYears(-2),
                    AddedOn = DateTime.Now.AddMonths(-3),
                    UserId = user.Id,
                    User = user,
                    IsActive = true
                },
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = "Max",
                    PetType = PetTypeEnum.Cat,
                    Gender = GenderEnum.Male,
                    Description = "Curious cat",
                    IncludesCage = true,
                    Birthday = DateTime.Now.AddYears(-1),
                    AddedOn = DateTime.Now.AddMonths(-1),
                    UserId = user.Id,
                    User = user,
                    IsActive = true
                },
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = "Charlie",
                    PetType = PetTypeEnum.Dog,
                    Gender = GenderEnum.Male,
                    Description = "Loyal dog",
                    IncludesCage = true,
                    Birthday = DateTime.Now.AddMonths(-1),
                    AddedOn = DateTime.Now.AddMonths(-6),
                     UserId = user.Id,
                    User = user,
                    IsActive = true
                }
            };
            foreach (var pet in pets)
                db.Pets.Add(pet);
            db.SaveChanges();

            var types = new List<PetTypeEnum> { PetTypeEnum.Dog };
            var genders = new List<GenderEnum> { GenderEnum.Male };
            var ages = new List<PetAgeEnum> { PetAgeEnum.Young };
            bool? withCage = true;
            int page = 1;
            int pageSize = 10;

            // Act: Call the method with the filters
            var result = await _petService.ReadWithFiltersAsync(types, genders, ages, withCage, page, pageSize);

            // Assert: Check that the correct pets are returned based on the filters
            Assert.AreEqual(1, result.Count); // Only 1 dog that matches the filters
            Assert.AreEqual("Charlie", result[0].Name); // "Charlie" is the only dog with cage, male and young
        }

        [Test]
        public async Task ReadWithFiltersAsync_ShouldReturnAllPets_WhenNoFiltersAreApplied()
        {

            // Arrange: Define no filters (all pets)
            var user = await GetExampleUser();
            var pets = new List<Pet>
            {
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = "Bella",
                    PetType = PetTypeEnum.Dog,
                    Gender = GenderEnum.Female,
                    Description = "Friendly dog",
                    IncludesCage = false,
                    Birthday = DateTime.Now.AddYears(-2),
                    AddedOn = DateTime.Now.AddMonths(-3),
                    UserId = user.Id,
                    User = user,
                    IsActive = true
                },
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = "Max",
                    PetType = PetTypeEnum.Cat,
                    Gender = GenderEnum.Male,
                    Description = "Curious cat",
                    IncludesCage = true,
                    Birthday = DateTime.Now.AddYears(-1),
                    AddedOn = DateTime.Now.AddMonths(-1),
                    UserId = user.Id,
                    User = user,
                    IsActive = true
                },
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = "Charlie",
                    PetType = PetTypeEnum.Dog,
                    Gender = GenderEnum.Male,
                    Description = "Loyal dog",
                    IncludesCage = true,
                    Birthday = DateTime.Now.AddYears(-4),
                    AddedOn = DateTime.Now.AddMonths(-6),
                    UserId = user.Id,
                    User = user,
                    IsActive = true
                }
            };
            foreach (var pet in pets)
                db.Pets.Add(pet);
            db.SaveChanges();


            var types = new List<PetTypeEnum>();
            var genders = new List<GenderEnum>();
            var ages = new List<PetAgeEnum>();
            bool? withCage = null;
            int page = 1;
            int pageSize = 10;

            // Act: Call the method with no filters
            var result = await _petService.ReadWithFiltersAsync(types, genders, ages, withCage, page, pageSize);

            // Assert: All pets should be returned
            Assert.AreEqual(3, result.Count); // All 3 pets should be returned
        }

        [Test]
        public async Task ReadWithFiltersAsync_ShouldReturnEmptyList_WhenNoPetsMatchTheFilters()
        {
            // Arrange: Define filters that no pets will match
            var types = new List<PetTypeEnum> { PetTypeEnum.Bird };
            var genders = new List<GenderEnum> { GenderEnum.Female };
            var ages = new List<PetAgeEnum> { PetAgeEnum.Adult };
            bool? withCage = true;
            int page = 1;
            int pageSize = 10;

            // Act: Call the method with filters that no pet matches
            var result = await _petService.ReadWithFiltersAsync(types, genders, ages, withCage, page, pageSize);

            // Assert: No pets should match the filters, so the result should be empty
            Assert.AreEqual(0, result.Count);
        }


        [Test]
        public async Task Read4OldestAsync_ShouldReturn4OldestPets()
        {
            // Arrange
            var user = await GetExampleUser();
            var pets = new List<Pet>
            {
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = "Bella",
                    PetType = PetTypeEnum.Dog,
                    Gender = GenderEnum.Female,
                    Description = "Friendly dog",
                    IncludesCage = false,
                    Birthday = DateTime.Now.AddYears(-2),
                    AddedOn = DateTime.Now.AddMonths(-3),
                    UserId = user.Id,
                    User = user,
                    IsActive = true
                },
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = "Max",
                    PetType = PetTypeEnum.Cat,
                    Gender = GenderEnum.Male,
                    Description = "Curious cat",
                    IncludesCage = true,
                    Birthday = DateTime.Now.AddYears(-1),
                    AddedOn = DateTime.Now.AddMonths(-1),
                    UserId = user.Id,
                    User = user,
                    IsActive = true
                },
                new Pet
                {
                    Id = Guid.NewGuid(),
                    Name = "Charlie",
                    PetType = PetTypeEnum.Dog,
                    Gender = GenderEnum.Male,
                    Description = "Loyal dog",
                    IncludesCage = true,
                    Birthday = DateTime.Now.AddYears(-4),
                    AddedOn = DateTime.Now.AddMonths(-6),
                    UserId = user.Id,
                    User = user,
                    IsActive = true
                }
            };
            foreach (var pet in pets)
                db.Pets.Add(pet);
            db.SaveChanges();
            // Act: Call the method
            var result = await _petService.Read4OldestAsync();

            // Assert: Check that the oldest 4 pets are returned
            Assert.AreEqual(3, result.Count); // There are only 3 pets, so all should be returned
            Assert.AreEqual("Charlie", result[0].Name); // "Charlie" should be the oldest pet
        }

        [Test]
        public async Task Read4OldestAsync_ShouldReturnEmpty_WhenNoPetsExist()
        {

            // Act: Call the method with an empty pet list
            var result = await _petService.Read4OldestAsync();

            // Assert: The result should be an empty list
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public async Task ReadAllWithFilterAsyncOfUser_ShouldReturnPetsForUser()
        {
            // Arrange: Define the userId and filters for the test
            var user = await GetExampleUser();
            var max = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Max",
                PetType = PetTypeEnum.Cat,
                Gender = GenderEnum.Male,
                Breed = "Cat",
                Description = "Curious cat",
                IncludesCage = true,
                Birthday = DateTime.Now.AddYears(-1),
                AddedOn = DateTime.Now.AddMonths(-1),
                UserId = user.Id,
                User = user,
                IsActive = true
            };
            db.Pets.Add(max);
            db.SaveChanges();
            var userId = user.Id;
            var name = "Max";
            var petBreed = "Cat";
            var petType = PetTypeEnum.Cat.ToDescriptionString();
            var gender = GenderEnum.Male.ToDescriptionString();
            int page = 1;
            int pageSize = 10;

            // Act: Call the method with the filters
            var result = await _petService.ReadAllWithFilterAsyncOfUser(userId, name, petBreed, petType, gender, page, pageSize);

            // Assert: Check that the correct pets are returned based on the filters
            Assert.AreEqual(1, result.Count); // Only 1 pet should match the filters
            Assert.AreEqual("Max", result[0].Name); // "Max" should be the returned pet
        }

        [Test]
        public async Task ReadAllWithFilterAsyncOfUser_ShouldReturnEmpty_WhenNoPetsMatchTheFilters()
        {
            // Arrange: Define filters that no pets will match
            var userId = Guid.NewGuid();
            var name = "Fluffy";
            var petBreed = "Dog";
            var petType = "Dog";
            var gender = "Female";
            int page = 1;
            int pageSize = 10;

            // Act: Call the method with filters that no pet matches
            var result = await _petService.ReadAllWithFilterAsyncOfUser(userId, name, petBreed, petType, gender, page, pageSize);

            // Assert: The result should be an empty list
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public async Task ReadAllWithFilterAsync_AppliesFilteringAndPaging()
        {
            // Arrange
            var user = await GetExampleUser();
            var pets = new List<Pet>
            {
                new Pet { Id = Guid.NewGuid(), Name = "Buddy", Breed = "Bulldog", PetType = PetTypeEnum.Dog, User = user },
                new Pet { Id = Guid.NewGuid(), Name = "Milo", Breed = "Poodle", PetType = PetTypeEnum.Dog, User = user },
                new Pet { Id = Guid.NewGuid(), Name = "Luna", Breed = "Siamese", PetType = PetTypeEnum.Cat, User = user }
            };
            await db.Pets.AddRangeAsync(pets);
            await db.SaveChangesAsync();

            // Act
            var result = await _petService.ReadAllWithFilterAsync("Milo", "", "", "", "", 1, 10);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Milo", result[0].Name);
        }

        [Test]
        public async Task Read4NewestAsync_ReturnsFourNewestPets()
        {
            // Arrange
            var user = await GetExampleUser();
            var pets = Enumerable.Range(1, 6)
                .Select(i => new Pet { Id = Guid.NewGuid(), Name = $"Pet{i}", AddedOn = DateTime.UtcNow.AddMinutes(-i), User = user })
                .ToList();

            await db.Pets.AddRangeAsync(pets);
            await db.SaveChangesAsync();

            // Act
            var result = await _petService.Read4NewestAsync();

            // Assert
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual("Pet1", result[0].Name);
        }

        [Test]
        public async Task UpdateAsync_WhenPetExists_UpdatesDatabase()
        {
            // Arrange
            var user = await GetExampleUser();
            var pet = new Pet { Id = Guid.NewGuid(), Name = "OldName", User = user };
            await db.Pets.AddAsync(pet);
            await db.SaveChangesAsync();

            // Act
            pet.Name = "NewName";
            await _petService.UpdateAsync(pet);
            var updatedPet = await db.Pets.FirstOrDefaultAsync(p => p.Id == pet.Id);

            // Assert
            Assert.AreEqual("NewName", updatedPet.Name);
            Assert.AreEqual(user.Id, updatedPet.User.Id);
        }

        [Test]
        public async Task DeleteAsync_WhenPetExists_RemovesFromDatabase()
        {
            // Arrange
            var user = await GetExampleUser();
            var pet = new Pet { Id = Guid.NewGuid(), Name = "ToDelete", User = user };
            await db.Pets.AddAsync(pet);
            await db.SaveChangesAsync();

            // Act
            await _petService.DeleteAsync(pet.Id);
            var deletedPet = await db.Pets.FirstOrDefaultAsync(p => p.Id == pet.Id);

            // Assert
            Assert.That(deletedPet.IsActive == false, "Pet should be removed");
        }
    }
}
