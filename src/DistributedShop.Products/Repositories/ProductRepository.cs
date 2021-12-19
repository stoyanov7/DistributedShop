namespace DistributedShop.Products.Repositories
{
    using DistributedShop.Common.Mongo.Contracts;
    using DistributedShop.Products.Domain;
    using System;
    using System.Threading.Tasks;

    public class ProductRepository : IProductRepository
    {
        private readonly IMongoRepository<Product> mongoRepository;

        public ProductRepository(IMongoRepository<Product> mongoRepository) => this.mongoRepository = mongoRepository;

        public async Task<Product> GetByIdAsync(Guid id) => await this.mongoRepository.GetByIdAsync(id);

        public async Task<bool> ExistsAsync(string name)
            => await this.mongoRepository.ExistsAsync(p => p.Name == name.ToLowerInvariant());

        public async Task<bool> ExistsAsync(Guid id) => await this.mongoRepository.ExistsAsync(p => p.Id == id);

        public async Task AddAsync(Product product) => await this.mongoRepository.AddAsync(product);

        public async Task UpdateAsync(Product product) => await this.mongoRepository.UpdateAsync(product);

        public async Task DeleteAsync(Guid id) => await this.mongoRepository.DeleteAsync(id);
    }
}
