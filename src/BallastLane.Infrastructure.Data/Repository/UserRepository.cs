using BallastLane.Domain.Model;
using BallastLane.Infrastructure.Data.Repository.Interface;
using Infra.Data.Repository.Base;
using MongoDB.Driver;

namespace BallastLane.Infrastructure.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, nameof(User))
        {
            _collection = mongoDatabase.GetCollection<User>(nameof(User));
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _collection.Find(model => model.Username == username).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, User model)
        {
            await _collection.FindOneAndUpdateAsync(
                Builders<User>.Filter.Eq(c => c.Id, id),
                Builders<User>.Update
                    .Set(c => c.Name, model.Name)
                    .Set(c => c.Username, model.Username)
                    .Set(c => c.Email, model.Email),
                new FindOneAndUpdateOptions<User> { ReturnDocument = ReturnDocument.After });
        }
    }
}
