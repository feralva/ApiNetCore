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
using Model.Visita;
using Services;
using Services.Contracts;
using Newtonsoft.Json.Linq;
using Exceptions;

namespace ApiHigieneYSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitaController : ControllerBase
    {
        private IGenericService<Visita> service;
        private IGenericService<TipoVisita> serviceTipoVisita;
        private IGenericService<EstadoVisita> serviceEstadoVisita;
        private ILogger<VisitaController> _logger;
        //private IReportingService reportingService;

        protected IMapper mapper;

        public VisitaController(ILogger<VisitaController> logger, IGenericService<Visita> Service, IGenericService<TipoVisita> ServiceTipoVisita,
            IMapper Mapper, IGenericService<EstadoVisita> ServiceEstadoVisita)
        {
            _logger = logger;
            this.service = Service;
            this.mapper = Mapper;
            this.serviceTipoVisita = ServiceTipoVisita;
            this.serviceEstadoVisita = ServiceEstadoVisita;
        }

        /// <summary>
        /// Alta de Visita
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpPost]
        public ActionResult AltaVisita([FromBody] RequestAlta<VisitaDTO> request)
        {
            try
            {
                var entidad = mapper.Map<Visita>(request.Model);
                var id = this.service.Alta(entidad);

                return Ok(JObject.FromObject(new
                {
                    response = id
                }).ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }        
        /// <summary>
        /// Obtener Visitas Empleado
        /// </summary>
        //[Authorize]
        [Route("Empleado/{idEmpleado:int}")]
        [HttpGet]
        public ActionResult ObtenerVisitasEmpleado(int idEmpleado, bool activo = true, int estado = 1)
        {
            try
            {
                List<VisitaSummaryDTO> visitas = new List<VisitaSummaryDTO>();
                List<int> estadosAFiltrar = new List<int>();
                if (estado == 1)
                {
                    estadosAFiltrar.Add(1); 
                    estadosAFiltrar.Add(2);
                }
                else
                {
                    estadosAFiltrar.Add(3);
                }

                foreach (int estadoFiltro in estadosAFiltrar)
                {
                    visitas.AddRange(((VisitaService)service).ObtenerVisitasEmpleado(new Visita() { EmpleadoId = idEmpleado, Activo = activo, EstadoId = estadoFiltro }));
                }

                return Ok(visitas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Tipos Visita
        /// </summary>
        //[Authorize]
        [Route("Tipos")]
        [HttpGet]
        public ActionResult ObtenerTiposVisita()
        {
            try
            {
                return Ok(this.serviceTipoVisita.ObtenerListado());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }        
        
        /// <summary>
        /// Obtener Estados Posibles Visita
        /// </summary>
        //[Authorize]
        [Route("Estados")]
        [HttpGet]
        public ActionResult ObtenerEstadosVisita()
        {
            try
            {
                return Ok(this.serviceEstadoVisita.ObtenerListado());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }


        /// <summary>
        /// Obtener Visita por Id
        /// </summary>
        //[Authorize]
        [Route("{idVisita:int}")]
        // get: api/Visita
        [HttpGet]
        public ActionResult ObtenerVisitaPorId(int idVisita)
        {
            try
            {
                var visita = this.service.ObtenerListado(new Visita() { Id = idVisita }, "Estado,TipoVisita,Establecimiento,Empleado").FirstOrDefault();

                if (visita.Empleado != null) visita.Empleado.Visitas = null;
                return Ok(visita);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Actualizar Auditor Visita
        /// </summary>
        //[Authorize]
        [Route("reasignar")]
        [HttpPut]
        public ActionResult ReasignarVisita([FromBody] RequestModify<VisitaDTO> request)
        {
            try
            {
                var entidad = mapper.Map<Visita>(request.Model);
                ((VisitaService)this.service).ReasignarVisita(entidad);

                return Ok(JObject.FromObject(new
                {
                    resultado = "Exitoso"
                }).ToString());
            }
            catch (EmpleadoNoDisponibleException ex)
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
        /// Actualizar Fecha Visita
        /// </summary>
        //[Authorize]
        [Route("cambiarFecha")]
        [HttpPut]
        public ActionResult CambiarFechaVisita([FromBody] RequestModify<VisitaDTO> request)
        {
            try
            {
                var entidad = mapper.Map<Visita>(request.Model);
                ((VisitaService)this.service).CambiarFechaVisita(entidad);

                return Ok(JObject.FromObject(new
                {
                    resultado = "Exitoso"
                }).ToString());
            }
            catch (EmpleadoNoDisponibleException ex)
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
        /// Cambiar estado Visita a Completo
        /// </summary>
        //[Authorize]
        [Route("completarVisita")]
        [HttpPut]
        public ActionResult CompletarVisita([FromBody] RequestModify<VisitaCambiarEstadoDTO> request)
        {
            try
            {
                var entidad = mapper.Map<Visita>(request.Model);
                ((VisitaService)this.service).CompletarVisita(entidad);

                return Ok(JObject.FromObject(new
                {
                    resultado = "Exitoso"
                }).ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, JObject.FromObject(new
                {
                    resultado = ex.Message
                }).ToString());
            }
        }        
        
        /// <summary>
        /// Cambiar estado Visita a Cancelado
        /// </summary>
        //[Authorize]
        [Route("cancelarVisita")]
        [HttpPut]
        public ActionResult CancelarVisita([FromBody] RequestModify<VisitaCambiarEstadoDTO> request)
        {
            try
            {
                var entidad = mapper.Map<Visita>(request.Model);
                ((VisitaService)this.service).CancelarVisita(entidad);

                return Ok(JObject.FromObject(new
                {
                    resultado = "Exitoso"
                }).ToString());
            }
            catch(VisitaCompletaNoPuedeCancelarseException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(400, JObject.FromObject(new
                {
                    resultado = ex.Message
                }).ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }        
        
        ///// <summary>
        ///// Generar Reporte Visita
        ///// </summary>
        ////[Authorize]
        //[Route("GenerarReporteVisita")]
        //[HttpGet]
        //public ActionResult CancelarVisita(int idVisita)
        //{
        //    try
        //    {
        //        this.reportingService.GenerarReporteVisita(idVisita, "");
        //        return Ok(JObject.FromObject(new
        //        {
        //            resultado = "Exitoso"
        //        }).ToString());
        //    }
        //    catch(VisitaCompletaNoPuedeCancelarseException ex)
        //    {
        //        _logger.LogError(ex, ex.Message);
        //        return StatusCode(400, JObject.FromObject(new
        //        {
        //            resultado = ex.Message
        //        }).ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);
        //        return StatusCode(404, ex.Message);
        //    }
        //}

    }
}