namespace DistributedShop.Identity.Handlers
{
    using DistributedShop.Common.Authentication;
    using DistributedShop.Common.Mediator.Types;
    using DistributedShop.Identity.Domain;
    using DistributedShop.Identity.Dto;
    using DistributedShop.Identity.Infrastructure.Services;
    using DistributedShop.Identity.Repositories.Contracts;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Threading.Tasks;

    public class SignInHandler : IQueryHandler<SignInInputModel, JsonWebToken>
    {
        private readonly IUserRepository userRepository;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IClaimsProvider claimsProvider;
        private readonly IJwtHandler jwtHandler;

        public SignInHandler(
            IUserRepository userRepository, 
            IRefreshTokenRepository refreshTokenRepository, 
            IPasswordHasher<User> passwordHasher, 
            IClaimsProvider claimsProvider, 
            IJwtHandler jwtHandler)
        {
            this.userRepository = userRepository;
            this.refreshTokenRepository = refreshTokenRepository;
            this.passwordHasher = passwordHasher;
            this.claimsProvider = claimsProvider;
            this.jwtHandler = jwtHandler;
        }

        public async Task<JsonWebToken> HandleAsync(SignInInputModel query)
        {
            var user = await this.userRepository.GetAsync(query.Email);
            if (user == null || !user.ValidatePassword(query.Password, this.passwordHasher))
                throw new Exception("Invalid credentials.");

            var claims = await this.claimsProvider.GetAsync(user.Id);
            var jwt = jwtHandler.CreateToken(user.Id.ToString("N"), user.Role, claims);
            var refreshToken = new RefreshToken(user, this.passwordHasher);
            jwt.RefreshToken = refreshToken.Token;

            await refreshTokenRepository.AddAsync(refreshToken);

            return jwt;
        }
    }
}
