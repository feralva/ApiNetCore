using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PlanDTO:IDTO
    {
        public int Id { get; set; }
        public int TipoPlanId { get; set; }
        
        public virtual IList<PlanEstablecimientoDTO> PlanesEstablecimientos { get; set; }
        public int ClienteId { get; set; }
        public virtual IList<VisitaDTO> Visitas { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int EmpleadoId { get; set; }
        public int EstadoId { get; set; }
        public int EmpresaId { get; set; }
        public bool Activo { get; set; }
    }
}
