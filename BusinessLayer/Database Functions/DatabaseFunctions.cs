using BusinessLayer.Functions;
using BusinessLayer.Models;
using DataLayer;
using DataLayer.Models;
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
        public static string adminPassword = "admin";
        /*
        public static void SeedDatabase()
        {
            DeleteAllEntries();
 
            var towns = new List<Town>
                {
                    new("New York"),
                    new("Toronto"),
                    new("London"),
                    new("Sydney"),
                    new("Berlin"),
                    new("Plovdiv")
                };
            TownService.Create(towns);

            var users = new List<User>
                {
                    new (towns[0],[],"John Doe",RoleEnum.User, false, "john@example.com", "john", "password"),
                    new (towns[1],[], "Alice Smith", "photoPath", false, "alice@example.com", "alice", "password"),
                    new (towns[2],[], "Bob Johnson", "photoPath", false, "bob@example.com", "bob", "password"),
                    new (towns[3],[], "Emily Brown", "photoPath", false, "emily@example.com", "emily", "password"),
                    new (towns[4],[], "Michael Wilson", "photoPath", false, "michael@example.com", "michael", "password"),
                    new (towns[5],[], "Viktor Zashev", "photoPath", false, "vbzashev@gmail.com", "vzashev", "TropchoEnjoyer")
                };

            UserService.Create(users);

            var pets = new List<Pet>
                {
                    new (users[0], "Max", string.Empty, 3, PetTypeEnum.Dog, "Friendly dog", true),
                    new (users[1], "Whiskers", string.Empty, 2, PetTypeEnum.Cat, "Playful cat", false),
                    new (users[2], "Buddy", string.Empty, 4, PetTypeEnum.Dog, "Loyal companion", true),
                    new (users[3], "Mittens", string.Empty, 1, PetTypeEnum.Cat, "Curious kitten", false),
                    new (users[4], "Rocky", string.Empty, 5, PetTypeEnum.Dog, "Energetic pup", true),
                    new (users[5], "Tropcho", string.Empty, 5, PetTypeEnum.SmallMammal, "Sleeping beauty", true)
                };
            PetService.Create(pets);

            var publicOffers = new List<PublicOffer>
                {
                    new (pets[0]),
                    new (pets[1]),
                    new (pets[2]),
                    new (pets[3]),
                    new (pets[4]),
                    new (pets[5])
                };
            PublicOfferService.Create(publicOffers);

            var userRequests = new List<UserRequests>
                {
                    new (publicOffers[0],users[0], true),
                    new (publicOffers[1],users[1], true),
                    new (publicOffers[2], users[2], true),
                    new (publicOffers[3], users[3], true),
                    new (publicOffers[4], users[4], true),
                    new (publicOffers[5], users[5], true)
                };
            UserRequestsService.Create(userRequests);
        }
        */
    }
}
