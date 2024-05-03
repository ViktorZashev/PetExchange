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
        T Read(K id, bool useNavigationalProperties = false, bool isReadOnly = true);

        List<T> ReadAll(bool useNavigationalProperties = false, bool isReadOnly = true);

        void Update(T entity, bool useNavigationalProperties = false);

        void Delete(K id);
    }
}
