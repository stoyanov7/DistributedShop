namespace DistributedShop.Common.Authentication
{
    using System.Collections.Generic;

    public class JsonWebToken
    {
        public string Id { get; set; }

        public string Role { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public long Expires { get; set; }

        public IDictionary<string, string> Claims { get; set; }
    }
}
