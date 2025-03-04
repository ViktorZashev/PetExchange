using BusinessLayer.Models;
using DataLayer;
using DataLayer.ModelsDbContext;
using DataLayer.ProjectDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Functions
{
	public class UserService(PetExchangeDbContext _ProjectContext) : IDbWithNav<User, Guid>
    {
		public  UserDbContext _UserContext = new UserDbContext(_ProjectContext);

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
