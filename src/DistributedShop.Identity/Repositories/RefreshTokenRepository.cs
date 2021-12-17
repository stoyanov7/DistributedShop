namespace DistributedShop.Identity.Repositories
{
    using DistributedShop.Common.Mongo.Contracts;
    using DistributedShop.Identity.Domain;
    using DistributedShop.Identity.Repositories.Contracts;
    using System.Threading.Tasks;

    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IMongoRepository<RefreshToken> mongoRepository;

        public RefreshTokenRepository(IMongoRepository<RefreshToken> mongoRepository) => this.mongoRepository = mongoRepository;

        public async Task AddAsync(RefreshToken token) => await mongoRepository.AddAsync(token);
    }
}
