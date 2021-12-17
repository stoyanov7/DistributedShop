namespace DistributedShop.Common.Authentication
{
    using System.Collections.Generic;

    public interface IJwtHandler
    {
        JsonWebToken CreateToken(string userId, string role = null, IDictionary<string, string> claims = null);
    }
}
