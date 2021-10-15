using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Evicencia")]
    public class Evidencia : BaseEntity
    {
        public string Url { get; set; }
    }
}