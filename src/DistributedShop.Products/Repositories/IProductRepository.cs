namespace DistributedShop.Products.Repositories
{
    using DistributedShop.Products.Domain;
    using System;
    using System.Threading.Tasks;

    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);

        Task<bool> ExistsAsync(string name);

        Task AddAsync(Product product);
    }
}
