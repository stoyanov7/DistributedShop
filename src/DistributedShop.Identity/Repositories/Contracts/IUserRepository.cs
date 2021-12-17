namespace DistributedShop.Identity.Repositories.Contracts
{
    using DistributedShop.Identity.Domain;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<User> GetAsync(string email);

        Task AddAsync(User user);
    }
}
