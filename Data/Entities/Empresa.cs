using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Empresa")]
    public class Empresa: BaseEntity
    {
        public string Nombre { get; set; }
        [Column("Responsable_Empresa_FK")]
        public int ResponsableEmpresaId { get; set; }
        public virtual ResponsableEmpresa Responsable { get; set; }
        [Column("Direccion_FK")]
        public int DireccionId { get; set; }
        public virtual Direccion Direccion { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
        public virtual ICollection<Empleado> Empleados { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<EquipoMedicion> EquiposMedicion { get; set; }
        public string UrlFoto { get; set; }
        public bool? Activo { get; set; }

        public Empresa()
        {
            Activo = true;
        }
    }
}