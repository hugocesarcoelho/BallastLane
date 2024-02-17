namespace BallastLane.Infrastructure.Data.Repository.Interface
{
    public interface IBaseRepository<T>
    {
        Task<T> CreateAsync(T param);
        Task UpdateAsync(string id, T param);
        Task DeleteAsync(string id);
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync(int offset, int fetch);
    }
}
