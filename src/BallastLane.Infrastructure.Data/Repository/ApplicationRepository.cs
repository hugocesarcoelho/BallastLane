using BallastLane.Domain.Model;
using BallastLane.Infrastructure.Data.Repository.Interface;
using Infra.Data.Repository.Base;
using MongoDB.Driver;

namespace BallastLane.Infrastructure.Data.Repository
{
    public class ApplicationRepository : BaseRepository<Application>, IApplicationRepository
    {
        private readonly IMongoCollection<Application> _collection;

        public ApplicationRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, nameof(Application))
        {
            _collection = mongoDatabase.GetCollection<Application>(nameof(Application));
        }

        public async Task UpdateAsync(string id, Application model)
        {
            await _collection.FindOneAndUpdateAsync(
                Builders<Application>.Filter.Eq(c => c.Id, id),
                Builders<Application>.Update
                    .Set(c => c.Name, model.Name)
                    .Set(c => c.Description, model.Description),
                new FindOneAndUpdateOptions<Application> { ReturnDocument = ReturnDocument.After });
        }
    }
}
