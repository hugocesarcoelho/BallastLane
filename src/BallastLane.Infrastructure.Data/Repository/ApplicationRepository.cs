using BallastLane.Domain.Model;
using BallastLane.Infrastructure.Data.Repository.Interface;
using Infra.Data.Repository.Base;
using MongoDB.Driver;

namespace BallastLane.Infrastructure.Data.Repository
{
    public class ApplicationRepository : BaseRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, nameof(Application))
        {
        }
    }
}
