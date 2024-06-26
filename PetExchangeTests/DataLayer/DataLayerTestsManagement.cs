using BusinessLayer.Models;
using DataLayer;
using DataLayer.ModelsDbContext;
using DataLayer.ProjectDbContext;
using Microsoft.EntityFrameworkCore;

namespace PetExchangeTests.DataLayer
{
    public partial class DataLayerTestsManagement
    {
        public static PetExchangeDbContext db;

        public static CountryDbContext countryContext;
        public static PetDbContext petContext;
        public static PublicOfferDbContext publicOfferContext;
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
            foreach (var entity in db.Countries)
            {
                db.Countries.Remove(entity);
            }

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

        protected bool AreTwoListsEqual(List<Country> list1, List<Country> list2)
        {
            if (list1.Count != list2.Count) return false;
            for (int i = 0; i < list1.Count; i++)
            {
                if (!AreCountriesEqual(list1[i], list2[i]))
                {
                    return false;
                }
            }
            return true;
        }

        protected static bool AreCountriesEqual(Country expected, Country actual)
		{
			return expected.Id == actual.Id && expected.Name == actual.Name;
		}

		[SetUp]
        public void Setup()
        {
            db = GetMemoryContext();
            countryContext = new CountryDbContext(db);
            petContext = new PetDbContext(db);
            publicOfferContext = new PublicOfferDbContext(db);
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
    }
}