namespace DistributedShop.Common.Mongo
{
    using DistributedShop.Common.Mongo.Contracts;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Bson.Serialization.Serializers;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MongoDbInitializer : IMongoDbInitializer
    {
        private static bool initialized;
        private readonly bool seed;
        private readonly IMongoDbSeeder mongoSeeder;

        public MongoDbInitializer(IMongoDbSeeder mongoSeeder, IOptions<MongoDbSettings> mongoOptions)
        {
            this.mongoSeeder = mongoSeeder;
            seed = mongoOptions.Value.Seed;
        }

        public async Task InitializeAsync()
        {
            if (initialized)
                return;
            
            this.RegisterConventions();
            initialized = true;

            if (!seed)
                return;

            await this.mongoSeeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            BsonSerializer.RegisterSerializer(typeof(decimal), new DecimalSerializer(BsonType.Decimal128));
            BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(new DecimalSerializer(BsonType.Decimal128)));
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            ConventionRegistry.Register("Conventions", new MongoDbConventions(), x => true);
        }

        private class MongoDbConventions : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}
