namespace SteamAPI.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<IQueryable<T>> Get(int page, int maxResults);

        Task<T?> GetByKey(int key);

        Task<T> Insert(T entity);

        Task<T> Update(int key, T entity);

        Task<int> Delete(int key);
    }
}
