using Data.Entities.EntidadesNoPersistidas;
using Data.Entities.Seguridad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Contracts
{
    public interface ISeguridadRepository
    {
        IEnumerable<Familia> ObtenerRolesPosibles();
        IEnumerable<Familia> ObtenerRolesUsuario(string idUsuario);
        IEnumerable<Patente> ObtenerPatentesUsuario(string idUsuario);
        void ActualizarRolesUsuario(string idUsuario, ICollection<UsuarioFamilia> usuarioFamilia);
        void ComprobarUsuario(string username, string passEncriptada);
        UsuarioDetalle ObtenerInformacionUsuario(string idUsuario);
        void AlmacenarRfreshToken(string idUsuario, string refreshToken, DateTime refreshTokenExpirationTime);
        Token ObtenerRefreshTokenUsuario(string username);
        void GuardarNuevaContraseniaUser(string idUsuario, string nuevaContrasenia);
        void ComprobarSiUsuarioExiste(string username);
    }
}
