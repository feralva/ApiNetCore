using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Control
    {
        public UbicacionDTO Ubicacion { get; set; }
        public int UbicacionId { get; set; }        
        public EstablecimientoDTO Establecimiento { get; set; }
        public int EstablecimientoId { get; set; }
        public DateTime Fecha { get; set; }
        public List<Medicion> Mediciones { get; set; }
    }
}
