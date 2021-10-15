using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Seguridad
{
    [Table("Usuario_Familia", Schema = "Seguridad")]
    public partial class UsuarioFamilia
    {
        public string IdUsuario { get; set; }
        public string IdFamilia { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Familia IdFamiliaNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
