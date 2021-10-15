using System;
using System.Collections.Generic;
using System.Text;

namespace Model.PlanModels
{
    public class PlanCambiarEstadoDTO: IDTO
    {

        public int Id { get; set; }
        public int EstadoId { get; set; }
    }
}
