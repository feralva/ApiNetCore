using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Exceptions;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/Empleado")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {

        private ILogger<EmpresaController> _logger;
        private IGenericService<Empleado> service;
        protected IMapper mapper;

        public EmpleadoController(ILogger<EmpresaController> logger, IGenericService<Empleado> Service, IMapper Mapper)
        {
            _logger = logger;
            this.service = Service;
            this.mapper = Mapper;
        }

        /// <summary>
        /// Alta Empleado
        /// </summary>
        [Authorize]
        [Route("")]
        [HttpPost]
        public ActionResult AltaEmpleado([FromBody] RequestAlta<EmpleadoDTO> request)
        {
            try
            {
                var model = mapper.Map<Empleado>(request.Model);
                this.service.Alta(model);

                return base.Ok(GenerarRespuestaExito());
            }
            catch (NumeroMaximoUsuariosLicenciaAlcanzadosException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(409, ex.Message);
            }catch (Exception ex)
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

        /// <summary>
        /// Busqueda Empleado
        /// </summary>
        //[Authorize]
        [Route("{idEmpleado:int}")]
        [HttpGet]
        public ActionResult ObtenerEmpleadoPorId(int idEmpleado)
        {
            try
            {
                //var empleado = this.service.ObtenerPorId(idEmpleado);

                var empleado = ((EmpleadoService) this.service).ObtenerDetalleEmpleado(idEmpleado);
                return Ok(empleado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Visitas Pendientes Asignadas a Empleado
        /// </summary>
        //[Authorize]
        [Route("{idEmpleado:int}/visitas")]
        [HttpGet]
        public ActionResult ObtenerVisitasPorEmpleado(int idEmpleado)
        {
            try
            {
                var visitas = ((EmpleadoService)this.service).ObtenerVisitasEmpleado(idEmpleado);

                return Ok(visitas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Actualizar Empleado
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpPut]
        public ActionResult ActualizarEmpleado([FromBody] RequestModify<EmpleadoDTO> request)
        {
            try
            {
                var entidad = mapper.Map<Empleado>(request.Model);
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


    }
}