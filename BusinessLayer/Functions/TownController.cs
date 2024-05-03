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
    public static class TownController
    {
        private static readonly PetExchangeDbContext _ProjectContext = new PetExchangeDbContext();
        private static readonly TownDbContext  _TownContext = new TownDbContext(_ProjectContext);

        public static void Create(Town town)
        {
            // Validation
            _TownContext.Create(town);
        }
        public static Town Read(Guid idt, bool useNav = false, bool isReadOnly = false)
        {
            
            return _TownContext.Read(idt, useNav,isReadOnly);
        }

        public static List<Town> ReadAll(bool useNav = false, bool isReadOnly = false)
        {
  
            return _TownContext.ReadAll(useNav, isReadOnly);
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
    }
}
