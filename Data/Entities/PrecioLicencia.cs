using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    [Table("Precio_Licencia")]
    public class PrecioLicencia : BaseEntity
    {
        public double Precio { get; set; }
        public DateTime FechaDesde { get; set; }
        [Column("TipoLicencia_FK")]
        public int TipoLicenciaId { get; set; }
        public virtual TipoLicencia TipoLicencia { get; set; }
    }
}
