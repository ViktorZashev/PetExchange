using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ModelsDbContext
{
    public interface IDbWithoutNav<T,K>
    {
        // CRUD Operations
        Task CreateAsync(T entity);
        Task<T>? ReadAsync(K id, bool isReadOnly = true);

        Task<List<T>>? ReadAllAsync(bool isReadOnly = true);

        Task UpdateAsync(T entity);

        Task DeleteAsync(K id);
    }
}
