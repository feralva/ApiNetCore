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
using Model.CaminoOptimo;
using Model.RequestHttp;
using Newtonsoft.Json.Linq;
using Services;
using Services.Contracts;

namespace ApiHigieneYSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstablecimientoController : ControllerBase
    {
        private ILogger<EmpresaController> _logger;
        private IGenericService<Establecimiento> service;
        private IGenericService<Direccion> serviceDireccion;
        private IGenericService<Cliente> serviceCliente;
        private IGenericService<Ubicacion> serviceUbicacion;

        protected IMapper mapper;

        public EstablecimientoController(ILogger<EmpresaController> logger, IGenericService<Establecimiento> Service,
            IMapper Mapper, IGenericService<Ubicacion> ServiceUbicacion, IGenericService<Direccion> ServiceDireccion,
            IGenericService<Cliente> ServiceCliente)
        {
            _logger = logger;
            this.service = Service;
            this.mapper = Mapper;
            this.serviceUbicacion = ServiceUbicacion;
            this.serviceDireccion = ServiceDireccion;
            this.serviceCliente = ServiceCliente;
        }

        /// <summary>
        /// Alta Establecimiento
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpPost]
        public ActionResult AltaEstablecimiento([FromBody] RequestAlta<EstablecimientoDTO> request)
        {
            try
            {
                var model = mapper.Map<Establecimiento>(request.Model);
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
        /// Obtener Establecimiento
        /// </summary>
        //[Authorize]
        [Route("{idEstablecimiento:int}")]
        [HttpGet]
        public ActionResult ObtenerEstablecimiento(int idEstablecimiento)
        {
            try
            {
                var establecimiento = this.service.ObtenerListado(new Establecimiento() {Id = idEstablecimiento })
                    .FirstOrDefault();

                establecimiento.Direccion = this.serviceDireccion.ObtenerPorId(establecimiento.DireccionId);
                var ubicaciones = this.serviceUbicacion.ObtenerListado(
                    new Ubicacion() { EstablecimientoId = idEstablecimiento }).ToList();

                var establecimientoDTO = this.mapper.Map<EstablecimientoDTO>(establecimiento);
                establecimientoDTO.Ubicaciones = this.mapper.Map<List<UbicacionDTO>>(ubicaciones);

                return Ok(establecimientoDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Ubicaciones
        /// </summary>
        //[Authorize]
        [Route("{idEstablecimiento:int}/Ubicaciones")]
        [HttpGet]
        public ActionResult ObtenerUbicacionesEstablecimiento(int idEstablecimiento)
        {
            try
            {
                var ubicaciones = this.serviceUbicacion.ObtenerListado(new Ubicacion() { EstablecimientoId = idEstablecimiento });

                return Ok(ubicaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }        
        
        /// <summary>
        /// Obtener Cliente en Base a Establecimiento
        /// </summary>
        //[Authorize]
        [Route("{idEstablecimiento:int}/Cliente")]
        [HttpGet]
        public ActionResult ObtenerClienteEnBaseaEstablecimiento(int idEstablecimiento)
        {
            try
            {
                var establecimiento = this.service.ObtenerListado(new Establecimiento() { Id = idEstablecimiento }).FirstOrDefault();
                var cliente = this.serviceCliente.ObtenerListado(new Cliente() { Id = establecimiento.ClienteId }).FirstOrDefault();

                return Ok(this.mapper.Map<ClienteDTO>(cliente));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }


        /// <summary>
        /// Actualizar Establecimiento
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpPut]
        public ActionResult ActualizarEstablecimiento([FromBody] RequestModify<EstablecimientoDTO> request)
        {
            try
            {
                var entidad = mapper.Map<Establecimiento>(request.Model);
                this.service.Modificar(entidad, "Ubicaciones");

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

        /// <summary>
        /// Obtener Mejor Ruta Establecimiento
        /// </summary>
        //[Authorize]
        [Route("{idEstablecimiento:int}/CaminoOptimo")]
        [HttpGet]
        public async Task<IActionResult> ObtenerCaminoOptimoEstablecimiento(int idEstablecimiento)
        {
            try
            {
                List<Tramo> camino = await ((EstablecimientoService)this.service).ObtenerCaminoOptimo(idEstablecimiento);

                return Ok(camino);
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