using DataLayer;

namespace BusinessLayer
{
	public class UserService : IDbWithNav<User, Guid>
    {
		public  UserDbContext _UserContext;

        public UserService(PetExchangeDbContext _ProjectContext)
        {
            _UserContext = new UserDbContext(_ProjectContext);
        }
        public async Task CreateAsync(User entity)
        {
            await _UserContext.CreateAsync(entity);
        }

        public async Task<User>? ReadAsync(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await _UserContext.ReadAsync(id, useNavigationalProperties, isReadOnly);
        }

        public async Task<List<User>>? ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await _UserContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(User entity, bool useNavigationalProperties = false)
        {
            await _UserContext.UpdateAsync(entity, useNavigationalProperties);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _UserContext.DeleteAsync(id);
        }
    }
}
