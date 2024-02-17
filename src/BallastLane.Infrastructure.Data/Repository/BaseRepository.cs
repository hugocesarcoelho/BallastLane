using BallastLane.Domain.Model;
using BallastLane.Domain.Settings;
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
            await _collection.InsertOneAsync(param);
            return param;
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync(int offset, int fetch)
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(model => model.Id == id).FirstOrDefaultAsync();
        }

        public Task UpdateAsync(string id, T param)
        {
            throw new NotImplementedException();
        }
    }
}
