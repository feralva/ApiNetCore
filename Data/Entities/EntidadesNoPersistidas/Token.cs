using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities.EntidadesNoPersistidas
{
    public class Token
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationTime { get; set; }
    }
}
