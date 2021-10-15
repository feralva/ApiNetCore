using Data.Entities.EntidadesNoPersistidas;
using Data.Repositories.Contracts;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class TokenService : ITokenService
    {
        ISeguridadRepository repoSeguridad;
        public TokenService(ISeguridadRepository RepoSeguridad)
        {
            this.repoSeguridad = RepoSeguridad;
        }
        public void AlmacenarRfreshToken(string idUsuario, string refreshToken, DateTime refreshTokenExpirationTime)
        {
            this.repoSeguridad.AlmacenarRfreshToken(idUsuario, refreshToken, refreshTokenExpirationTime);
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("43729FD696AABBCC1A541BB1AB399FAD"));

            var token = new JwtSecurityToken(
                issuer: "ApiHigieneYSeguridad",
                audience: "ApiHigieneYSeguridad",
                expires: DateTime.Now.AddMinutes(10),
                claims: claims,
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("43729FD696AABBCC1A541BB1AB399FAD")),
                ValidateLifetime = false 
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public Token ObtenerTokenRefrescoActual(string idUsuario)
        {
            return this.repoSeguridad.ObtenerRefreshTokenUsuario(idUsuario);
        }
    }
}
