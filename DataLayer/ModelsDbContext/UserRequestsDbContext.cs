using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class UserRequestsDbContext: IDbWithNav<UserRequest, Guid>
	{
		private readonly PetExchangeDbContext _dbcontext;
        public UserRequestsDbContext(PetExchangeDbContext context)
        {
            _dbcontext = context;
        }
        public async Task CreateAsync(UserRequest entity)
        {
            try
            {
                await _dbcontext.Requests.AddAsync(entity);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<UserRequest>? ReadAsync(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<UserRequest> query = _dbcontext.Requests;
                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                if (useNavigationalProperties)
                {
                    query = query.Include(e => e.PublicOffer);
                }
                return await query.SingleOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<UserRequest>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<UserRequest> query = _dbcontext.Requests;

                if (isReadOnly)
                {
                    query = query.AsNoTrackingWithIdentityResolution();
                }
                if (useNavigationalProperties)
                {
                    query = query.Include(e => e.PublicOffer);
                }
                return await query.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(UserRequest request, bool useNavigationalProperties = true)
        {
            try
            {
                UserRequest requestFromDb = await ReadAsync(request.Id, useNavigationalProperties, false);

                if (requestFromDb is null)
                {
                    throw new ArgumentException("User request with id = " + request.Id + "does not exist!");
                }
                if (useNavigationalProperties) _dbcontext.Requests.Update(request); // updates all linked entities
                else
                {
                    _dbcontext.Requests.Entry(requestFromDb).CurrentValues.SetValues(request); // updates only the core entity
                }

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
                UserRequest request = await ReadAsync(key, false, false);

                if (request is null)
                {
                    throw new ArgumentException("User request with id = " + key + " does not exist!");
                }

                _dbcontext.Requests.Remove(request);
                await _dbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
