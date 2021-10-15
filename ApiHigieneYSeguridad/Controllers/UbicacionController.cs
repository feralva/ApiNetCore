using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Contracts;

namespace ApiHigieneYSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UbicacionController : ControllerBase
    {
        private IGenericService<Irregularidad> serviceIrregularidad;
        private IGenericService<Ubicacion> service;
        private ILogger<UbicacionController> _logger;

        protected IMapper mapper;

        public UbicacionController(ILogger<UbicacionController> logger, IGenericService<Irregularidad> ServiceIrregularidad,
            IMapper Mapper, IGenericService<Ubicacion> Service)
        {
            _logger = logger;
            this.serviceIrregularidad = ServiceIrregularidad;
            this.mapper = Mapper;
            this.service = Service;
        }

        /// <summary>
        /// Obtener Irregularidades Ubicacion
        /// </summary>
        //[Authorize]
        [Route("{idUbicacion:int}/Irregularidades")]
        [HttpGet]
        public ActionResult ObtenerUbicacionesEstablecimiento(int idUbicacion)
        {
            try
            {
                var irregularidades = this.serviceIrregularidad
                    .ObtenerListado(new Irregularidad() { UbicacionId = idUbicacion });

                return Ok(irregularidades);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Ubicacion por Id
        /// </summary>
        //[Authorize]
        [Route("{idVisita:int}")]
        // get: api/Visita
        [HttpGet]
        public ActionResult ObtenerUbicacionPorId(int idVisita)
        {
            try
            {
                var ubicacion = this.service.ObtenerPorId(idVisita);
                return Ok(ubicacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Borrar Ubicacion por Id
        /// </summary>
        //[Authorize]
        [Route("{idVisita:int}")]
        // get: api/Visita
        [HttpDelete]
        public ActionResult BorrarUbicacionPorId(int idVisita)
        {
            try
            {
                this.service.Borrar(this.service.ObtenerPorId(idVisita));
                return Ok();
            }
            catch(BaseDeDatosExcepcion ex)
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
    }
}