using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Responsable_Empresa")]
    public class ResponsableEmpresa:BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        [Column("Correo_Electronico")]
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
    }
}