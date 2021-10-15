using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Establecimiento
{
    public class EstablecimientoSummaryDTO:IDTO
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public virtual DireccionDTO Direccion { get; set; }
        public int CantidadUbicaciones { get; set; }
    }
}
