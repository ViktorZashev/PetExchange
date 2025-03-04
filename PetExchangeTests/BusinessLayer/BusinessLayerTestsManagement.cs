using DataLayer;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace PetExchangeTests
{
    public class BusinessLayerTestsManagement
    {
        public static PetExchangeDbContext db;
        public PetService _petService;
        public PublicOfferService _publicOfferService ;
        public TownService _townService;
        public UserRequestsService _userRequestsService;
        public UserService _userService;
        private static PetExchangeDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<PetExchangeDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;
            return new PetExchangeDbContext(options);
        }
        [SetUp]
        public void Setup()
        {
            db = GetMemoryContext();
            _petService = new PetService(db);
            _publicOfferService = new PublicOfferService(db);
            _townService = new TownService(db);
            _userRequestsService = new UserRequestsService(db);
            _userService = new UserService(db);
            DeleteAllEntriesInDb();
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

            foreach (var entity in db.PublicOffers)
            {
                db.PublicOffers.Remove(entity);
            }

            foreach (var entity in db.Requests)
            {
                db.Requests.Remove(entity);
            }

            db.SaveChanges();
        }

        [TearDown]
        public void DisposeContext()
        {
            db.Dispose();
        }

    }
}
