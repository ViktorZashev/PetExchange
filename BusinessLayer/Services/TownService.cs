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
    public class TownService(PetExchangeDbContext _ProjectContext) : IDbWithoutNav<Town, Guid>
    {
        public TownDbContext  _TownContext = new TownDbContext(_ProjectContext);

        public async Task CreateAsync(Town entity)
        {
            await _TownContext.CreateAsync(entity);
        }

        public async Task<Town>? ReadAsync(Guid id,bool isReadOnly = true)
        {
            return await _TownContext.ReadAsync(id, isReadOnly);
        }

        public async Task<List<Town>>? ReadAllAsync(bool isReadOnly = true)
        {
            return await _TownContext.ReadAllAsync(isReadOnly);
        }

        public async Task UpdateAsync(Town entity)
        {
            await _TownContext.UpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _TownContext.DeleteAsync(id);
        }
    }
}
