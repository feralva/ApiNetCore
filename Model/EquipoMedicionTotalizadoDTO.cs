using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class EquipoMedicionTotalizadoDTO: IDTO
    {
        public int Id { get; set; }
        public string EquipoMedicionNombre { get; set; }
        public int Cantidad { get; set; }

    }
}
