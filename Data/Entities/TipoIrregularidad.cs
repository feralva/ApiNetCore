using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Tipo_Irregularidad")]
    public class TipoIrregularidad : BaseEntity
    {
        public string Descripcion { get; set; }
    }
}