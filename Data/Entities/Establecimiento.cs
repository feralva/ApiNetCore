using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Establecimiento")]
    public class Establecimiento:BaseEntity
    {
        public string Nombre { get; set; }
        [Column("Direccion_FK")]
        public int DireccionId { get; set; }
        public virtual Direccion Direccion { get; set; }
        [Column("Cliente_FK")]
        public int ClienteId { get; set; }
        [NotMapped]
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<Ubicacion> Ubicaciones { get; set; }
        public virtual ICollection<PlanEstablecimiento> PlanesEstablecimientos { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public bool? Activo { get; set; }

    }
}