namespace DistributedShop.Identity.Repositories
{
    using DistributedShop.Common.Mongo.Contracts;
    using DistributedShop.Identity.Domain;
    using DistributedShop.Identity.Repositories.Contracts;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User> mongoRepository;

        public UserRepository(IMongoRepository<User> mongoRepository) => this.mongoRepository = mongoRepository;

        public async Task<User> GetAsync(string email)
            => await this.mongoRepository.GetAsync(x => x.Email == email.ToLowerInvariant());

        public async Task AddAsync(User user) => await mongoRepository.AddAsync(user);
    }
}
