using BusinessLayer.Functions;
using BusinessLayer.Models;
using DataLayer;
using DataLayer.ProjectDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Database_Functions
{
    public static class DatabaseFunctions
    {
        public static void DeleteAllEntries()
        {
            TownService.DeleteAll();
            CountryService.DeleteAll();
            PetService.DeleteAll();
            UserService.DeleteAll();
            UserRequestsService.DeleteAll();
            PublicOfferService.DeleteAll();
        }

        public static void InitialSetup()
        {
            DeleteAllEntries();
            var countries = new List<Country>
                {
                    new Country(Guid.NewGuid(), "United States"),
                    new Country(Guid.NewGuid(), "Canada"),
                    new Country(Guid.NewGuid(), "United Kingdom"),
                    new Country(Guid.NewGuid(), "Australia"),
                    new Country(Guid.NewGuid(), "Germany"),
                    new Country(Guid.NewGuid(), "Bulgaria")
                };
            CountryService.Create(countries);
            Console.WriteLine(TownService.ReadAll().Count);
            var towns = new List<Town>
                {
                    new Town(Guid.NewGuid(), countries[0], "New York"),
                    new Town(Guid.NewGuid(), countries[1], "Toronto"),
                    new Town(Guid.NewGuid(), countries[2], "London"),
                    new Town(Guid.NewGuid(), countries[3], "Sydney"),
                    new Town(Guid.NewGuid(), countries[4], "Berlin"),
                    new Town(Guid.NewGuid(), countries[5], "Plovdiv")
                };
            TownService.Create(towns);

            var users = new List<User>
                {
                    new User(Guid.NewGuid(), towns[0],new List<Pet>(), new List<UserRequests>(),"John Doe","photoPath", true, "john@example.com", "john", "password"),
                    new User(Guid.NewGuid(), towns[1],new List<Pet>(),new List<UserRequests>(), "Alice Smith", "photoPath", false, "alice@example.com", "alice", "password"),
                    new User(Guid.NewGuid(), towns[2],new List<Pet>(), new List < UserRequests >(), "Bob Johnson", "photoPath", true, "bob@example.com", "bob", "password"),
                    new User(Guid.NewGuid(), towns[3],new List<Pet>(), new List < UserRequests >(), "Emily Brown", "photoPath", false, "emily@example.com", "emily", "password"),
                    new User(Guid.NewGuid(), towns[4],new List<Pet>(), new List < UserRequests >(), "Michael Wilson", "photoPath", true, "michael@example.com", "michael", "password"),
                    new User(Guid.NewGuid(), towns[5],new List<Pet>(), new List < UserRequests >(), "Viktor Zashev", "photoPath", true, "vbzashev@gmail.com", "vzashev", "TropchoEnjoyer")
                };

            UserService.Create(users);

            var pets = new List<Pet>
                {
                    new Pet(Guid.NewGuid(), users[0], "Max", string.Empty, 3, "Dog", "Friendly dog", true),
                    new Pet(Guid.NewGuid(), users[1], "Whiskers", string.Empty, 2, "Cat", "Playful cat", false),
                    new Pet(Guid.NewGuid(), users[2], "Buddy", string.Empty, 4, "Dog", "Loyal companion", true),
                    new Pet(Guid.NewGuid(), users[3], "Mittens", string.Empty, 1, "Cat", "Curious kitten", false),
                    new Pet(Guid.NewGuid(), users[4], "Rocky", string.Empty, 5, "Dog", "Energetic pup", true),
                    new Pet(Guid.NewGuid(), users[5], "Tropcho", string.Empty, 5, "Gerbil", "Sleeping beauty", true)
                };
            PetService.Create(pets);

            var publicOffers = new List<PublicOffer>
                {
                    new PublicOffer(Guid.NewGuid(), pets[0], true),
                    new PublicOffer(Guid.NewGuid(), pets[1], false),
                    new PublicOffer(Guid.NewGuid(), pets[2], true),
                    new PublicOffer(Guid.NewGuid(), pets[3], false),
                    new PublicOffer(Guid.NewGuid(), pets[4], true),
                    new PublicOffer(Guid.NewGuid(), pets[5], true)
                };
            PublicOfferService.Create(publicOffers);

            var userRequests = new List<UserRequests>
                {
                    new UserRequests(Guid.NewGuid(), publicOffers[0], true),
                    new UserRequests(Guid.NewGuid(), publicOffers[1], false),
                    new UserRequests(Guid.NewGuid(),publicOffers[2], true),
                    new UserRequests(Guid.NewGuid(),publicOffers[3], false),
                    new UserRequests(Guid.NewGuid(),publicOffers[4], false),
                    new UserRequests(Guid.NewGuid(),publicOffers[5], true)
                };
            UserRequestsService.Create(userRequests);
        }
    }
}
