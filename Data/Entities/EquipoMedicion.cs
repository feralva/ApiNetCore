using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    [Table("Equipo_Medicion")]
    public class EquipoMedicion:BaseEntity
    {
        public string Nombre { get; set; }
        [Column("Tipo_Equipo_Medicion_FK")]
        public int TipoEquipoMedicionId { get; set; }
        public virtual TipoEquipoMedicion Tipo { get; set; }
        [Column("Empresa_FK")]
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
        public bool? Activo { get; set; }
    }
}
