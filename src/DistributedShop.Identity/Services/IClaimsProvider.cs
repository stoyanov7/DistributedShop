namespace DistributedShop.Identity.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IClaimsProvider
    {
        Task<IDictionary<string, string>> GetAsync(Guid userId);
    }
}
