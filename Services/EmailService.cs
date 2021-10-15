using Data.Entities;
using Data.Entities.Seguridad;
using Microsoft.Extensions.Configuration;
using Model.PlanModels;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void enviarEmailBienvenidaUsuario(Usuario usuario, string contrasenia)
        {
            string MailText = ObtenerHtmldeRuta(@"..\Services\EmailHtmlTemplates\Welcome.html");

            MailText = MailText.Replace("{USUARIO}", usuario.IdUsuario.Trim());
            MailText = MailText.Replace("{Contrasenia}", contrasenia);

            this.Send(usuario.IdUsuario, "Bienvenido a Safetify", MailText);
        }        
        public void enviarEmailBienvenidaEmpresa(string emailResponsable, string nombreEmpresa)
        {
            string MailText = ObtenerHtmldeRuta(@"..\Services\EmailHtmlTemplates\WelcomeEmpresa.html");

            MailText = MailText.Replace("{Empresa}", nombreEmpresa.Trim());

            this.Send(emailResponsable, "Bienvenido a Safetify", MailText);
        }

        private static string ObtenerHtmldeRuta(string path)
        {
            StreamReader str = new StreamReader(path);
            string MailText = str.ReadToEnd();
            str.Close();
            return MailText;
        }

        public void enviarEmailDeForgotPassword(string email, string resetToken)
        {
            var urlBase = _config.GetValue<string>(
                "RutasFE:RutaBase");
            
            var resetUrl = urlBase + $"login/{email}/forgotPassword/{System.Web.HttpUtility.UrlEncode(resetToken)}";

            string MailText = ObtenerHtmldeRuta(@"..\Services\EmailHtmlTemplates\forgotPassword.html");

            MailText = MailText.Replace("{URLPASSRESET}", resetUrl);
            MailText = MailText.Replace("{USUARIO}", email);

            this.Send(email, "Safetify - Password Reset", MailText);
        }

        public void Send(string to, string subject, string bodyHtml)
        {
            var fromAddress = new MailAddress(_config.GetValue<string>(
                "CuentaEmail:cuenta"), _config.GetValue<string>(
                "CuentaEmail:displayName"));

            string fromPassword = _config.GetValue<string>(
                                        "CuentaEmail:contrasenia");

            var toAddress = new MailAddress(to, "To Name");

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };

            using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = bodyHtml,
                        IsBodyHtml = true
                    }
            )
            {
                smtp.Send(message);
            }
        }

        public void enviarEmailCambioContrseniaexitoso(string email)
        {
            string MailText = ObtenerHtmldeRuta(@"..\Services\EmailHtmlTemplates\CambioContraseniaExitoso.html");

            this.Send(email, "Safetify - Cambio Contraseña Exitoso", MailText);
        }
    }
}
