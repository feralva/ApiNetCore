using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Cliente")]
    public class Cliente:BaseEntity
    {
        public string Nombre { get; set; }
        [Column("Responsable_Empresa_FK")]
        public int ResponsableEmpresaId { get; set; }
        public virtual ResponsableEmpresa Responsable { get; set; }
        [Column("Direccion_FK")]
        public int DireccionId { get; set; }
        public virtual Direccion Direccion { get; set; }
        public virtual ICollection<Establecimiento> Establecimientos { get; set; }
        [Column("Empresa_FK")]
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<Plan> Planes { get; set; }
        public string UrlFoto { get; set; }
        public bool? Activo { get; set; }
    }
}