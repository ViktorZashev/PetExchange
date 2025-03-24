using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace PetExchangeTests
{
	public class UserRequestsServiceTests : BusinessLayerTestsManagement
	{

        [Test]
        public async Task CreateAsync_WhenCalled_AddsUserRequestToDatabase()
        {
            // Arrange
            var user = await GetExampleUser();
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = user,
                UserId = user.Id,
                IsActive = true
            };
            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            var recipient = await GetExampleUser(); // recipient user
            var userRequest = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = user.Id,
                RecipientId = recipient.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now,
                Pet = pet,
                Sender = user,
                Recipient = recipient
            };

            // Act
            await _userRequestsService.CreateAsync(userRequest);
            var result = await db.Requests.FirstOrDefaultAsync(r => r.Id == userRequest.Id);

            // Assert
            Assert.IsNotNull(result, "UserRequest should be added to the database");
            Assert.AreEqual(userRequest.SenderId, result.SenderId);
            Assert.AreEqual(userRequest.PetId, result.PetId);
        }

        [Test]
        public async Task ReadAllWithFilterAsync_WhenCalledWithFilters_ReturnsFilteredUserRequests()
        {
            // Arrange
            var sender = await GetExampleUser(false);
            sender.Name = "John";
            var receiver = await GetExampleUser(false);
            receiver.Name = "Jane";
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = receiver,
                UserId = receiver.Id,
                IsActive = true
            };
            db.Users.Add(sender);
            db.Users.Add(receiver);
            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            var userRequest1 = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = sender.Id,
                RecipientId = receiver.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now,
                Pet = pet,
                Sender = sender,
                Recipient = receiver
            };

            var userRequest2 = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = sender.Id,
                RecipientId = receiver.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now.AddDays(-1),
                Pet = pet,
                Sender = sender,
                Recipient = receiver
            };

            db.Requests.AddRange(userRequest1, userRequest2);
            await db.SaveChangesAsync();

            // Act
            var result = await _userRequestsService.ReadAllWithFilterAsync("Buddy", "Golden Retriever", "John", "Jane", 1, 10);

            // Assert
            Assert.AreEqual(2, result.Count, "Filtered results should match the number of UserRequests created");
        }

        [Test]
        public async Task CreateAsync_WhenCalledWithMultipleUserRequests_AddsUserRequestsToDatabase()
        {
            // Arrange
            var user = await GetExampleUser();
            var sender = await GetExampleUser(false);
            sender.Name = "John";
            var receiver = await GetExampleUser(false);
            receiver.Name = "Jane";
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = user,
                UserId = user.Id,
                IsActive = true
            };
            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            var userRequests = new List<UserRequest>
    {
        new UserRequest { Id = Guid.NewGuid(), SenderId = sender.Id, RecipientId = receiver.Id, PetId = pet.Id, CreatedOn = DateTime.Now, Pet = pet, Sender = sender, Recipient = receiver },
        new UserRequest { Id = Guid.NewGuid(), SenderId = sender.Id, RecipientId = receiver.Id, PetId = pet.Id, CreatedOn = DateTime.Now, Pet = pet, Sender = sender, Recipient = receiver }
    };

            // Act
            await _userRequestsService.CreateAsync(userRequests);

            // Assert
            var result = await db.Requests.ToListAsync();
            Assert.AreEqual(2, result.Count, "Two UserRequests should be created and added to the database");
        }

        [Test]
        public async Task ReadAllWithFilterAsync_WhenCalledWithValidFilters_ReturnsFilteredRequests()
        {
            // Arrange
            var sender = await GetExampleUser();
            sender.Name = "John";
            var receiver = await GetExampleUser();
            receiver.Name = "Jane";
            var pet1 = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = receiver,
                UserId = receiver.Id,
                IsActive = true
            };
            var pet2 = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Max",
                Breed = "Bulldog",
                Birthday = DateTime.Now.AddYears(-3),
                User = receiver,
                UserId = receiver.Id,
                IsActive = true
            };

            db.Pets.AddRange(pet1, pet2);
            await db.SaveChangesAsync();

            var userRequest1 = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = sender.Id,
                RecipientId = receiver.Id,
                PetId = pet1.Id,
                CreatedOn = DateTime.Now,
                Pet = pet1,
                Sender = sender,
                Recipient = receiver
            };

            var userRequest2 = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = sender.Id,
                RecipientId = receiver.Id,
                PetId = pet2.Id,
                CreatedOn = DateTime.Now,
                Pet = pet2,
                Sender = sender,
                Recipient = receiver
            };

            db.Requests.AddRange(userRequest1, userRequest2);
            await db.SaveChangesAsync();

            // Act
            var result = await _userRequestsService.ReadAllWithFilterAsync("Max", "Bulldog", "John", "Jane", 1, 10);

            // Assert
            Assert.AreEqual(1, result.Count, "Only the request for 'Max' Bulldog should be returned.");
            Assert.AreEqual(pet2.Id, result[0].PetId, "The pet ID should match 'Max' Bulldog.");
        }

        [Test]
        public async Task ReadAllWithFilterAsync_WhenCalledWithNoFilters_ReturnsAllRequests()
        {
            // Arrange
            var sender = await GetExampleUser();
            sender.Name = "John";
            var receiver = await GetExampleUser();
            receiver.Name = "Jane";
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = receiver,
                UserId = receiver.Id,
                IsActive = true
            };
            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            var userRequest = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = sender.Id,
                RecipientId = receiver.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now,
                Pet = pet,
                Sender = sender,
                Recipient = receiver
            };

            db.Requests.Add(userRequest);
            await db.SaveChangesAsync();

            // Act
            var result = await _userRequestsService.ReadAllWithFilterAsync("", "", "", "", 1, 10);

            // Assert
            Assert.AreEqual(1, result.Count, "One user request should be returned when no filters are applied.");
        }

        [Test]
        public async Task ReadAsync_WhenCalledWithValidId_ReturnsUserRequest()
        {
            // Arrange
            var sender = await GetExampleUser();
            var receiver = await GetExampleUser();
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = receiver,
                UserId = receiver.Id,
                IsActive = true
            };

            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            var userRequest = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = sender.Id,
                RecipientId = receiver.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now,
                Pet = pet,
                Sender = sender,
                Recipient = receiver
            };
            db.Requests.Add(userRequest);
            await db.SaveChangesAsync();

            // Act
            var result = await _userRequestsService.ReadAsync(userRequest.Id);

            // Assert
            Assert.IsNotNull(result, "The user request should be returned.");
            Assert.AreEqual(userRequest.Id, result.Id, "The returned user request ID should match the requested ID.");
        }

        [Test]
        public async Task ReadAllAsync_WhenCalled_ReturnsAllUserRequests()
        {
            // Arrange
            var sender = await GetExampleUser();
            var receiver = await GetExampleUser();
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = receiver,
                UserId = receiver.Id,
                IsActive = true
            };

            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            var userRequest1 = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = sender.Id,
                RecipientId = receiver.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now,
                Pet = pet,
                Sender = sender,
                Recipient = receiver
            };

            var userRequest2 = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = sender.Id,
                RecipientId = receiver.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now,
                Pet = pet,
                Sender = sender,
                Recipient = receiver
            };

            db.Requests.AddRange(userRequest1, userRequest2);
            await db.SaveChangesAsync();

            // Act
            var result = await _userRequestsService.ReadAllAsync();

            // Assert
            Assert.AreEqual(2, result.Count, "There should be 2 user requests returned.");
        }

        [Test]
        public async Task CancelAsync_WhenCalled_CancelsUserRequest()
        {
            // Arrange
            var sender = await GetExampleUser();
            var receiver = await GetExampleUser();
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = receiver,
                UserId = receiver.Id,
                IsActive = true
            };

            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            var userRequest = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = sender.Id,
                RecipientId = receiver.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now,
                Pet = pet,
                Sender = sender,
                Recipient = receiver
            };

            db.Requests.Add(userRequest);
            await db.SaveChangesAsync();

            // Act
            await _userRequestsService.CancelAsync(userRequest.Id);

            // Assert
            var canceledRequest = await db.Requests.FirstOrDefaultAsync(r => r.Id == userRequest.Id);
            Assert.IsTrue(canceledRequest.CanceledOn != null, "The request status should be updated to 'Cancelled'.");
        }

        [Test]
        public async Task AcceptAsync_WhenCalled_UpdatesUserRequestStatus()
        {
            // Arrange
            var sender = await GetExampleUser();
            var receiver = await GetExampleUser();
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = receiver,
                UserId = receiver.Id,
                IsActive = true
            };

            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            var userRequest = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = sender.Id,
                RecipientId = receiver.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now,
                Pet = pet,
                Sender = sender,
                Recipient = receiver
            };

            db.Requests.Add(userRequest);
            await db.SaveChangesAsync();

            // Act
            await _userRequestsService.AcceptAsync(userRequest.Id, "Accepted");

            // Assert
            var acceptedRequest = await db.Requests.FirstOrDefaultAsync(r => r.Id == userRequest.Id);
            Assert.IsNotNull(acceptedRequest, "The user request should still exist.");
            Assert.IsTrue(acceptedRequest.AcceptedOn!=null, "The request status should be updated to 'Accepted'.");
        }

        [Test]
        public async Task DenyAsync_WhenCalled_UpdatesUserRequestStatus()
        {
            // Arrange
            var sender = await GetExampleUser();
            var receiver = await GetExampleUser();
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = receiver,
                UserId = receiver.Id,
                IsActive = true
            };

            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            var userRequest = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = sender.Id,
                RecipientId = receiver.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now,
                Pet = pet,
                Sender = sender,
                Recipient = receiver
            };

            db.Requests.Add(userRequest);
            await db.SaveChangesAsync();

            // Act
            await _userRequestsService.DenyAsync(userRequest.Id, "Denied");

            // Assert
            var deniedRequest = await db.Requests.FirstOrDefaultAsync(r => r.Id == userRequest.Id);
            Assert.IsNotNull(deniedRequest, "The user request should still exist.");
            Assert.IsTrue(deniedRequest.DeniedOn != null, "The request status should be updated to 'Denied'.");
        }

        [Test]
        public async Task ReadUserRequestOutboxAsync_WhenUserIdProvided_ReturnsUserRequestOutbox()
        {
            // Arrange
            var user = await GetExampleUser();
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = user,
                UserId = user.Id,
                IsActive = true
            };
            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            var recipient = await GetExampleUser(false); // recipient user
            var userRequest = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = user.Id,
                RecipientId = recipient.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now,
                Pet = pet,
                Sender = user,
                Recipient = recipient
            };

            db.Requests.Add(userRequest);
            await db.SaveChangesAsync();

            // Act
            var result = await _userRequestsService.ReadUserRequestOutboxAsync(user.Id);

            // Assert
            Assert.AreEqual(1, result.Count, "Should return the correct number of user requests in the outbox");
            Assert.AreEqual(userRequest.SenderId, result[0].SenderId, "The user request should be in the sender's outbox");
        }

        [Test]
        public async Task ReadUserRequestInboxAsync_WhenUserIdProvided_ReturnsUserRequestInbox()
        {
            // Arrange
            var user = await GetExampleUser();
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = user,
                UserId = user.Id,
                IsActive = true
            };
            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            var sender = await GetExampleUser(false); // sender user
            var userRequest = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = sender.Id,
                RecipientId = user.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now,
                Pet = pet,
                Sender = sender,
                Recipient = user
            };

            db.Requests.Add(userRequest);
            await db.SaveChangesAsync();

            // Act
            var result = await _userRequestsService.ReadUserRequestInboxAsync(user.Id);

            // Assert
            Assert.AreEqual(1, result.Count, "Should return the correct number of user requests in the inbox");
            Assert.AreEqual(userRequest.RecipientId, result[0].RecipientId, "The user request should be in the recipient's inbox");
        }

        [Test]
        public async Task UpdateAsync_WhenCalled_UpdatesUserRequestInDatabase()
        {
            // Arrange
            var user = await GetExampleUser();
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = user,
                UserId = user.Id,
                IsActive = true
            };
            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            var recipient = await GetExampleUser(false); // recipient user
            var userRequest = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = user.Id,
                RecipientId = recipient.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now,
                Pet = pet,
                Sender = user,
                Recipient = recipient
            };

            db.Requests.Add(userRequest);
            await db.SaveChangesAsync();

            userRequest.RequestMessage = "Updated Request Message"; // Changing the message

            // Act
            await _userRequestsService.UpdateAsync(userRequest);

            // Assert
            var updatedRequest = await db.Requests.FirstOrDefaultAsync(r => r.Id == userRequest.Id);
            Assert.AreEqual("Updated Request Message", updatedRequest.RequestMessage, "The request message should be updated.");
        }

        [Test]
        public async Task DeleteAsync_WhenCalled_DeletesUserRequestFromDatabase()
        {
            // Arrange
            var user = await GetExampleUser();
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "Buddy",
                Breed = "Golden Retriever",
                Birthday = DateTime.Now.AddYears(-2),
                User = user,
                UserId = user.Id,
                IsActive = true
            };
            db.Pets.Add(pet);
            await db.SaveChangesAsync();

            var recipient = await GetExampleUser(false); // recipient user
            var userRequest = new UserRequest
            {
                Id = Guid.NewGuid(),
                SenderId = user.Id,
                RecipientId = recipient.Id,
                PetId = pet.Id,
                CreatedOn = DateTime.Now,
                Pet = pet,
                Sender = user,
                Recipient = recipient
            };

            db.Requests.Add(userRequest);
            await db.SaveChangesAsync();

            // Act
            await _userRequestsService.DeleteAsync(userRequest.Id);

            // Assert
            var deletedRequest = await db.Requests.FirstOrDefaultAsync(r => r.Id == userRequest.Id);
            Assert.IsNull(deletedRequest, "The user request should be deleted from the database.");
        }
    }
}
