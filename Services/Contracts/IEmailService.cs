using Data.Entities.Seguridad;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts
{
    public interface IEmailService
    {
        void enviarEmailBienvenidaEmpresa(string emailResponsable, string nombreEmpresa);
        void enviarEmailBienvenidaUsuario(Usuario usario, string contrasenia);
        void enviarEmailDeForgotPassword(string idUsuario, string refreshToken);
        void enviarEmailCambioContrseniaexitoso(string email);
    }
}
