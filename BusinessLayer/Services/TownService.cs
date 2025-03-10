using DataLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer
{
    public class TownService : IDbWithoutNav<Town, Guid>
    {
        public TownDbContext _TownContext;

        public TownService(PetExchangeDbContext _ProjectContext)
        {
            _TownContext = new TownDbContext(_ProjectContext);
        }

        public async Task<List<Tuple<Town, int>>> ReadAllWithFilterAsync(bool ascendingNumberUsers, int page = 1, int pageSize = 10, bool isReadOnly = true)
        {
            return await _TownContext.ReadAllWithFilterAsync(ascendingNumberUsers, page, pageSize, isReadOnly);
        }
        public async Task<List<SelectOption>> GetTownOptions()
        {
            var result = new List<SelectOption>();
            foreach (var town in await ReadAllAsync())
            {
                result.Add(new SelectOption(label: town.Name, value: town.Id.ToString()));
            }
            result = result.OrderBy(x => x.Label).ToList();
            return result;
        }


        #region CRUD
        public async Task CreateAsync(Town entity)
        {
            await _TownContext.CreateAsync(entity);
        }
        public async Task CreateAsync(List<Town> towns)
        {
            await _TownContext.CreateAsync(towns);
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
        #endregion

    }
}
