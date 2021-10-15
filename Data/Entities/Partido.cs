using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Partido: BaseEntity
    {
        public string Nombre { get; set; }
        [Column("Provincia_FK")]
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }

    }
}
