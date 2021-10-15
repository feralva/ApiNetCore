using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities.Seguridad;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model;
using Model.RequestHttp;
using Model.Seguridad;
using Newtonsoft.Json.Linq;
using Services.Contracts;

namespace ApiHigieneYSeguridad.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _IAuthenticateService;
        private readonly ITokenService tokenService;
        protected IMapper mapper;
        private ILogger<AuthenticateController> _logger;

        public AuthenticateController(IAuthenticateService authenticate, IMapper Mapper, ILogger<AuthenticateController> logger,
            ITokenService TokenService)
        {
            _IAuthenticateService = authenticate;
            this.mapper = Mapper;
            _logger = logger;
            tokenService = TokenService;
        }

        /// <summary>
        /// Get JWT token
        /// </summary>
        /// <remarks>
        /// Requiere usuario y password de red.
        ///
        /// </remarks>
        [HttpPost]
        [Route("[action]")]
        public ActionResult Login([FromBody] LoginModel model)
        {

            try
            {
                Random random = new Random();
                var idSolicitud = random.Next(0, 999999999) * 10000000;

                _IAuthenticateService.IsAuthenticated(model.Username, model.Password);

                var authClaims = new[]
                {
                        new Claim(ClaimTypes.Name, model.Username),
                        new Claim(JwtRegisteredClaimNames.Sub, model.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("idSolicitud", idSolicitud.ToString())
                    };

                var accessToken = tokenService.GenerateAccessToken(authClaims);
                var refreshToken = tokenService.GenerateRefreshToken();
                var RefreshTokenExpirationTime = DateTime.Now.AddDays(7);

                tokenService.AlmacenarRfreshToken(model.Username, refreshToken, RefreshTokenExpirationTime);

                return Ok(new
                {
                    token = accessToken,
                    refreshToken = refreshToken
                });
            }
            catch(UsuarioOContraseniaIncorrectaException ex)
            {
                this._logger.LogError(ex.ToString());
                return StatusCode(409, ex.Message);
            }            
            catch(EmpresaDeshabilitadaException ex)
            {
                this._logger.LogError(ex.ToString());
                return StatusCode(409, ex.Message);
            }            
            catch(LicenciaVencidaException ex)
            {
                this._logger.LogError(ex.ToString());
                return StatusCode(409, ex.Message);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                return StatusCode(400, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Roles Disponibles
        /// </summary>
        //[Authorize]
        [Route("roles")]
        [HttpGet]
        public ActionResult ObtenerRolesPosibles()
        {
            try
            {
                var familias = this._IAuthenticateService.ObtenerRolesDisponibles();

                return Ok(mapper.Map<IEnumerable<Familia>, IEnumerable<FamiliaDTO>>(familias));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Roles De Usuario
        /// </summary>
        //[Authorize]
        [Route("user/{idUsuario}/roles")]
        [HttpGet]
        public ActionResult ObtenerRolesUsuario(string idUsuario)
        {
            try
            {
                var familias = this._IAuthenticateService.ObtenerRolesUsuario(idUsuario);

                return Ok(mapper.Map<IEnumerable<Familia>, IEnumerable<FamiliaDTO>>(familias));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }


        /// <summary>
        /// Envio Correo con el token para que el usuario reestablezca su contraseña
        /// </summary>
        [Route("user/{idUsuario}/forgotPassword")]
        [HttpPost]
        public ActionResult ForgotPassword(string idUsuario)
        {
            try
            {
                this._IAuthenticateService.ForgotPassword(idUsuario);

                return Ok(JObject.FromObject(new
                {
                    mensaje = "Favor de Revisar su Correo Electronico para continuar"
                }).ToString());
            }
            catch (UsuarioNoExisteException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(409, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }        
        
        /// <summary>
        /// Envio Correo con el token para que el usuario reestablezca su contraseña
        /// </summary>
        [Route("User/ReestablecerPass")]
        [HttpPost]
        public ActionResult ReestablecerPassUsuario([FromBody] RequestReestablecerPassUser request)
        {
            try
            {
                this._IAuthenticateService.ReestablecerPass(request.emailUsuario, request.nuevaPass, request.tokenRefresh);

                return Ok(JObject.FromObject(new
                {
                    mensaje = "Contraseña Reestablecida"
                }).ToString());
            }
            catch (UsuarioNoExisteException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(409, ex.Message);
            }
            catch (TokenPassResetExpiradoException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(401, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Información Usuario
        /// </summary>
        //[Authorize]
        [Route("user/{idUsuario}")]
        [HttpGet]
        public ActionResult ObtenerInformacionUsuario(string idUsuario)
        {
            try
            {
                var detalleUsuario = this._IAuthenticateService.ObtenerInformacionUsuario(idUsuario);

                return Ok(detalleUsuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Patentes de Usuario
        /// </summary>
        //[Authorize]
        [Route("user/{idUsuario}/patentes")]
        [HttpGet]
        public ActionResult ObtenerPatentesUsuario(string idUsuario)
        {
            try
            {
                var patentes = this._IAuthenticateService.ObtenerPatentesUsuario(idUsuario);

                return Ok(patentes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        [HttpPost]
        [Route("refreshToken")]
        public IActionResult Refresh(RequestRefreshToken tokenApiModel)
        {
            try
            {
                if (tokenApiModel is null)
                {
                    return BadRequest("Invalid client request");
                }

                string accessToken = tokenApiModel.AccessToken;
                string refreshToken = tokenApiModel.RefreshToken;

                var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
                var username = principal.Identity.Name; //this is mapped to the Name claim by default

                TokenDTO userToken = this._IAuthenticateService.ObtenerTokenUsuario(username);

                if (userToken == null || userToken.RefreshToken != refreshToken || userToken.RefreshTokenExpirationTime <= DateTime.Now)
                {
                    return StatusCode(401, "Refresh Token Expired");
                }

                var newAccessToken = tokenService.GenerateAccessToken(principal.Claims);

                return Ok(new
                {
                    accessToken = newAccessToken
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

    }
}
