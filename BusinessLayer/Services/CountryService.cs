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
    public static class CountryService
    {
        private static PetExchangeDbContext _ProjectContext = new PetExchangeDbContext();
        public static CountryDbContext  _CountryContext = new CountryDbContext(_ProjectContext);

        public static void Create(Country country)
        {
            // Validation
            _CountryContext.Create(country);
        }
       
        public static void Create(List<Country> entities)
        {
            foreach (var country in entities)
            {
                Create(country);
            }
        }

        public static Country Read(Guid idt, bool useNav = true)
        {

            return _CountryContext.Read(idt, useNav);
        }
        public static List<Country> ReadAll(bool useNav = true)
        {
  
            return _CountryContext.ReadAll(useNav);
        }
        public static void Update(Country country)
        {
            // validation
            _CountryContext.Update(country);
        }
        public static void Delete(Guid id)
        {
            // validation
            _CountryContext.Delete(id);
        }
        public static void DeleteAll()
        {
            var Countries = ReadAll();
            foreach (var Country in Countries)
            {
                Delete(Country.Id);
            }
        }
        public static Country RetrieveCountry(string name)
        {
            return _CountryContext.RetrieveCountry(name);
        }
		public static void LoadDb()
		{
			try
			{
				Delete(Guid.NewGuid());
			}
			catch
			{

			}
		}

	}
}
