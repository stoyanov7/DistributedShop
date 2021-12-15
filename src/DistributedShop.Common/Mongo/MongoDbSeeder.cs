namespace DistributedShop.Common.Mongo
{
    using DistributedShop.Common.Mongo.Contracts;
    using MongoDB.Driver;
    using System.Linq;
    using System.Threading.Tasks;

    public class MongoDbSeeder : IMongoDbSeeder
    {
        protected readonly IMongoDatabase mongoDatabase;

        public MongoDbSeeder(IMongoDatabase database) => this.mongoDatabase = database;

        public async Task SeedAsync() => await this.CustomSeedAsync();

        protected virtual async Task CustomSeedAsync()
        {
            var cursor = await mongoDatabase.ListCollectionsAsync();
            var collections = await cursor.ToListAsync();

            if (collections.Any())
                return;

            await Task.CompletedTask;
        }
    }
}
