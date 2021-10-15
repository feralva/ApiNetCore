using System.Collections.Generic;

namespace Data.Entities
{
    public class Provincia: BaseEntity
    {
        public string Nombre { get; set; }
        public virtual IList<Partido> Partidos { get; set; }
    }
}