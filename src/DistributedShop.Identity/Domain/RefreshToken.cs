namespace DistributedShop.Identity.Domain
{
    using DistributedShop.Common.Types;
    using Microsoft.AspNetCore.Identity;
    using System;

    public class RefreshToken : IIdentifiable
    {
        public RefreshToken(User user, IPasswordHasher<User> passwordHasher)
        {
            this.Id = Guid.NewGuid();
            this.UserId = user.Id;
            this.CreatedAt = DateTime.UtcNow;
            this.Token = CreateToken(user, passwordHasher);
        }

        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }

        public string Token { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? RevokedAt { get; private set; }

        public bool Revoked => this.RevokedAt.HasValue;

        private static string CreateToken(User user, IPasswordHasher<User> passwordHasher)
            => passwordHasher.HashPassword(user, Guid.NewGuid().ToString("N"))
                .Replace("=", string.Empty)
                .Replace("+", string.Empty)
                .Replace("/", string.Empty);
    }
}
