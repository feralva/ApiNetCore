using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Seguridad
{
    [Table("Usuario_Patente", Schema = "Seguridad")]
    public partial class UsuarioPatente
    {
        public string IdUsuario { get; set; }
        public string IdPatente { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Patente IdPatenteNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
