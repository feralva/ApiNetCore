using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace Data.Entities
{
    [Table("Ubicacion")]
    public class Ubicacion:BaseEntity
    {
        public string Nombre { get; set; }
        //[Column(TypeName = "geometry")]
        //public virtual Point Location { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        [Column("Establecimiento_FK")]
        public int EstablecimientoId { get; set; }
        public virtual Establecimiento Establecimiento { get; set; }
        public virtual ICollection<Irregularidad> Irregularidades { get; set; }
    }

}