using Data.Entities.EntidadesNoPersistidas;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Services.Contracts
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        void AlmacenarRfreshToken(string idUsuario, string refreshToken, DateTime refreshTokenExpirationTime);
        Token ObtenerTokenRefrescoActual(string idUsuario);
    }
}
