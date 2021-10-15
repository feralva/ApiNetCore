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
    public class IrregularidadController : ControllerBase
    {
        private ILogger<IrregularidadController> _logger;
        private IGenericService<Irregularidad> service;
        private IGenericService<TipoIrregularidad> serviceTipoIrregularidad;

        protected IMapper mapper;

        public IrregularidadController(ILogger<IrregularidadController> logger, IGenericService<Irregularidad> Service,
            IMapper Mapper, IGenericService<TipoIrregularidad> ServiceTipoIrregularidad)
        {
            _logger = logger;
            this.service = Service;
            this.mapper = Mapper;
            this.serviceTipoIrregularidad = ServiceTipoIrregularidad;
        }

        /// <summary>
        /// Obtener Tipos Irregularidad
        /// </summary>
        //[Authorize]
        [Route("tipos")]
        [HttpGet]
        public ActionResult ObtenerTiposIrregularidades()
        {
            try
            {
                return Ok(this.serviceTipoIrregularidad.ObtenerListado());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }        
        
        /// <summary>
        /// Obtener Irregularidades
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpGet]
        public ActionResult ObtenerIrregularidades(int idEmpresa = 0, int estado = 1, int idCliente = 0, int idIrregularidad = 0)
        {
            try
            {
                var irregularidades = ((IrregularidadService)this.service).ObtenerIrregularidades(
                    new Irregularidad() { Id = idIrregularidad, EstadoId = estado, EmpresaId = idEmpresa, ClienteId = idCliente }, "Estado,Tipo,Ubicacion,Cliente,Empresa,Empleado");

                return Ok(irregularidades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }        
        
        /// <summary>
        /// Obtener Irregularidad
        /// </summary>
        //[Authorize]
        [Route("{idIrregularidad:int}")]
        [HttpGet]
        public ActionResult ObtenerIrregularidad(int idIrregularidad)
        {
            try
            {
                var irregularidad = this.service.ObtenerListado(
                    new Irregularidad() { Id = idIrregularidad}, "").FirstOrDefault();

                return Ok(irregularidad);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Alta Irregularidad
        /// </summary>
        //[Authorize]
        [Route("")]
        // post: api/Irregularidad
        [HttpPost]
        public ActionResult AltaIrregularidad([FromBody] RequestAlta<IrregularidadDTO> request)
        {
            try
            {
                var model = mapper.Map<Irregularidad>(request.Model);
                model.FechaDeteccion = DateTime.Now;
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
        /// Completado Irregularidad
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpPut]
        public ActionResult CompletarIrregularidad([FromBody] RequestModify<IrregularidadDTO> request)
        {
            try
            {
                var entidad = mapper.Map<Irregularidad>(request.Model);
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