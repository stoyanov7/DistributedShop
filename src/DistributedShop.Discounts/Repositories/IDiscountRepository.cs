namespace DistributedShop.Discounts.Repositories
{
    using DistributedShop.Discounts.Domain;
    using System.Threading.Tasks;

    public interface IDiscountRepository
    {
        Task AddAsync(Discount discount);
    }
}
