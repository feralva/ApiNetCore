using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities.EntidadesNoPersistidas
{
    public class VisitaSummary
    {
        public int id { get; set; }
        public string NombreEstablecimiento { get; set; }
        public string NombreCliente { get; set; }
        public string TipoVisita { get; set; }
        public string EmpleadoAsignado { get; set; }
        public string Estado { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime FechaPactada { get; set; }
    }
}
