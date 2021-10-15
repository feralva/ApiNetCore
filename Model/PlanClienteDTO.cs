using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PlanClienteDTO: IDTO
    {
        public int Id { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string Estado { get; set; }
        public string EmpleadoNombre { get; set; }
        public string TipoPlan { get; set; }
    }
}
