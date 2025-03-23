using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataLayer
{
    public class TownDbContext : IDbWithoutNav<Town, Guid>
    {
        private readonly PetExchangeDbContext _dbcontext;
        public TownDbContext(PetExchangeDbContext context)
        {
            _dbcontext = context;
        }

        public async Task CreateAsync(Town entity)
        {
            if (_dbcontext.Towns.Count(x => x.Name == entity.Name) > 0)
            {
                throw new DuplicateNameException("There is already a town with the same name.");
            }
            await _dbcontext.Towns.AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
        }
        public async Task CreateAsync(List<Town> towns)
        {
            foreach (var town in towns)
            {
                await CreateAsync(town);
            }
        }

        #region CRUD
        public async Task<Town>? ReadAsync(Guid id, bool isReadOnly = true)
        {
            IQueryable<Town> query = _dbcontext.Towns;

            if (isReadOnly)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }

            return await query.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Town>> ReadAllAsync(bool isReadOnly = true)
        {
            IQueryable<Town> query = _dbcontext.Towns;

            if (isReadOnly)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Town town)
        {
            Town townFromDb = await ReadAsync(town.Id, false);

            if (townFromDb is null)
            {
                throw new ArgumentException("Pet with id = " + town.Id + "does not exist!");
            }

            _dbcontext.Towns.Entry(townFromDb).CurrentValues.SetValues(town); // updates only the core entity
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid key)
        {
            Town town = await ReadAsync(key, false);

            if (town is null)
            {
                throw new ArgumentException("Town with id = " + key + " does not exist!");
            }

            _dbcontext.Towns.Remove(town);
            await _dbcontext.SaveChangesAsync();
        }
        #endregion
    }
}
