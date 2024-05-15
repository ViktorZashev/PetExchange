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

        private static PetExchangeDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<PetExchangeDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;
            return new PetExchangeDbContext(options);
		}

        private static void DeleteAllEntriesInDb()
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

			foreach (var entity in db.Countries)
			{
				db.Countries.Remove(entity);
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
            DeleteAllEntriesInDb(); // To ensure controlled environment in every unit test
        }

        [TearDown]
        public void DisposeContext()
        {
            db.Dispose();
        }
    }
}