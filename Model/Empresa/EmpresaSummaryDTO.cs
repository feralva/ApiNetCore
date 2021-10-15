using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Empresa
{
    public class EmpresaSummaryDTO:IDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ResponsableNombre { get; set; }
        public string Direccion{ get; set; }
        public string UrlFoto { get; set; }
    }
}
