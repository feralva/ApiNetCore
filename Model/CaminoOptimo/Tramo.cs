using System;
using System.Collections.Generic;
using System.Text;

namespace Model.CaminoOptimo
{
    public class Tramo
    {
        public Location UbicacionOrigen { get; set; }
        public Location UbicacionDestino { get; set; }
        public int Distancia { get; set; }
    }
}
