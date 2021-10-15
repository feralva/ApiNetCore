using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    [Table("Tipo_Licencia")]
    public class TipoLicencia: BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad_Maxima_Usuarios { get; set; }
        public bool? Activo { get; set; }
        public virtual ICollection<PrecioLicencia> PreciosHistoricos { get; set; }
    }
}
