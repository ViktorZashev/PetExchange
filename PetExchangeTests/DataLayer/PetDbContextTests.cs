using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Models;
using DataLayer;
using DataLayer.ModelsDbContext;
using DataLayer.ProjectDbContext;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace PetExchangeTests.DataLayer
{
    public class PetDbContextTests
    {
        public static PetDbContext petDbContext;
        public static Mock<PetExchangeDbContext> mockDbContext;

        [SetUp]
        public void Setup()
        {
            mockDbContext = new Mock<PetExchangeDbContext>();
            petDbContext = new PetDbContext(mockDbContext.Object);
        }

        [Test]
        public void Create_AddsPetToDatabase()
        {
            // Arrange
            var user = new User(Guid.NewGuid(), "TestUser");
            var pet = new Pet(Guid.NewGuid(), "TestPet", user);
            mockDbContext.Setup(db => db.Users.FirstOrDefault(u => u.Id == user.Id)).Returns(user);
            var pets = new List<Pet>();
            var mockSet = new Mock<DbSet<Pet>>();
            mockSet.As<IQueryable<Pet>>().Setup(m => m.Provider).Returns(pets.AsQueryable().Provider);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.Expression).Returns(pets.AsQueryable().Expression);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.ElementType).Returns(pets.AsQueryable().ElementType);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.GetEnumerator()).Returns(pets.AsQueryable().GetEnumerator());
            mockDbContext.Setup(db => db.Pets).Returns(mockSet.Object);

            // Act
            petDbContext.Create(pet);

            // Assert
            mockDbContext.Verify(db => db.Pets.Add(pet), Times.Once);
            mockDbContext.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Test]
        public void Read_ReturnsPetById()
        {
            // Arrange
            var petId = Guid.NewGuid();
            var user = new User(Guid.NewGuid(), "TestUser");
            var expectedPet = new Pet(petId, "TestPet", user);
            var pets = new List<Pet> { expectedPet };
            var mockSet = new Mock<DbSet<Pet>>();
            mockSet.As<IQueryable<Pet>>().Setup(m => m.Provider).Returns(pets.AsQueryable().Provider);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.Expression).Returns(pets.AsQueryable().Expression);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.ElementType).Returns(pets.AsQueryable().ElementType);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.GetEnumerator()).Returns(pets.AsQueryable().GetEnumerator());
            mockDbContext.Setup(db => db.Pets).Returns(mockSet.Object);

            // Act
            var result = petDbContext.Read(petId);

            // Assert
            Assert.AreEqual(expectedPet, result, "Read method doesn't return correct pet by id query!");
        }

        [Test]
        public void ReadAll_ReturnsPets()
        {
            // Arrange
            var expectedPets = new List<Pet> { new Pet("TestPet1", new User("TestUser1")), new Pet("TestPet2", new User("TestUser2")) };
            var mockSet = new Mock<DbSet<Pet>>();
            mockSet.As<IQueryable<Pet>>().Setup(m => m.Provider).Returns(expectedPets.AsQueryable().Provider);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.Expression).Returns(expectedPets.AsQueryable().Expression);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.ElementType).Returns(expectedPets.AsQueryable().ElementType);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.GetEnumerator()).Returns(expectedPets.AsQueryable().GetEnumerator());
            mockDbContext.Setup(db => db.Pets).Returns(mockSet.Object);

            // Act
            var result = petDbContext.ReadAll();

            // Assert
            Assert.IsTrue(expectedPets.SequenceEqual(result), "Read method doesn't return correct pets!");
        }

        [Test]
        public void Update_EntityDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            var petId = Guid.NewGuid();
            var nonExistingPet = new Pet(petId, "NonExistingPet", new User("TestUser"));
            mockDbContext.Setup(db => db.Pets.Find(petId)).Returns((Pet)null);
            var pets = new List<Pet> { new Pet("Pet1", new User("TestUser1")), new Pet("Pet2", new User("TestUser2")) };
            var mockSet = new Mock<DbSet<Pet>>();
            mockSet.As<IQueryable<Pet>>().Setup(m => m.Provider).Returns(pets.AsQueryable().Provider);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.Expression).Returns(pets.AsQueryable().Expression);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.ElementType).Returns(pets.AsQueryable().ElementType);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.GetEnumerator()).Returns(pets.AsQueryable().GetEnumerator());
            mockDbContext.Setup(db => db.Pets).Returns(mockSet.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => petDbContext.Update(nonExistingPet));
            Assert.AreEqual("Entity with id:" + petId + " doesn't exist in the database!", ex.Message, "Exception message is incorrect");
        }

        [Test]
        public void Delete_EntityExists_DeletesEntity()
        {
            // Arrange
            var petId = Guid.NewGuid();
            var existingPet = new Pet(petId, "ExistingPet", new User("TestUser"));
            mockDbContext.Setup(db => db.Pets.Find(petId)).Returns(existingPet);
            var pets = new List<Pet> { new Pet("Pet1", new User("TestUser1")), existingPet, new Pet("Pet2", new User("TestUser2")) };
            var mockSet = new Mock<DbSet<Pet>>();
            mockSet.As<IQueryable<Pet>>().Setup(m => m.Provider).Returns(pets.AsQueryable().Provider);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.Expression).Returns(pets.AsQueryable().Expression);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.ElementType).Returns(pets.AsQueryable().ElementType);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.GetEnumerator()).Returns(pets.AsQueryable().GetEnumerator());
            mockDbContext.Setup(db => db.Pets).Returns(mockSet.Object);

            // Act
            petDbContext.Delete(petId);

            // Assert
            mockDbContext.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Test]
        public void Delete_EntityDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            var petId = Guid.NewGuid();
            mockDbContext.Setup(db => db.Pets.Find(petId)).Returns((Pet)null);
            var pets = new List<Pet> { new Pet("Pet1", new User("TestUser1")), new Pet("Pet2", new User("TestUser2")) };
            var mockSet = new Mock<DbSet<Pet>>();
            mockSet.As<IQueryable<Pet>>().Setup(m => m.Provider).Returns(pets.AsQueryable().Provider);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.Expression).Returns(pets.AsQueryable().Expression);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.ElementType).Returns(pets.AsQueryable().ElementType);
            mockSet.As<IQueryable<Pet>>().Setup(m => m.GetEnumerator()).Returns(pets.AsQueryable().GetEnumerator());
            mockDbContext.Setup(db => db.Pets).Returns(mockSet.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => petDbContext.Delete(petId));
        }
    }
}