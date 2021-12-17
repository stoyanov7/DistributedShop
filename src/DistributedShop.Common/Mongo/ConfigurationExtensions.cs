namespace DistributedShop.Common.Mongo
{
    using DistributedShop.Common.Mongo.Contracts;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddMongoDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(option => configuration.GetSection(nameof(MongoDbSettings)).Bind(option));

            services.AddSingleton<IMongoClient>(context =>
            {
                var options = context.GetService<IOptions<MongoDbSettings>>();

                return new MongoClient(options.Value.ConnectionString);
            });

            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var options = sp.GetService<IOptions<MongoDbSettings>>();

                return client.GetDatabase(options.Value.Database);
            });

            services.AddTransient<IMongoDbSeeder, MongoDbSeeder>();
            services.AddTransient<IMongoDbInitializer, MongoDbInitializer>();
            services.AddScoped(c => c.GetService<IMongoClient>().StartSession());

            return services;
        }
    }
}
