using System;
using System.Collections.Generic;
using System.Text;
using NetTopologySuite.Geometries;

namespace Model
{
    public class UbicacionDTO:IDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        //public virtual Point Location { get; set; }

        public string Longitud { get; set; }
        public string Latitud { get; set; }
        public int EstablecimientoId { get; set; }
        public virtual ICollection<IrregularidadDTO> Irregularidades { get; set; }
    }
}
