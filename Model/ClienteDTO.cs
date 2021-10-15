using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ClienteDTO:IDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int ResponsableEmpresaId { get; set; }
        public virtual ResponsableEmpresa Responsable { get; set; }
        public int DireccionId { get; set; }
        public virtual DireccionDTO Direccion { get; set; }
        public virtual ICollection<EstablecimientoDTO> Establecimientos { get; set; }
        public virtual ICollection<Plan> Planes { get; set; }
        public int EmpresaId { get; set; }
        public string UrlFoto { get; set; }

        public bool? Activo { get; set; }

        public ClienteDTO()
        {
            Activo = true;
        }
    }
}
