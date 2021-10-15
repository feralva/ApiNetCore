using Model.Seguridad;
using System.Collections.Generic;

namespace Model.Seguridad
{
    public class UsuarioDTO:IDTO
    {
        public string IdUsuario { get; set; }
        public string Contraseña { get; set; }
        public virtual ICollection<UsuarioRolDTO> UsuarioRoles { get; set; }
    }
}