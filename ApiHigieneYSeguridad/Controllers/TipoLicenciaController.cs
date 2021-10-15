using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Model.RequestHttp;
using Model.TipoLicencia;
using Newtonsoft.Json.Linq;
using Services.Contracts;

namespace ApiHigieneYSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoLicenciaController : ControllerBase
    {
        private ILogger<TipoLicenciaController> _logger;
        private IGenericService<TipoLicencia> service;
        private IGenericService<PrecioLicencia> servicePrecioLicencia;
        protected IMapper mapper;

        public TipoLicenciaController(ILogger<TipoLicenciaController> logger, IGenericService<TipoLicencia> Service, IMapper Mapper,
            IGenericService<PrecioLicencia> ServicePrecioLicencia)
        {
            _logger = logger;
            this.service = Service;
            this.mapper = Mapper;
            this.servicePrecioLicencia = ServicePrecioLicencia;
        }

        /// <summary>
        /// Alta Tipo Licencia
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpPost]
        public ActionResult AltaLicencia([FromBody] RequestAlta<TipoLicenciaAltaDTO> request)
        {
            try
            {
                var model = mapper.Map<TipoLicencia>(request.Model);
                model.PreciosHistoricos = new List<PrecioLicencia>();
                
                model.PreciosHistoricos.Add(new PrecioLicencia()
                {
                    Precio = request.Model.PrecioActual,
                    FechaDesde = DateTime.Now
                });

                this.service.Alta(model);

                return base.Ok(GenerarRespuestaExito());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Actualizar Precio Tipo Licencia
        /// </summary>
        //[Authorize]
        [Route("Precio")]
        [HttpPost]
        public ActionResult ActualizarPrecioTipoLicencia([FromBody] RequestModify<TipoLicenciaAltaDTO> request)
        {
            try
            {
                var tipoLicencia = request.Model;

                servicePrecioLicencia.Alta(new PrecioLicencia()
                {
                    TipoLicenciaId = tipoLicencia.Id,
                    Precio = tipoLicencia.PrecioActual,
                    FechaDesde = DateTime.Now
                });

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

        /// <summary>
        /// Busqueda Tipo Licencia
        /// </summary>
        //[Authorize]
        [Route("{idTipoLicencia:int}")]
        [HttpGet]
        public ActionResult ObtenerTipoLicenciaPorId(int idTipoLicencia)
        {
            try
            {
                var tipoLicencia = this.mapper.Map<TipoLicenciaDTO>(this.service.ObtenerListado(new TipoLicencia() { Id = idTipoLicencia }).FirstOrDefault());
                return Ok(tipoLicencia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Modificar Tipo Licencia
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpPut]
        public ActionResult ModificarTipoLicencia([FromBody] RequestModify<TipoLicenciaDTO> request)
        {
            try
            {
                var entidad = mapper.Map<TipoLicencia>(request.Model);
                this.service.Modificar(entidad);

                return Ok(JObject.FromObject(new
                {
                    resultado = "Exitoso"
                }).ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        private static string GenerarRespuestaExito()
        {
            return JObject.FromObject(new
            {
                response = "Success"
            }).ToString();
        }
    }
}