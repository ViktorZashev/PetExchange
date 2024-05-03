using BusinessLayer.Functions;
using BusinessLayer.Models;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Database_Functions
{
    public static class DatabaseFunctions
    {
        public static void DeleteAllEntries()
        {
            TownController.DeleteAll();
            CountryController.DeleteAll();
            PetController.DeleteAll();
            UserController.DeleteAll();
            UserRequestsController.DeleteAll();
            PublicOfferController.DeleteAll();
        }
        public static void InitialSetup()
        {
            var country = new Country(Guid.NewGuid(), "Bulgaria");
            var Pet = new Pet(Guid.NewGuid(), null, "Tropcho", string.Empty, 3, "Gerbil", "He is a cute gerbil who sleeps a lot", true);
            CountryController.Create(country);
        }
    }
}
