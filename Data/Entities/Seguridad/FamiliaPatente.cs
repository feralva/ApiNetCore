using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Seguridad
{
    [Table("Familia_Patente", Schema = "Seguridad")]
    public partial class FamiliaPatente
    {
        public string IdFamilia { get; set; }
        public string IdPatente { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Familia IdFamiliaNavigation { get; set; }
        public virtual Patente IdPatenteNavigation { get; set; }
    }
}
