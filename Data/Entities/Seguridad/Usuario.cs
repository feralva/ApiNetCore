using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Seguridad
{
    [Table("Usuario", Schema = "Seguridad")]
    public partial class Usuario
    {
        public Usuario()
        {
            UsuarioFamilia = new HashSet<UsuarioFamilia>();
            UsuarioPatente = new HashSet<UsuarioPatente>();
        }

        public string IdUsuario { get; set; }
        public byte[] Timestamp { get; set; }
        public string Contraseña { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public virtual ICollection<UsuarioFamilia> UsuarioFamilia { get; set; }
        public virtual ICollection<UsuarioPatente> UsuarioPatente { get; set; }
    }
}
