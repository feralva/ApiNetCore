using Data.Entities.Seguridad;
using Model;
using Model.Seguridad;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Services.Contracts
{
    public interface IAuthenticateService
    {
        void IsAuthenticated(string username, string pwd);
        IEnumerable<Familia> ObtenerRolesDisponibles();
        IEnumerable<Familia> ObtenerRolesUsuario(string idUsuario);
        IEnumerable<Patente> ObtenerPatentesUsuario(string idUsuario);
        UsuarioDetalleDTO ObtenerInformacionUsuario(string idUsuario);
        TokenDTO ObtenerTokenUsuario(string username);
        void ForgotPassword(string idUsuario);
        void ReestablecerPass(string idUsuario, string nuevaContrasenia, string tokenRefresco);
    }
}
