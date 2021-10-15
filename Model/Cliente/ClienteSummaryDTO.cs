using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Cliente
{
    public class ClienteSummaryDTO:IDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CantidadEstablecimientos{ get; set; }
        public int CantidadPlanes{ get; set; }
        public string UrlFoto { get; set; }
        //public int CantidadIrregularidades{ get; set; }
    }
}
