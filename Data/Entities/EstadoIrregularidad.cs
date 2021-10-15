using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Estado_Irregularidad")]
    public class EstadoIrregularidad:BaseEntity
    {
        public string Descripcion { get; set; }
    }
}