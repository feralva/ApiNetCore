using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Model.RequestHttp;
using Newtonsoft.Json.Linq;
using Services;
using Services.Contracts;

namespace ApiHigieneYSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenciaController : ControllerBase
    {
        private ILogger<LicenciaController> _logger;
        private IGenericService<Licencia> service;
        private IGenericService<TipoLicencia> serviceTipoLicencia;
        protected IMapper mapper;

        public LicenciaController(ILogger<LicenciaController> logger, IGenericService<Licencia> Service,
            IMapper Mapper, IGenericService<TipoLicencia> ServiceTipoLicencia)
        {
            _logger = logger;
            service = Service;
            mapper = Mapper;
            serviceTipoLicencia = ServiceTipoLicencia;
        }

        /// <summary>
        /// Obtener Licencias
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpGet]
        public ActionResult ObtenerLicencias(int idEmpresa = 0)
        {
            try
            {
                var licencias = this.service.ObtenerListado(new Licencia() { EmpresaId = idEmpresa}, 
                    propiedadesAIncluir: "Empresa,TipoLicencia,Estado");

                return Ok(licencias);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Tipos Licencia
        /// </summary>
        //[Authorize]
        [Route("Tipos")]
        [HttpGet]
        public ActionResult ObtenerTiposLicencia(bool activo = true)
        {
            try
            {
                var tiposLicenciaConPrecio = this.mapper.Map<List<TipoLicenciaDTO>>(this.serviceTipoLicencia.ObtenerListado(new TipoLicencia() { Activo = activo } , 
                    propiedadesAIncluir: "PreciosHistoricos"));
                //Excluyo la licencia de prueba
                return Ok(tiposLicenciaConPrecio.Where(lic => lic.Id != 5));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Alta de Licencia
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpPost]
        public ActionResult Alta([FromBody] RequestAlta<LicenciaDTO> request)
        {
            try
            {
                var entidad = mapper.Map<Licencia>(request.Model);
                var cantidadMeses = request.Model.CantidadMeses;

                ((LicenciaService)service).ProcesarAdquisicionLicencia(entidad, cantidadMeses);

                return Ok(JObject.FromObject(new
                {
                    resultado = "Success"
                }).ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        private static string GenerarRespuestaCreacionExitosa(int id)
        {
            return JObject.FromObject(new
            {
                id = id
            }).ToString();
        }
    }
}
