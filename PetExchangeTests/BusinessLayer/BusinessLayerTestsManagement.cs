﻿using DataLayer;
using BusinessLayer;
using Microsoft.EntityFrameworkCore;

namespace PetExchangeTests
{
    public class BusinessLayerTestsManagement
    {
        public static PetExchangeDbContext db;
        public PetService _petService;
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
