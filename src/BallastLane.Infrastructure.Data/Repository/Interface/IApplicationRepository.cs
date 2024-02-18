using BallastLane.Domain.Model;

namespace BallastLane.Infrastructure.Data.Repository.Interface
{
    public interface IApplicationRepository : IBaseRepository<Application>
    {
        Task UpdateAsync(string id, Application model);
    }
}
