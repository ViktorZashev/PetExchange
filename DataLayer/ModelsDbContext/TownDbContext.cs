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
        public async Task CreateAsync(List<Town> towns)
        {
            try
            {
                foreach (var town in towns)
                {
                    await CreateAsync(town);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<List<Tuple<Town, int>>> ReadAllWithFilterAsync(bool ascendingNumberUsers, int page = 1, int pageSize = 10, bool isReadOnly = true)
        {
            try
            {
                var allTowns = await ReadAllAsync(isReadOnly);
                // filtering
                var allUsers = await _dbcontext.Users.ToListAsync();
                var filteredList = new List<Tuple<Town, int>>();
                foreach (var town in allTowns)
                {
                    var numberOfUsersInTown = allUsers.Where(u => u.TownId == town.Id).Count();
                    if(numberOfUsersInTown > 0)
                    {
                        filteredList.Add(new Tuple<Town,int>(town,numberOfUsersInTown));
                    }
                }
                // sorting
                if (ascendingNumberUsers)
                {
                    filteredList = filteredList.OrderBy(x => x.Item2).ToList();
                }
                else
                {
                    filteredList = filteredList.OrderByDescending(x => x.Item2).ToList();
                }

                // paging
                filteredList = filteredList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return filteredList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        #region CRUD
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
        #endregion
    }
}
