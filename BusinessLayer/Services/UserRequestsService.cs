using DataLayer;

namespace BusinessLayer
{
    public class UserRequestsService : IDbWithNav<UserRequest, Guid>
    {
        public UserRequestsDbContext  _UserRequestsContext;
        public UserRequestsService(PetExchangeDbContext _ProjectContext)
        {
            _UserRequestsContext = new UserRequestsDbContext(_ProjectContext);
        }
        public async Task CreateAsync(UserRequest entity)
        {
            await _UserRequestsContext.CreateAsync(entity);
        }
        public async Task CreateAsync(List<UserRequest> requests)
        {
            await _UserRequestsContext.CreateAsync(requests); 
        }

        public async Task<UserRequest>? ReadAsync(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await _UserRequestsContext.ReadAsync(id, useNavigationalProperties, isReadOnly);
        }

        public async Task<List<UserRequest>>? ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await _UserRequestsContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task<List<UserRequest>>? ReadUserRequestOutboxAsync(Guid userId)
        {
            return await _UserRequestsContext.ReadUserRequestOutboxAsync(userId);
        }

        public async Task<List<UserRequest>>? ReadUserRequestInboxAsync(Guid userId)
        {
            return await _UserRequestsContext.ReadUserRequestInboxAsync(userId);
        }

        public async Task CancelAsync(Guid requestId)
        {
            await _UserRequestsContext.CancelAsync(requestId);
        }

        public async Task AcceptAsync(Guid requestId, string? message)
        {
            await _UserRequestsContext.AcceptAsync(requestId,message);
        }

        public async Task DenyAsync(Guid requestId, string? message)
        {
            await _UserRequestsContext.DenyAsync(requestId, message);
        }


        public async Task UpdateAsync(UserRequest entity, bool useNavigationalProperties = false)
        {
            await _UserRequestsContext.UpdateAsync(entity, useNavigationalProperties);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _UserRequestsContext.DeleteAsync(id);
        }
    }
}
