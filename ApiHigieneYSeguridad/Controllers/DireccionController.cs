using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using Services.Contracts;

namespace ApiHigieneYSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController : ControllerBase
    {
        private ILogger<DireccionController> _logger;
        private IGenericService<Direccion> service;
        protected IMapper mapper;

        public DireccionController(IMapper Mapper, IGenericService<Direccion> Service
            , ILogger<DireccionController> logger)
        {
            mapper = Mapper;
            service = Service;
            _logger = logger;
        }

        /// <summary>
        /// Obtener Partidos disponibles para Provincia
        /// </summary>
        //[Authorize]
        [Route("provincia/{idProvincia:int}/Partidos")]
        [HttpGet]
        public ActionResult ObtenerPartidosDeProvincia(int idProvincia)
        {
            try
            {
                return Ok(((DireccionService)service).ObtenerPartidosProvincia(idProvincia));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Provincias Disponibles
        /// </summary>
        //[Authorize]
        [Route("provincias")]
        [HttpGet]
        public ActionResult ObtenerProvinciasDisponibles()
        {
            try
            {
                return Ok(((DireccionService)service).ObtenerProvincias());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

    }
}