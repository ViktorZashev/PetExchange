using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class UserRequestsDbContext : IDbWithNav<UserRequest, Guid>
    {
        private readonly PetExchangeDbContext _dbcontext;

        public UserRequestsDbContext(PetExchangeDbContext context)
        {
            _dbcontext = context;
        }

        public async Task<List<UserRequest>> ReadAllWithFilterAsync(string petName, string petBreed, string senderName, string receiverName,
            int page, int pageSize, bool useNavigationalProperties = true, bool isReadOnly = true)
        {
            var allRequests = await ReadAllAsync(useNavigationalProperties, isReadOnly);
            // filtering
            var filteredRequests = allRequests.Where(x =>
            (String.IsNullOrWhiteSpace(petName) || x.Pet.Name.ToLower().Contains(petName.ToLower()))
            && (String.IsNullOrWhiteSpace(petBreed) || x.Pet.Breed.ToLower().Contains(petBreed.ToLower()))
            && (String.IsNullOrWhiteSpace(senderName) || x.Sender.Name.ToLower().Contains(senderName.ToLower()))
            && (String.IsNullOrWhiteSpace(receiverName) || x.Recipient.Name.ToLower().Contains(receiverName.ToLower()))
            ).ToList();
            // paging
            filteredRequests = filteredRequests.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return filteredRequests;
        }

        #region CRUD
        public async Task CreateAsync(UserRequest entity)
        {
            await _dbcontext.Requests.AddAsync(entity);
            var userFromDb = await _dbcontext.Users.FindAsync(entity.SenderId);
            if (userFromDb == null)
            {
                throw new Exception("Associated user is null!");
            }
            userFromDb.RequestOutbox.Add(entity);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task CreateAsync(List<UserRequest> requests)
        {
            foreach (var request in requests)
            {
                await CreateAsync(request);
            }
        }

        public async Task<UserRequest>? ReadAsync(Guid id, bool useNavigationalProperties = true, bool isReadOnly = true)
        {
            IQueryable<UserRequest> query = _dbcontext.Requests;

            if (isReadOnly)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }
            if (useNavigationalProperties)
            {
                query = query.Include(e => e.Pet).Include(e => e.Recipient).ThenInclude(e => e.Town).Include(e => e.Sender);
            }
            return await query.SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<UserRequest>> ReadAllAsync(bool useNavigationalProperties = true, bool isReadOnly = true)
        {
            IQueryable<UserRequest> query = _dbcontext.Requests;

            if (isReadOnly)
            {
                query = query.AsNoTrackingWithIdentityResolution();
            }
            if (useNavigationalProperties)
            {
                query = query.Include(e => e.Pet).Include(e => e.Recipient).ThenInclude(e => e.Town).Include(e => e.Sender);
            }
            return await query.ToListAsync();
        }

        public async Task<List<UserRequest>> ReadUserRequestOutboxAsync(Guid userId)
        {
            IQueryable<UserRequest> query = _dbcontext.Requests;

            query = query.AsNoTrackingWithIdentityResolution();
            query = query.Include(e => e.Pet);
            query = query.Include(e => e.Recipient).ThenInclude(r => r.Town);
            query = query.Include(e => e.Sender);
            query = query.Where(e => e.SenderId == userId).OrderByDescending(x => x.CreatedOn);
            return await query.ToListAsync();
        }

        public async Task<List<UserRequest>> ReadUserRequestInboxAsync(Guid userId)
        {
            IQueryable<UserRequest> query = _dbcontext.Requests;

            query = query.AsNoTrackingWithIdentityResolution();
            query = query.Include(e => e.Pet);
            query = query.Include(e => e.Recipient).ThenInclude(r => r.Town);
            query = query.Include(e => e.Sender);
            query = query.Where(e => e.RecipientId == userId).OrderByDescending(x => x.CreatedOn);
            return await query.ToListAsync();
        }

        public async Task CancelAsync(Guid requestId)
        {
            UserRequest requestFromDb = await ReadAsync(requestId, false, false);

            if (requestFromDb is null)
            {
                throw new ArgumentException("User request with id = " + requestId + "does not exist!");
            }
            requestFromDb.CanceledOn = DateTime.Now;

            _dbcontext.Requests.Update(requestFromDb);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task AcceptAsync(Guid requestId, string? message)
        {
            UserRequest requestFromDb = await ReadAsync(requestId, true, false);

            if (requestFromDb is null)
            {
                throw new ArgumentException("User request with id = " + requestId + "does not exist!");
            }
            requestFromDb.AcceptedOn = DateTime.Now;
            requestFromDb.AnswerMessage = message;
            requestFromDb.Pet.AdoptedOn = DateTime.Now;

            _dbcontext.Requests.Update(requestFromDb);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DenyAsync(Guid requestId, string? message)
        {
            UserRequest requestFromDb = await ReadAsync(requestId, true, false);

            if (requestFromDb is null)
            {
                throw new ArgumentException("User request with id = " + requestId + "does not exist!");
            }
            requestFromDb.DeniedOn = DateTime.Now;
            requestFromDb.AnswerMessage = message;

            _dbcontext.Requests.Update(requestFromDb);
            await _dbcontext.SaveChangesAsync();
        }


        public async Task UpdateAsync(UserRequest request, bool useNavigationalProperties = true)
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

        public async Task DeleteAsync(Guid key)
        {
            UserRequest request = await ReadAsync(key, false, false);

            if (request is null)
            {
                throw new ArgumentException("User request with id = " + key + " does not exist!");
            }

            _dbcontext.Requests.Remove(request);
            await _dbcontext.SaveChangesAsync();
        }
        #endregion
    }
}
