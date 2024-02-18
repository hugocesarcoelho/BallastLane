using BallastLane.Domain.Model;
using BallastLane.Infrastructure.Data.Repository.Interface;
using MongoDB.Driver;

namespace Infra.Data.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : ModelBase
    {
        private readonly IMongoCollection<T> _collection;

        public BaseRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<T> CreateAsync(T param)
        {
            param.Id = Guid.NewGuid().ToString();
            await _collection.InsertOneAsync(param);
            return param;
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(c => c.Id == id);            
        }

        public async Task<IEnumerable<T>> GetAllAsync(int offset, int fetch)
        {
            var filter = Builders<T>.Filter.Empty;

            return _collection
                .Find(filter)
                .Skip(offset)
                .Limit(fetch)
                .ToList();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(model => model.Id == id).FirstOrDefaultAsync();
        }
    }
}
