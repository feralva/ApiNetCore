using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class IrregularidadDTO:IDTO
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public string Establecimiento { get; set; }
        public int UbicacionId { get; set; }
        public virtual UbicacionDTO Ubicacion { get; set; }
        public int EstadoId { get; set; }
        public virtual string Estado { get; set; }
        public int EmpleadoId { get; set; }
        public int ClienteId { get; set; }
        public int EmpresaId { get; set; }
        public int TipoId { get; set; }
        public virtual string Tipo { get; set; }
        public string Descripcion { get; set; }
        public string Empleado { get; set; }
        public DateTime FechaDeteccion { get; set; }
        public DateTime? FechaFinalizado { get; set; }
        public string DescripcionResolucion { get; set; }
        public string Url { get; set; }
        public string UrlEvidenciaResultado { get; set; }

    }
}
