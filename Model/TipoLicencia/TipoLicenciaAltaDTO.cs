using System;
using System.Collections.Generic;
using System.Text;

namespace Model.TipoLicencia
{
    public class TipoLicenciaAltaDTO:IDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad_Maxima_Usuarios { get; set; }
        public double PrecioActual { get; set; }
        public bool Activo { get; set; }
    }
}
