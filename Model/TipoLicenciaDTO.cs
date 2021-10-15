using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TipoLicenciaDTO: IDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad_Maxima_Usuarios { get; set; }
        public double PrecioActual { get; set; }
        public bool? Activo { get; set; }
        public virtual ICollection<PrecioLicenciaDTO> PreciosHistoricos { get; set; }
    }
}
