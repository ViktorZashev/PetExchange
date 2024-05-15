using BusinessLayer.Functions;
using DataLayer;
using DataLayer.ModelsDbContext;
using DataLayer.ProjectDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests.BusinessLayer
{
    public class BusinessLayerTestsManagement
    {
        public static PetExchangeDbContext db;
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
            CountryService._CountryContext = new CountryDbContext(db);
            PetService._PetContext = new PetDbContext(db);
            PublicOfferService._PublicOfferContext = new PublicOfferDbContext(db);
            TownService._TownContext = new TownDbContext(db);
            UserRequestsService._UserRequestsContext = new UserRequestsDbContext(db);
            UserService._UserContext = new UserDbContext(db);
            DeleteAllEntriesInDb();
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

        [TearDown]
        public void DisposeContext()
        {
            db.Dispose();
        }

    }
}
