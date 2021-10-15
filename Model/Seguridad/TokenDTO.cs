using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Seguridad
{
    public class TokenDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationTime { get; set; }
    }
}
