using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PagoDTO: IDTO
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public virtual EmpresaDTO Empresa { get; set; }
        public int MedioPagoId { get; set; }
        public virtual MedioPagoDTO MedioPago { get; set; }
        public double Monto { get; set; }
        public string TokenPago { get; set; }
        public int TipoLicenciaId { get; set; }
        public virtual TipoLicenciaDTO TipoLicencia { get; set; }
        public double PrecioLicencia { get; set; }
        public int CantidadMeses { get; set; }
        public DateTime Fecha { get; set; }
    }
}
