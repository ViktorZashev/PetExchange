using DataLayer;
using NuGet.Protocol.Core.Types;

namespace PetExchangeTests
{
    public class PetDbContextTests : DataLayerTestsManagement
	{
        [Test]
        public async Task CreateMethod_AddsPetToDatabase()
        {
            var newUser = GetExampleUser();
            var newPet = new Pet(newUser, "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false);

            var initialCount = db.Pets.Count();
            await petContext.CreateAsync(newPet);
            var actualCount = db.Pets.Count();

            Assert.That(actualCount, Is.EqualTo(initialCount + 1), "Pet count did not increment by 1.");
        }

        [Test]
        public async Task CreateMethod_ThrowsExceptionForDuplicateKey()
        {
            var user = GetExampleUser();
            var matchingId = Guid.NewGuid();
            var newPet = new Pet(user, "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false) { Id = matchingId };
            var duplicatePet = new Pet(user, "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false) { Id = matchingId };

            await petContext.CreateAsync(newPet);
            Assert.ThrowsAsync<InvalidOperationException>(async () => await petContext.CreateAsync(duplicatePet));
        }

        [Test]
        public async Task ReadMethod_RetrievesPetFromDatabase()
        {
            var id = Guid.NewGuid();
            var enteredPet = new Pet(GetExampleUser(), "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false) { Id = id };
            db.Pets.Add(enteredPet);
            await db.SaveChangesAsync();

            var actualPet = await petContext.ReadAsync(id);
            Assert.That(actualPet,Is.EqualTo(enteredPet), "Read method did not return the correct pet.");
        }

        [Test]
        public async Task ReadAllMethod_ReturnsAllPets()
        {
            db.Pets.AddRange(
                new Pet(GetExampleUser(), "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false),
                new Pet(GetExampleUser(), "Buddy", "", 3, PetTypeEnum.Dog, "A playful dog", false)
            );
            await db.SaveChangesAsync();

            var pets = await petContext.ReadAllAsync();
            Assert.That(pets.Count, Is.EqualTo(2), "ReadAll method did not return correct number of pets.");
        }

        [Test]
        public async Task UpdateMethod_UpdatesPetDetails()
        {
            var pet = new Pet(GetExampleUser(), "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false);
            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            pet.Name = "UpdatedFluffy";
            await petContext.UpdateAsync(pet);

            var updatedPet = await petContext.ReadAsync(pet.Id);
            Assert.That(updatedPet.Name, Is.EqualTo("UpdatedFluffy"), "Pet name was not updated correctly.");
        }

        [Test]
        public async Task DeleteMethod_SetsPetAsInactive()
        {
            var pet = new Pet(GetExampleUser(), "Fluffy", "", 2, PetTypeEnum.Cat, "A cute cat", false);
            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            await petContext.DeleteAsync(pet.Id);
            var deletedPet = await petContext.ReadAsync(pet.Id);
            Assert.That(deletedPet.IsActive, Is.False, "Pet was not set to inactive.");
        }

        public User GetExampleUser()
        {
            return new User()
            {
                Email = "example@gmail.com",
                PhoneNumber = "0885328493",
                UserName = "TobiasRieper"
            };
        }
    }
}
