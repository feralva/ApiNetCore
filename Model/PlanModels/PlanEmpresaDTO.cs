using System;
using System.Collections.Generic;
using System.Text;

namespace Model.PlanModels
{
    public class PlanEmpresaDTO
    {
        public int Id { get; set; }
        public string TipoPlan { get; set; }
        public string NombreCliente { get; set; }
        public string Estado { get; set; }
        public int VisitasPendientes { get; set; }
        public int Establecimientos { get; set; }
        public string UsuarioCreador { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
