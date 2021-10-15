using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ResponsableEmpresaDTO: IDTO
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
    }
}
