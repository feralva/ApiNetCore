﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Visita
{
    public class VisitaCambiarEstadoDTO: IDTO
    {
        public int Id { get; set; }
        public int EstadoId { get; set; }
    }
}
