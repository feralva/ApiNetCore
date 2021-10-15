using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    [Table("Licencia")]
    public class Licencia: BaseEntity
    {
        [Column("Empresa_FK")]
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
        [Column("Tipo_Licencia_FK")]
        public int TipoLicenciaId { get; set; }
        public virtual TipoLicencia TipoLicencia { get; set; }
        //[Column("Fecha_Inicio")]
        //public DateTime? FechaInicio { get; set; }
        [Column("Fecha_Fin")]
        public DateTime? FechaFin { get; set; }
        [Column("Estado_FK")]
        public int EstadoId { get; set; }
        public virtual EstadoLicencia Estado { get; set; }
        //[Column("Pago_FK")]
        //public int PagoId { get; set; }
        //public virtual Pago Pago { get; set; }

    }
}
