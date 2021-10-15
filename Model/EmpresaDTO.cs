using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class EmpresaDTO:IDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ResponsableEmpresaDTO Responsable { get; set; }
        public DireccionDTO Direccion { get; set; }
        public string UrlFoto { get; set; }
        public bool? Activo { get; set; }
    }
}
