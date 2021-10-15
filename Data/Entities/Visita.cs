using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Visita")]
    public class Visita:BaseEntity
    {
        [Column("Establecimiento_FK")]
        public int EstablecimientoId { get; set; }
        public virtual Establecimiento Establecimiento { get; set; }
        [Column("Tipo_Visita_FK")]
        public int TipoVisitaId { get; set; }
        public virtual TipoVisita TipoVisita { get; set; }
        [Column("Empleado_FK")]
        public int? EmpleadoId { get; set; }
        public virtual Empleado Empleado { get; set; }
        public DateTime? Fecha { get; set; }
        [Column("Mes_Pactado")]
        public int MesPactado { get; set; }        
        [Column("Anio_Pactado")]
        public int AnioPactado { get; set; }
        [Column("Plan_FK")]
        public int PlanId { get; set; }
        public virtual Plan Plan { get; set; }
        [Column("Estado_FK")]
        public int EstadoId { get; set; }
        public virtual EstadoVisita Estado { get; set; }
        public double? Duracion { get; set; }
        public bool Activo { get; set; }

    }
}