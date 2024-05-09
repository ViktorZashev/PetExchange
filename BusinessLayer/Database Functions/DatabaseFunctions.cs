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

        public static void SeedDatabase()
        {
            DeleteAllEntries();
            var countries = new List<Country>
                {
                    new Country("United States"),
                    new Country("Canada"),
                    new Country("United Kingdom"),
                    new Country("Australia"),
                    new Country("Germany"),
                    new Country("Bulgaria")
                };
            CountryService.Create(countries);
 
            var towns = new List<Town>
                {
                    new Town(countries[0], "New York"),
                    new Town(countries[1], "Toronto"),
                    new Town(countries[2], "London"),
                    new Town(countries[3], "Sydney"),
                    new Town(countries[4], "Berlin"),
                    new Town(countries[5], "Plovdiv")
                };
            TownService.Create(towns);

            var users = new List<User>
                {
                    new User(towns[0],new List<Pet>(), new List<UserRequests>(),"John Doe","photoPath", true, "john@example.com", "john", "password"),
                    new User(towns[1],new List<Pet>(),new List<UserRequests>(), "Alice Smith", "photoPath", false, "alice@example.com", "alice", "password"),
                    new User(towns[2],new List<Pet>(), new List < UserRequests >(), "Bob Johnson", "photoPath", true, "bob@example.com", "bob", "password"),
                    new User(towns[3],new List<Pet>(), new List < UserRequests >(), "Emily Brown", "photoPath", false, "emily@example.com", "emily", "password"),
                    new User(towns[4],new List<Pet>(), new List < UserRequests >(), "Michael Wilson", "photoPath", true, "michael@example.com", "michael", "password"),
                    new User(towns[5],new List<Pet>(), new List < UserRequests >(), "Viktor Zashev", "photoPath", true, "vbzashev@gmail.com", "vzashev", "TropchoEnjoyer")
                };

            UserService.Create(users);

            var pets = new List<Pet>
                {
                    new Pet(users[0], "Max", string.Empty, 3, "Dog", "Friendly dog", true),
                    new Pet(users[1], "Whiskers", string.Empty, 2, "Cat", "Playful cat", false),
                    new Pet(users[2], "Buddy", string.Empty, 4, "Dog", "Loyal companion", true),
                    new Pet(users[3], "Mittens", string.Empty, 1, "Cat", "Curious kitten", false),
                    new Pet(users[4], "Rocky", string.Empty, 5, "Dog", "Energetic pup", true),
                    new Pet(users[5], "Tropcho", string.Empty, 5, "Gerbil", "Sleeping beauty", true)
                };
            PetService.Create(pets);

            var publicOffers = new List<PublicOffer>
                {
                    new PublicOffer(pets[0], true),
                    new PublicOffer(pets[1], false),
                    new PublicOffer(pets[2], true),
                    new PublicOffer(pets[3], false),
                    new PublicOffer(pets[4], true),
                    new PublicOffer(pets[5], true)
                };
            PublicOfferService.Create(publicOffers);

            var userRequests = new List<UserRequests>
                {
                    new UserRequests(publicOffers[0], true),
                    new UserRequests(publicOffers[1], false),
                    new UserRequests(publicOffers[2], true),
                    new UserRequests(publicOffers[3], false),
                    new UserRequests(publicOffers[4], false),
                    new UserRequests(publicOffers[5], true)
                };
            UserRequestsService.Create(userRequests);
        }

        public static int CheckUserReturnsCode(string username, string password)
        {
            return UserService.AuthenticateUserReturnsCode(username, password);
        }
        public static void ReturnTownsAndCountries()
        {
            var towns = TownService.ReadAll();
            foreach (var town in towns)
            {
                Console.WriteLine(town.Name + " " + town.Country.Name);
            }
        }
    }
}
