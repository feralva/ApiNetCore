using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    [Table("Irregularidad")]
    public class Irregularidad:BaseEntity
    {
        [Column("Ubicacion_FK")]
        public int UbicacionId { get; set; }
        public virtual Ubicacion Ubicacion { get; set; }
        [Column("Estado_FK")]
        public int EstadoId { get; set; }
        public virtual EstadoIrregularidad Estado { get; set; }
        [Column("Tipo_FK")]
        public int TipoId { get; set; }
        public virtual TipoIrregularidad Tipo { get; set; }        
        [Column("Empleado_FK")]
        public int EmpleadoId { get; set; }
        public virtual Empleado Empleado { get; set; }        
        [Column("Cliente_FK")]
        public int ClienteId { get; set; }
        public virtual Cliente Cliente { get; set; }        
        [Column("Empresa_FK")]
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionResolucion { get; set; }
        public DateTime FechaDeteccion { get; set; }
        public DateTime? FechaFinalizado { get; set; }
        public string Url { get; set; }
        public string UrlEvidenciaResultado { get; set; }

    }
}
