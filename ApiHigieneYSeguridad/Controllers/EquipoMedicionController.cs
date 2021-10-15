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
    public class EquipoMedicionController : ControllerBase
    {
        private ILogger<EquipoMedicionController> _logger;
        private IGenericService<EquipoMedicion> service;
        private IGenericService<TipoEquipoMedicion> serviceTipoEquipoMedicion;
        protected IMapper mapper;

        public EquipoMedicionController(ILogger<EquipoMedicionController> logger, IGenericService<EquipoMedicion> Service, 
            IMapper Mapper, IGenericService<TipoEquipoMedicion> ServiceTipoEquipoMedicion)
        {
            _logger = logger;
            this.service = Service;
            this.serviceTipoEquipoMedicion = ServiceTipoEquipoMedicion;
            this.mapper = Mapper;
        }

        /// <summary>
        /// Alta Equipo Medicion
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpPost]
        public ActionResult AltaEquipoMedicion([FromBody] RequestAlta<EquipoMedicionDTO> request)
        {
            try
            {
                var model = mapper.Map<EquipoMedicion>(request.Model);
                this.service.Alta(model);

                return Ok(GenerarRespuestaExito());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Equipo Medicion Por Id
        /// </summary>
        //[Authorize]
        [Route("{idEquipo:int}")]
        [HttpGet]
        public ActionResult ObtenerEquipo(int idEquipo)
        {
            try
            {
                return Ok(this.service.ObtenerPorId(idEquipo));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        ///// <summary>
        ///// Obtener Tipo Equipo Medición
        ///// </summary>
        ////[Authorize]
        //[Route("tipo/{idTipoEquipoMedicion:int}")]
        //[HttpGet]
        //public ActionResult ObtenerTipoEquipo(int idTipoEquipoMedicion)
        //{
        //    try
        //    {
        //        return Ok(this.serviceTipoEquipoMedicion.ObtenerPorId(idTipoEquipoMedicion));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);
        //        return StatusCode(404, ex.Message);
        //    }
        //}        
        /// <summary>
        /// Obtener Tipo Equipo Medición
        /// </summary>
        //[Authorize]
        [Route("tipo/{nombre}")]
        [HttpGet]
        public ActionResult ObtenerTipoEquipo(string nombre, int empresaID)
        {
            try
            {
                 var equipo = ((EquipoMedicionService)this.service).ObtenerEquiposMedicionTotalizados(
                                    new EquipoMedicion() { Nombre = nombre, EmpresaId = empresaID }).FirstOrDefault();
                
                return Ok(equipo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Equipos
        /// </summary>
        //[Authorize]
        [Route("{tipoEquipoMedicion:int}")]
        [HttpGet]
        public ActionResult ObtenerEquiposMedicion(int tipoEquipoMedicion)
        {
            try
            {
                return Ok(this.service.ObtenerListado(new EquipoMedicion() { TipoEquipoMedicionId = tipoEquipoMedicion }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Tipos Equipos
        /// </summary>
        //[Authorize]
        [Route("tipos")]
        [HttpGet]
        public ActionResult ObtenerTiposEquipoMedicion()
        {
            try
            {
                return Ok(this.serviceTipoEquipoMedicion.ObtenerListado());
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