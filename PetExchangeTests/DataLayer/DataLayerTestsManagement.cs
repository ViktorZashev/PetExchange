using DataLayer.ModelsDbContext;
using DataLayer.ProjectDbContext;
using Microsoft.EntityFrameworkCore;

namespace PetExchangeTests.DataLayer
{
    public partial class DataLayerTestsManagement
    {
        public static PetExchangeDbContext db;
        public static CountryDbContext countryContext;
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
            countryContext = new CountryDbContext(db);
        }

        [TearDown]
        public void DisposeContext()
        {
            db.Dispose();
        }
    }
}