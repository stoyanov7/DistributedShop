namespace DistributedShop.Products.Repositories
{
    using DistributedShop.Common.Mongo.Contracts;
    using DistributedShop.Products.Domain;
    using System.Threading.Tasks;

    public class ProductRepository : IProductRepository
    {
        private readonly IMongoRepository<Product> mongoRepository;

        public ProductRepository(IMongoRepository<Product> mongoRepository) => this.mongoRepository = mongoRepository;

        public async Task<bool> ExistsAsync(string name)
            => await this.mongoRepository.ExistsAsync(p => p.Name == name.ToLowerInvariant());

        public async Task AddAsync(Product product) => await this.mongoRepository.AddAsync(product);
    }
}
