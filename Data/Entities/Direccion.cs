using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Direccion:BaseEntity
    {
        public string Calle { get; set; }
        public int Altura { get; set; }
        [Column("Partido_FK")]
        public int PartidoId { get; set; }
        public Partido Partido { get; set; }
    }
}