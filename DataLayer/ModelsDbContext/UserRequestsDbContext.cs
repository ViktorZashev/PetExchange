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
		public async Task CreateAsync(UserRequest entity)
		{
			try
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
			catch (Exception)
			{
				throw;
			}
		}

		public async Task CreateAsync(List<UserRequest> requests)
		{
			try
			{
				foreach (var request in requests)
				{
					await CreateAsync(request);
				}
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
					query = query.Include(e => e.Pet);
					query = query.Include(e => e.Sender);
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
					query = query.Include(e => e.Pet);
				}
				return await query.ToListAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<List<UserRequest>> ReadUserRequestOutboxAsync(Guid userId)
		{
			try
			{
				IQueryable<UserRequest> query = _dbcontext.Requests;

				query = query.AsNoTrackingWithIdentityResolution();
				query = query.Include(e => e.Pet);
				query = query.Include(e => e.Recipient).ThenInclude(r=> r.Town);
				query = query.Include(e => e.Sender);
				query = query.Where(e => e.SenderId == userId).OrderByDescending(x => x.CreatedOn);
				return await query.ToListAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<List<UserRequest>> ReadUserRequestInboxAsync(Guid userId)
		{
			try
			{
				IQueryable<UserRequest> query = _dbcontext.Requests;

				query = query.AsNoTrackingWithIdentityResolution();
				query = query.Include(e => e.Pet);
				query = query.Include(e => e.Recipient).ThenInclude(r=> r.Town);
				query = query.Include(e => e.Sender);
				query = query.Where(e => e.RecipientId == userId).OrderByDescending(x => x.CreatedOn);
				return await query.ToListAsync();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task CancelAsync(Guid requestId)
		{
			try
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
			catch (Exception)
			{

				throw;
			}
		}

		public async Task AcceptAsync(Guid requestId, string? message)
		{
			try
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
			catch (Exception)
			{

				throw;
			}
		}

		public async Task DenyAsync(Guid requestId, string? message)
		{
			try
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
