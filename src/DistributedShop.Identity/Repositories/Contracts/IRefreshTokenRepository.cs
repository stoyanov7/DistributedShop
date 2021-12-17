namespace DistributedShop.Identity.Repositories.Contracts
{
    using DistributedShop.Identity.Domain;
    using System.Threading.Tasks;

    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
    }
}
