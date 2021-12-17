namespace DistributedShop.Common.Mongo.Contracts
{
    using System.Threading.Tasks;

    public interface IMongoDbSeeder
    {
        Task SeedAsync();
    }
}
