namespace DistributedShop.Products.Repositories
{
    using DistributedShop.Products.Domain;
    using System;
    using System.Threading.Tasks;

    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(Guid id);

        Task<bool> ExistsAsync(string name);

        Task<bool> ExistsAsync(Guid id);

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Guid id);
    }
}
