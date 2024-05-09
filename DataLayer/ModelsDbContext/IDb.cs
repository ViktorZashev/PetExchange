using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ModelsDbContext
{
    public interface IDb<T, K>
    {
        // CRUD Operations
        void Create(T entity);
        T Read(K id, bool useNavigationalProperties = true);

        List<T> ReadAll(bool useNavigationalProperties = true);

        void Update(T entity, bool useNavigationalProperties = true);

        void Delete(K id);
    }
}
