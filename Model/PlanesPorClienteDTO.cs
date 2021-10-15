using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class PlanesPorClienteDTO:IDTO
    {
        public PlanClienteDTO Plan { get; set; }
        public TotalizadosPlanesDTO Totalizados { get; set; }
    }
}
