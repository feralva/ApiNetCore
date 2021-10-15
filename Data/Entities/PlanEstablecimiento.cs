using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    [Table("Plan_Establecimiento")]
    public class PlanEstablecimiento
    {
        [Column("Establecimiento_FK")]
        public int EstablecimientoId { get; set; }
        public virtual Establecimiento Establecimiento { get; set; }

        [Column("Plan_FK")]
        public int PlanId { get; set; }
        public virtual Plan Plan { get; set; }
    }
}
