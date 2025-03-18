using DataLayer;

namespace PetExchangeTests
{
	public class UserRequestsServiceTests : BusinessLayerTestsManagement
	{

		[Test]
		public async Task Create_Adds_New_UserRequest_To_Database()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid() };
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.SaveChanges();
			var initialRequestsCount = db.Requests.Count();

			// Act
			var request = new UserRequest(pet, null);
			await _userRequestsService.CreateAsync(request);
			var newRequestsCount = db.Requests.Count();

			// Assert
			Assert.That(newRequestsCount, Is.EqualTo(initialRequestsCount + 1), "The count of user requests should increment by 1 after creating a new request.");
		}

		[Test]
		public async Task Read_Returns_Correct_UserRequest()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid() };
			var request = new UserRequest(pet, null) { Id = Guid.NewGuid() };
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.Requests.Add(request);
			db.SaveChanges();

			// Act
			var result = await _userRequestsService.ReadAsync(request.Id);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.Id, Is.EqualTo(request.Id));
		}

		[Test]
		public async Task ReadAll_Returns_All_UserRequests()
		{
			// Arrange
			var initialCount = db.Requests.Count();

			// Act
			var result = await _userRequestsService.ReadAllAsync();

			// Assert
			Assert.That(result, Has.Count.EqualTo(initialCount));
		}

		[Test]
		public async Task Update_Modifies_Existing_UserRequest()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid() };
			var request = new UserRequest(pet, null);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.Requests.Add(request);
			db.SaveChanges();

			// Act
			request.AcceptedOn = DateTime.Now;
            await _userRequestsService.UpdateAsync(request);
			var updatedRequest = db.Requests.Find(request.Id);

			// Assert
			Assert.NotNull(updatedRequest.AcceptedOn);
		}

		[Test]
		public async Task Delete_Removes_UserRequest_From_Database()
		{
			// Arrange
			var user = new User { Id = Guid.NewGuid() };
			var pet = new Pet { Id = Guid.NewGuid() };
			var request = new UserRequest(pet, null);
			db.Users.Add(user);
			db.Pets.Add(pet);
			db.Requests.Add(request);
			db.SaveChanges();
			var initialRequestsCount = db.Requests.Count();

            // Act
            await _userRequestsService.DeleteAsync(request.Id);
			var newRequestsCount = db.Requests.Count();

			// Assert
			Assert.That(newRequestsCount, Is.EqualTo(initialRequestsCount - 1), "The count of user requests should decrement by 1 after deleting the request.");
		}
	}
}
