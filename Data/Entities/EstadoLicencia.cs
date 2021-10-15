using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Estado_Licencia")]
    public class EstadoLicencia:BaseEntity
    {
        public string Descripcion { get; set; }
    }
}