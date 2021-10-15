using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class LicenciaDTO:IDTO
    {
        public int EmpresaId { get; set; }
        public virtual EmpresaDTO Empresa { get; set; }
        public int TipoLicenciaId { get; set; }
        public virtual TipoLicenciaDTO TipoLicencia { get; set; }
        public int CantidadMeses { get; set; }
        public DateTime? FechaFin { get; set; }
        public int EstadoId { get; set; }
        public virtual EstadoLicenciaDTO Estado { get; set; }
        public bool Activo { get; set; }
    }
}
