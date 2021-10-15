using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class VisitaDTO:IDTO
    {
        public int Id { get; set; }
        public int EstablecimientoId { get; set; }
        public int TipoVisitaId { get; set; }
        public int? EmpleadoId { get; set; }
        public DateTime? Fecha { get; set; }
        public int MesPactado { get; set; }
        public int PlanId { get; set; }
        public int AnioPactado { get; set; }
        public int EstadoId { get; set; }
        public double? Duracion { get; set; }
        public bool Activo { get; set; }
    }
}
