using Microsoft.EntityFrameworkCore;

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
            try
            {
                await _dbcontext.Towns.AddAsync(entity);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<Town>? ReadAsync(Guid id, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Town> query = _dbcontext.Towns;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.SingleOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Town>> ReadAllAsync(bool isReadOnly = true)
        {
            try
            {
                IQueryable<Town> query = _dbcontext.Towns;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }

                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Town town)
        {
            try
            {
                Town townFromDb = await ReadAsync(town.Id,false);

                if (townFromDb is null)
                {
                    throw new ArgumentException("Pet with id = " + town.Id + "does not exist!");
                }

                _dbcontext.Towns.Entry(townFromDb).CurrentValues.SetValues(town); // updates only the core entity
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAsync(Guid key)
        {
            try
            {
                Town town = await ReadAsync(key, false);

                if (town is null)
                {
                    throw new ArgumentException("Town with id = " + key + " does not exist!");
                }

                _dbcontext.Towns.Remove(town);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
