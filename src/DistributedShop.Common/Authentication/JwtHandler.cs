namespace DistributedShop.Common.Authentication
{
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;

    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings jwtSettings;
        private readonly SigningCredentials signingCredentials;

        public JwtHandler(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
        }

        public JsonWebToken CreateToken(string userId, string role = null, IDictionary<string, string> claims = null)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User id claim can not be empty.", nameof(userId));
            }

            var now = DateTime.UtcNow;
            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString()),
            };

            if (!string.IsNullOrWhiteSpace(role))
            {
                jwtClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var customClaims = claims?.Select(claim => new Claim(claim.Key, claim.Value)).ToArray() ?? Array.Empty<Claim>();
            jwtClaims.AddRange(customClaims);

            var expires = now.AddMinutes(jwtSettings.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                RefreshToken = string.Empty,
                Expires = expires.ToTimestamp(),
                Id = userId,
                Role = role ?? string.Empty,
                Claims = customClaims.ToDictionary(c => c.Type, c => c.Value)
            };
        }
    }
}
