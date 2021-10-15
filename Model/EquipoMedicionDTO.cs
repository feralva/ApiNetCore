using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class EquipoMedicionDTO:IDTO
    {
        public string Nombre { get; set; }
        public int TipoEquipoMedicionId { get; set; }
        public int EmpresaId { get; set; }
        public bool? Activo { get; set; }
    }
}
