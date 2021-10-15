using Data.Entities.Seguridad;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Empleado")]
    public class Empleado:BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [Column("Correo_Electronico")]
        public string CorreoElectronico { get; set; }
        [Column("Usuario_FK")]
        [ForeignKey("Usuario")]
        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        [Column("Empresa_FK")]
        public int EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
        public virtual ICollection<Visita> Visitas { get; set; }
        public string urlFoto { get; set; }
        public bool? Activo { get; set; }
    }
}