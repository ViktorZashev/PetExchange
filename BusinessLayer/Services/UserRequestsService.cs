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

        public async Task<UserRequest>? ReadAsync(Guid id, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await _UserRequestsContext.ReadAsync(id, useNavigationalProperties, isReadOnly);
        }

        public async Task<List<UserRequest>>? ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await _UserRequestsContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
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
