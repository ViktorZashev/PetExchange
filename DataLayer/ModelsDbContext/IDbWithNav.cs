namespace DataLayer
{
    public interface IDbWithNav<T, K>
    {
        // CRUD Operations
        Task CreateAsync(T entity);

        Task CreateAsync(List<T> entities);

        Task<T>? ReadAsync(K id, bool useNavigationalProperties = false, bool isReadOnly = true);

        Task<List<T>>? ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true);

        Task UpdateAsync(T entity, bool useNavigationalProperties = false);

        Task DeleteAsync(K id);
    }
}
