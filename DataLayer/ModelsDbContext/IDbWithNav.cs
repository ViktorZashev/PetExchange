using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ModelsDbContext
{
    public interface IDbWithNav<T, K>
    {
        // CRUD Operations
        Task CreateAsync(T entity);
        Task<T>? ReadAsync(K id, bool useNavigationalProperties = false, bool isReadOnly = true);

        Task<List<T>>? ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true);

        Task UpdateAsync(T entity, bool useNavigationalProperties = false);

        Task DeleteAsync(K id);
    }
}
