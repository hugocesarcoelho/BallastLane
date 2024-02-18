using BallastLane.Domain.Model;

namespace BallastLane.Infrastructure.Data.Repository.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task UpdateAsync(string id, User model);
    }
}
