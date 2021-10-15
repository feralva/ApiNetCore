using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    [Table("Tasa_Conversion_Licencia")]
    public class TasaConversionTipoLicencia: BaseEntity
    {
        [Column("Tipo_Licencia_Origen_FK")]
        public int TipoLicenciaOrigenId { get; set; }
        public virtual TipoLicencia TipoLicenciaOrigen { get; set; }        
        
        [Column("Tipo_Licencia_Destino_FK")]
        public int TipoLicenciaDestinoId { get; set; }
        public virtual TipoLicencia TipoLicenciaDestino { get; set; }

        public double RatioConversion { get; set; }
    }
}
