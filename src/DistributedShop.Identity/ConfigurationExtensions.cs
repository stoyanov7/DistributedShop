namespace DistributedShop.Identity
{
    using DistributedShop.Common.Mongo;
    using DistributedShop.Common.Mongo.Contracts;
    using DistributedShop.Identity.Domain;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddMongoRepositories(this IServiceCollection services)
            => services
                .AddScoped(typeof(IMongoRepository<User>), typeof(MongoRepository<User>))
                .AddScoped(typeof(IMongoRepository<RefreshToken>), typeof(MongoRepository<RefreshToken>));
    }
}
