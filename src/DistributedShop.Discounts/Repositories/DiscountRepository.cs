namespace DistributedShop.Discounts.Repositories
{
    using DistributedShop.Common.Mongo.Contracts;
    using DistributedShop.Discounts.Domain;
    using System.Threading.Tasks;

    public class DiscountRepository : IDiscountRepository
    {
        private readonly IMongoRepository<Discount> mongoRepository;

        public DiscountRepository(IMongoRepository<Discount> mongoRepository)
            => this.mongoRepository = mongoRepository;

        public async Task AddAsync(Discount discount) => await this.mongoRepository.AddAsync(discount);
    }
}
