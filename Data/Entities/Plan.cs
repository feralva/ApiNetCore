using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Plan")]
    public class Plan:BaseEntity
    {
        [Column("Tipo_Plan_FK")]
        public int TipoPlanId { get; set; }
        public virtual TipoPlan TipoPlan { get; set; }
        public virtual IList<PlanEstablecimiento> PlanesEstablecimientos { get; set; }
        [Column("Cliente_FK")]
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }
        [Column("Empresa_FK")]
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual IList<Visita> Visitas { get; set; }
        [Column("Fecha_Creacion")]
        public DateTime? FechaCreacion { get; set; }
        [Column("Empleado_Creador_FK")]
        public int EmpleadoId { get; set; }
        public virtual Empleado Empleado { get; set; }
        [Column("Estado_FK")]
        public int EstadoId { get; set; }
        public virtual EstadoPlan Estado { get; set; }
        //public string UrlReporte { get; set; }
        public bool? Activo { get; set; }
    }
}