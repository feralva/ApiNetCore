using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Tipo_Equipo_Medicion")]
    public class TipoEquipoMedicion:BaseEntity
    {
        public string Descripcion { get; set; }
    }
}