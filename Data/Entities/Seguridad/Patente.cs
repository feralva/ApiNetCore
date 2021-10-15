using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Seguridad
{
    [Table("Patente", Schema = "Seguridad")]
    public partial class Patente
    {
        public Patente()
        {
            FamiliaPatente = new HashSet<FamiliaPatente>();
            UsuarioPatente = new HashSet<UsuarioPatente>();
        }

        public string IdPatente { get; set; }
        public string Nombre { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual ICollection<FamiliaPatente> FamiliaPatente { get; set; }
        public virtual ICollection<UsuarioPatente> UsuarioPatente { get; set; }
    }
}
