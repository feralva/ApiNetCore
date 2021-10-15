using Model.Seguridad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class EmpleadoDTO:IDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string UsuarioId { get; set; }
        public virtual UsuarioDTO Usuario { get; set; }
        public int EmpresaId { get; set; }
        public string urlFoto { get; set; }
        public bool? Activo { get; set; }
        public List<string> Roles { get; set; }
    }
}
