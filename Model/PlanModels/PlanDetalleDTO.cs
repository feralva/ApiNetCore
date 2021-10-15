using Model.Visita;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.PlanModels
{
    public class PlanDetalleDTO: IDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Cliente { get; set; }
        public int IdCliente { get; set; }
        public string usuarioCreador { get; set; }
        public string Estado { get; set; }
        public IEnumerable<VisitaSummaryDTO> Visitas { get; set; }

    }
}
