namespace DistributedShop.Products.Repositories
{
    using DistributedShop.Products.Domain;
    using System.Threading.Tasks;

    public interface IProductRepository
    {
        Task<bool> ExistsAsync(string name);

        Task AddAsync(Product product);
    }
}
