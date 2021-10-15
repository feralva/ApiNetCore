using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Pago")]
    public class Pago:BaseEntity
    {
        [Column("Empresa_FK")]
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
        [Column("Medio_Pago_FK")]
        public int MedioPagoId { get; set; }
        public virtual MedioPago MedioPago { get; set; }
        public double Monto { get; set; }
        public string TokenPago { get; set; }
        [Column("Tipo_Licencia_FK")]
        public int TipoLicenciaId { get; set; }
        public virtual TipoLicencia TipoLicencia { get; set; }
        public double PrecioLicencia { get; set; }
        [Column("Cantidad_Meses")]
        public int CantidadMeses { get; set; }
        public DateTime Fecha { get; set; }
    }
}