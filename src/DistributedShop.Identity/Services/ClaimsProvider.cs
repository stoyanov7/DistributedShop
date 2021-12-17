namespace DistributedShop.Identity.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ClaimsProvider : IClaimsProvider
    {
        public async Task<IDictionary<string, string>> GetAsync(Guid userId)
            => await Task.FromResult(new Dictionary<string, string>());
    }
}
