using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace PetExchangeTests
{
    public partial class DataLayerTestsManagement
    {
        public static PetExchangeDbContext db;
        public static PetDbContext petContext;
        public static TownDbContext townContext;
        public static UserDbContext userContext;
        public static UserRequestsDbContext userRequestsContext;

        private static PetExchangeDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<PetExchangeDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;
            return new PetExchangeDbContext(options);
		}

        protected static void DeleteAllEntriesInDb()
        {

			foreach (var entity in db.Pets)
			{
				db.Pets.Remove(entity);
			}

			foreach (var entity in db.Users)
			{
				db.Users.Remove(entity);
			}

			foreach (var entity in db.Towns)
			{
				db.Towns.Remove(entity);
			}
			foreach (var entity in db.Requests)
			{
				db.Requests.Remove(entity);
			}

			db.SaveChanges();
		}

		[SetUp]
        public void Setup()
        {
            db = GetMemoryContext();
            petContext = new PetDbContext(db);
            townContext = new TownDbContext(db);
            userContext = new UserDbContext(db);
            userRequestsContext = new UserRequestsDbContext(db);
            DeleteAllEntriesInDb(); // To ensure controlled environment in every unit test
        }

        [TearDown]
        public void DisposeContext()
        {
            db.Dispose();
        }

        public async Task<User> GetExampleUser(bool saveUser = true)
        {
            var town = new Town("Example Town");
            var user = new User()
            {
                Email = "example@example.com",
                PhoneNumber = "1234567890",
                UserName = "ExampleUser",
                Name = "ExampleName",
                PhotoPath = "path/to/photo.jpg",
                PasswordHash = "PassowrdHashExample",
                IsActive = true,
                Role = RoleEnum.User,
                TownId = town.Id,
                Town = town
            }; 
            if (saveUser)
            {
                db.Towns.Add(town);
                db.Users.Add(user);
                db.SaveChanges();
            }

            return user;
        }
    }
}