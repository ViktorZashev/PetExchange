using BusinessLayer.Models;
using DataLayer;
using DataLayer.ModelsDbContext;
using DataLayer.ProjectDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Functions
{
    public static class TownService
    {
        private static readonly PetExchangeDbContext _ProjectContext = new PetExchangeDbContext();
        public static TownDbContext  _TownContext = new TownDbContext(_ProjectContext);

        public static void Create(Town town)
        {
            // Validation
            _TownContext.Create(town);
        }

        public static void Create(List<Town> towns)
        {
            foreach (var town in towns)
            {
                Create(town);
            }
        }
        public static Town Read(Guid idt, bool useNav = true)
        {
            
            return _TownContext.Read(idt, useNav);
        }

        public static List<Town> ReadAll(bool useNav = true)
        {
  
            return _TownContext.ReadAll(useNav);
        }
        public static void Update(Town town)
        {
            // validation
            _TownContext.Update(town);
        }
        public static void Delete(Guid id)
        {
            // validation
            _TownContext.Delete(id);
        }
        public static void DeleteAll()
        {
            var Towns = ReadAll();
            foreach (var Town in Towns)
            {
                Delete(Town.Id);
            }
        }
        public static bool CheckIfExists(string name)
        {
            return _TownContext.CheckExists(name);
        }
        public static Town RetrieveTown(string name)
        {
            var foundTown = _TownContext.ReadAll().Where(x => x.Name == name).FirstOrDefault();
            if(foundTown == null)
            {
                throw new IndexOutOfRangeException("No such town is found!");
            }
            return foundTown;
        }
        /*
        public static Country RetrieveCountry(Town town)
        {
            var countries = CountryService.ReadAll();
            if(countries.Where(x => x.Id == town.Country.)
        }
        */
    }
}
