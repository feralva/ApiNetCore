using AutoMapper;
using Data.Entities.Seguridad;
using Data.Repositories.Contracts;
using Encriptacion;
using Exceptions;
using Microsoft.Extensions.Logging;
using Model;
using Model.Seguridad;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class AuthentificateService : IAuthenticateService
    {
        protected ISeguridadRepository seguridadRepository;
        protected IHashingService hashingService;
        protected ILogger<AuthentificateService> logger;
        protected IMapper mapper;
        private readonly ITokenService tokenService;
        private readonly IEmailService emailService;

        public AuthentificateService(ISeguridadRepository SeguridadRepository, ILogger<AuthentificateService> Logger,
            IHashingService HashingService, IMapper Mapper, ITokenService TokenService, IEmailService EmailService)
        {
            this.hashingService = HashingService;
            this.seguridadRepository = SeguridadRepository;
            this.logger = Logger;
            this.mapper = Mapper;
            this.tokenService = TokenService;
            this.emailService = EmailService;
        }

        public byte[] Encrypt(string publicKeyXML, string dataToDycript)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKeyXML);

            return rsa.Encrypt(ASCIIEncoding.ASCII.GetBytes(dataToDycript), true);
        }

        public string Decrypt(string publicPrivateKeyXML, byte[] encryptedData)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicPrivateKeyXML);

            return ASCIIEncoding.ASCII.GetString(rsa.Decrypt(encryptedData, true));
        }

        public void IsAuthenticated(string username, string pwd)
        {
            try
            {
                this.seguridadRepository.ComprobarUsuario(username, pwd);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }      
           
        }

        public IEnumerable<Patente> ObtenerPatentesUsuario(string idUsuario)
        {
            return this.seguridadRepository.ObtenerPatentesUsuario(idUsuario);
        }

        public IEnumerable<Familia> ObtenerRolesDisponibles()
        {
            return this.seguridadRepository.ObtenerRolesPosibles();
        }

        public IEnumerable<Familia> ObtenerRolesUsuario(string idUsuario)
        {
            return this.seguridadRepository.ObtenerRolesUsuario(idUsuario);
        }

        public UsuarioDetalleDTO ObtenerInformacionUsuario(string idUsuario)
        {
            return this.mapper.Map<UsuarioDetalleDTO>(this.seguridadRepository.ObtenerInformacionUsuario(idUsuario));
        }

        public TokenDTO ObtenerTokenUsuario(string username)
        {
            return this.mapper.Map<TokenDTO>(this.seguridadRepository.ObtenerRefreshTokenUsuario(username));
        }

        /// <summary>
        /// Genero el token para que el usuario resetee su password y se lo envio por correo
        /// </summary>
        /// <param name="idUsuario">Email del usuario</param>
        public void ForgotPassword(string idUsuario)
        {
            try
            {
                this.seguridadRepository.ComprobarSiUsuarioExiste(idUsuario);

                var refreshToken = tokenService.GenerateRefreshToken();
                var RefreshTokenExpirationTime = DateTime.Now.AddHours(1);

                tokenService.AlmacenarRfreshToken(idUsuario, refreshToken, RefreshTokenExpirationTime);

                this.emailService.enviarEmailDeForgotPassword(idUsuario, refreshToken);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }

        }

        /// <summary>
        /// Se establece una nueva contraseña para el usuario enviado, si el token continua activo
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="nuevaContrasenia"></param>
        /// <param name="tokenRefresco"></param>
        public void ReestablecerPass(string idUsuario, string nuevaContrasenia, string tokenRefresco)
        {
            try
            {
                this.seguridadRepository.ComprobarSiUsuarioExiste(idUsuario);

                var tokenRefrescoAlmacenado = tokenService.ObtenerTokenRefrescoActual(idUsuario);

                if ((tokenRefrescoAlmacenado.RefreshTokenExpirationTime > DateTime.Now) 
                        && (tokenRefrescoAlmacenado.RefreshToken == tokenRefresco))
                {
                    this.GuardarNuevaContraseniaUser(idUsuario, nuevaContrasenia);
                }
                else
                {
                    throw new TokenPassResetExpiradoException();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }
        /// <summary>
        /// Tomo la contraseña del usuario, la encripto y se almacena.
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="nuevaContrasenia"></param>
        private void GuardarNuevaContraseniaUser(string idUsuario, string nuevaContrasenia)
        {
            this.seguridadRepository.GuardarNuevaContraseniaUser(idUsuario, this.hashingService.CreateSha256(nuevaContrasenia));
            this.emailService.enviarEmailCambioContrseniaexitoso(idUsuario);
        }
    }
}
