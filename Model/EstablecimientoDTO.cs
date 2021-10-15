using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class EstablecimientoDTO:IDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public virtual DireccionDTO Direccion { get; set; }
        public virtual ClienteDTO Cliente { get; set; }
        public int ClienteId { get; set; }
        public virtual ICollection<UbicacionDTO> Ubicaciones { get; set; }
        public bool? Activo { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public EstablecimientoDTO()
        {
            Activo = true;
        }
    }
}
