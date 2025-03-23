using DataLayer;
using System.Threading.Tasks;

namespace PetExchangeTests
{
    public class UserRequestsDbContextTests : DataLayerTestsManagement
    {
        private async Task<UserRequest> GetExampleUserRequest(bool saveRequest = true)
        {
            var sender = await GetExampleUser(saveRequest);
            sender.UserName = "senderUser";
            sender.Email = "sender@gmail.com";

            var recipient = await  GetExampleUser(saveRequest);
            recipient.UserName = "recipientUser";
            recipient.Email = "recipient@gmail.com";
            var pet = new Pet
            {
                Id = Guid.NewGuid(),
                Name = "ExamplePet",
                Breed = "BreedExample",
                PetType = PetTypeEnum.Dog,
                AdoptedOn = null
            };

            var userRequest = new UserRequest
            {
                Id = Guid.NewGuid(),
                Pet = pet,
                Sender = sender,
                Recipient = recipient,
                SenderId = sender.Id,
                RecipientId = recipient.Id,
                CreatedOn = DateTime.Now
            };

            if (saveRequest)
            {
                db.Pets.Add(pet);
                db.Requests.Add(userRequest);
                db.SaveChanges();
            }

            return userRequest;
        }

        [Test]
        public async Task CreateAsync_SingleRequest_Succeeds()
        {
            // Arrange
            var userRequest = await GetExampleUserRequest(false);

            // Act
            await userRequestsContext.CreateAsync(userRequest);

            // Assert
            var createdRequest = await db.Requests.FindAsync(userRequest.Id);
            Assert.NotNull(createdRequest);
            Assert.AreEqual("senderUser", createdRequest.Sender.UserName);
        }

        [Test]
        public async Task CreateAsync_MultipleRequests_Succeeds()
        {
            // Arrange
            var userRequest1 = await GetExampleUserRequest(false);
            var userRequest2 = await GetExampleUserRequest(false);

            var requests = new List<UserRequest> { userRequest1, userRequest2 };

            // Act
            await userRequestsContext.CreateAsync(requests);

            // Assert
            var createdRequest1 = await db.Requests.FindAsync(userRequest1.Id);
            var createdRequest2 = await db.Requests.FindAsync(userRequest2.Id);
            Assert.NotNull(createdRequest1);
            Assert.NotNull(createdRequest2);
        }

        [Test]
        public async Task ReadAsync_ExistingRequest_ReturnsRequest()
        {
            // Arrange
            var userRequest = await GetExampleUserRequest();

            // Act
            var retrievedRequest = await userRequestsContext.ReadAsync(userRequest.Id);

            // Assert
            Assert.NotNull(retrievedRequest);
            Assert.AreEqual("senderUser", retrievedRequest.Sender.UserName);
        }

        [Test]
        public async Task ReadAsync_NonExistentRequest_ReturnsNull()
        {
            // Act
            var nonExistentRequest = await userRequestsContext.ReadAsync(Guid.NewGuid());

            // Assert
            Assert.IsNull(nonExistentRequest);
        }

        [Test]
        public async Task ReadAllWithFilterAsync_FiltersBySenderName_Succeeds()
        {
            // Arrange
            var userRequest1 = await GetExampleUserRequest(false);
            var userRequest2 = await GetExampleUserRequest(false);
            userRequest2.Sender.Name = "otherSender";
            userRequest1.Sender.Name = "senderUser";
            db.Requests.Add(userRequest2);
            db.Requests.Add(userRequest1);
            db.SaveChanges();

            // Act
            var result = await userRequestsContext.ReadAllWithFilterAsync("", "", "senderUser", "", 1, 10);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("senderUser", result.First().Sender.UserName);
        }

        [Test]
        public async Task UpdateAsync_ExistingRequest_UpdatesSuccessfully()
        {
            // Arrange
            var userRequest = await GetExampleUserRequest();
            var newMessage = "Updated request message";
            userRequest.AnswerMessage = newMessage;

            // Act
            await userRequestsContext.UpdateAsync(userRequest);

            // Assert
            var updatedRequest = await db.Requests.FindAsync(userRequest.Id);
            Assert.NotNull(updatedRequest);
            Assert.AreEqual(newMessage, updatedRequest.AnswerMessage);
        }

        [Test]
        public async Task CancelAsync_ExistingRequest_CancelsSuccessfully()
        {
            // Arrange
            var userRequest = await GetExampleUserRequest();

            // Act
            await userRequestsContext.CancelAsync(userRequest.Id);

            // Assert
            var canceledRequest = await db.Requests.FindAsync(userRequest.Id);
            Assert.NotNull(canceledRequest);
            Assert.IsNotNull(canceledRequest.CanceledOn);
        }

        [Test]
        public async Task AcceptAsync_ExistingRequest_AcceptsSuccessfully()
        {
            // Arrange
            var userRequest = await GetExampleUserRequest();

            // Act
            await userRequestsContext.AcceptAsync(userRequest.Id, "Accepted request");

            // Assert
            var acceptedRequest = await db.Requests.FindAsync(userRequest.Id);
            Assert.NotNull(acceptedRequest);
            Assert.IsNotNull(acceptedRequest.AcceptedOn);
            Assert.AreEqual("Accepted request", acceptedRequest.AnswerMessage);
        }

        [Test]
        public async Task DenyAsync_ExistingRequest_DeniesSuccessfully()
        {
            // Arrange
            var userRequest = await GetExampleUserRequest();

            // Act
            await userRequestsContext.DenyAsync(userRequest.Id, "Denied request");

            // Assert
            var deniedRequest = await db.Requests.FindAsync(userRequest.Id);
            Assert.NotNull(deniedRequest);
            Assert.IsNotNull(deniedRequest.DeniedOn);
            Assert.AreEqual("Denied request", deniedRequest.AnswerMessage);
        }

        [Test]
        public async Task DeleteAsync_ExistingRequest_DeletesSuccessfully()
        {
            // Arrange
            var userRequest = await GetExampleUserRequest();

            // Act
            await userRequestsContext.DeleteAsync(userRequest.Id);

            // Assert
            var deletedRequest = await db.Requests.FindAsync(userRequest.Id);
            Assert.IsNull(deletedRequest);
        }

        [Test]
        public void DeleteAsync_NonExistentRequest_ThrowsArgumentException()
        {
            // Arrange
            var nonExistentRequestId = Guid.NewGuid();

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await userRequestsContext.DeleteAsync(nonExistentRequestId));
            Assert.AreEqual($"User request with id = {nonExistentRequestId} does not exist!", ex.Message);
        }
    }
}
