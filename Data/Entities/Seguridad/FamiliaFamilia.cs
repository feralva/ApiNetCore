using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Seguridad
{
    [Table("Familia_Familia", Schema = "Seguridad")]
    public partial class FamiliaFamilia
    {
        public string IdFamilia { get; set; }
        public string IdFamiliaHijo { get; set; }
        public byte[] Timestamp { get; set; }

        public virtual Familia IdFamiliaHijoNavigation { get; set; }
        public virtual Familia IdFamiliaNavigation { get; set; }
    }
}
