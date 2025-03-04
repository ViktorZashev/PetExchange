namespace DataLayer
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
