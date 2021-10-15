using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Seguridad
{
    public class UsuarioDetalleDTO: IDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> roles { get; set; }
        public IEnumerable<string> patentes { get; set; }
        public int EmpresaId { get; set; }
        public int EmpleadoId { get; set; }
        public string EmpresaNombre { get; set; }
        public string UrlFotoEmpleado { get; set; }
        public string urlFotoEmpresa { get; set; }

    }
}
