using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities.EntidadesNoPersistidas
{
    public class Control
    {
        public string Id { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public int UbicacionId { get; set; }
        public Establecimiento Establecimiento { get; set; }
        public int EstablecimientoId { get; set; }
        public DateTime Fecha { get; set; }
        public List<Medicion> Mediciones { get; set; }
    }
}
