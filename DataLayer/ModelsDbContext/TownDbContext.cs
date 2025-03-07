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
        /*
        public async Task<List<Tuple<Town, int>>> ReadAllWithFilterAsync(bool ascendingNumberUsers, int page = 1, int pageSize = 10, bool isReadOnly = true)
        {
            try
            {
                var allUsers = await ReadAllAsync(useNavigationalProperties, isReadOnly);
                // filtering
                var filteredUsers = allUsers.Where(x =>
                        (String.IsNullOrWhiteSpace(username) || x.UserName.Contains(username))
                        && (String.IsNullOrWhiteSpace(name) || x.Name.Contains(name))
                        && (String.IsNullOrWhiteSpace(email) || x.Email.Contains(email))
                        && (String.IsNullOrWhiteSpace(town) || x.Town.ToString() == town)
                        && (String.IsNullOrWhiteSpace(role) || x.Role.ToString() == role)
                        ).ToList();
                // sorting
                if (ascendingUsername == true)
                {
                    filteredUsers = filteredUsers.OrderBy(x => x.UserName).ThenBy(x => x.Name).ToList();
                }
                // paging
                filteredUsers = filteredUsers.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return filteredUsers;
            }
            catch (Exception)
            {
                throw;
            }
        }
        */
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
