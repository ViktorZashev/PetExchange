using BusinessLayer.Models;
using DataLayer;
using DataLayer.ProjectDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Functions
{
    public static class PetController
    {
        private static readonly PetExchangeDbContext _ProjectContext = _ProjectContext = new PetExchangeDbContext();
        private static readonly PetDbContext  _PetContext = new PetDbContext(_ProjectContext);

        public static void Create(Pet pet)
        {
            // Validation
            _PetContext.Create(pet);
        }
        public static Pet Read(Guid idt, bool useNav = false, bool isReadOnly = false)
        {
            
            return _PetContext.Read(idt, useNav,isReadOnly);
        }

        public static List<Pet> ReadAll(bool useNav = false, bool isReadOnly = false)
        {
  
            return _PetContext.ReadAll(useNav, isReadOnly);
        }
        public static void Update(Pet pet)
        {
            // validation
            _PetContext.Update(pet);
        }
        public static void Delete(Guid id)
        {
            // validation
            _PetContext.Delete(id);
        }
    }
}
