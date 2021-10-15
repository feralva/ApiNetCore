using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Seguridad
{
    [Table("Familia", Schema = "Seguridad")]
    public partial class Familia
    {
        public Familia()
        {
            FamiliaFamiliaIdFamiliaHijoNavigation = new HashSet<FamiliaFamilia>();
            FamiliaFamiliaIdFamiliaNavigation = new HashSet<FamiliaFamilia>();
            FamiliaPatente = new HashSet<FamiliaPatente>();
            UsuarioFamilia = new HashSet<UsuarioFamilia>();
        }
        public string IdFamilia { get; set; }
        public string Nombre { get; set; }
        public byte[] Timestamp { get; set; }
        public virtual ICollection<FamiliaFamilia> FamiliaFamiliaIdFamiliaHijoNavigation { get; set; }
        public virtual ICollection<FamiliaFamilia> FamiliaFamiliaIdFamiliaNavigation { get; set; }
        public virtual ICollection<FamiliaPatente> FamiliaPatente { get; set; }
        public virtual ICollection<UsuarioFamilia> UsuarioFamilia { get; set; }
    }
}
