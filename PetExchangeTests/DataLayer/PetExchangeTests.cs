using DataLayer;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests.DataLayer
{
    public class PetExchangeTests : DataLayerTestsManagement
    {
        private Mock<UserManager<User>> _userManagerMock;
        [SetUp]
        public void SetUp()
        {


            // Set up UserManager with basic mocked functionality
            _userManagerMock = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null
            );

            // Mock the AddToRoleAsync method only (we don't mock CreateAsync)
            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(um => um.RemoveFromRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Initialize UserDbContext with the in-memory dbContext and mocked UserManager
            userContext = new UserDbContext(db, _userManagerMock.Object);
        }

        [Test]
        public async Task SeedingMethodSeedsAllTowns()
        {
            //Arrance 
            await db.SeedAsync(userContext);
            // Act: Call ReadAllAsync
            var result = await townContext.ReadAllAsync();

            // Assert: Verify the correct number of towns is returned
            Assert.AreEqual(27, result.Count, "Should return all towns in the database");
        }

        [Test]
        public async Task SeedingMethodSeedsAllUsers()
        {
            //Arrance 
            await db.SeedAsync(userContext);
            // Act: Call ReadAllAsync
            var result = await userContext.ReadAllAsync();

            // Assert: Verify the correct number of towns is returned
            Assert.AreEqual(10, result.Count, "Should return all towns in the database");
        }

        [Test]
        public async Task SeedingMethodSeedsAllPets()
        {
            //Arrance 
            await db.SeedAsync(userContext);
            // Act: Call ReadAllAsync
            var result = await petContext.ReadAllAsync();

            // Assert: Verify the correct number of towns is returned
            Assert.AreEqual(38, result.Count, "Should return all towns in the database");
        }
        [Test]
        public async Task SeedingMethodSeedsAllRequests()
        {
            //Arrance 
            await db.SeedAsync(userContext);
            // Act: Call ReadAllAsync
            var result = await userRequestsContext.ReadAllAsync();

            // Assert: Verify the correct number of towns is returned
            Assert.AreEqual(5, result.Count, "Should return all towns in the database");
        }
    }
}
