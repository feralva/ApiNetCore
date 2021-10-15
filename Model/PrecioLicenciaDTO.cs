using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PrecioLicenciaDTO:IDTO
    {
        public double Precio { get; set; }
        public DateTime FechaDesde { get; set; }
        public int TipoLicenciaId { get; set; }
        //public virtual TipoLicenciaDTO TipoLicencia { get; set; }
    }
}
