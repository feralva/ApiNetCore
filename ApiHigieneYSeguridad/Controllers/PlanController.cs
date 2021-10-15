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
using Model.PlanModels;
using Model.RequestHttp;
using Newtonsoft.Json.Linq;
using Services;
using Services.Contracts;

namespace ApiHigieneYSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {

        private ILogger<PlanController> _logger;
        private IGenericService<Plan> service;
        private IGenericService<TipoPlan> serviceTipoPlan;
        private IGenericService<EstadoPlan> serviceEstadoPlan;
        private IGenericService<Visita> serviceVisita;
        protected IMapper mapper;

        public PlanController(ILogger<PlanController> logger, IGenericService<Plan> Service, IMapper Mapper, 
            IGenericService<TipoPlan> ServiceTipoPlan, IGenericService<EstadoPlan> ServiceEstadoPlan)
        {
            _logger = logger;
            this.service = Service;
            this.mapper = Mapper;
            this.serviceTipoPlan = ServiceTipoPlan;
            this.serviceEstadoPlan = ServiceEstadoPlan;
        }

        /// <summary>
        /// Alta Plan Accion
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpPost]
        public ActionResult AltaPlan([FromBody] RequestAlta<PlanDTO> request)
        {
            try
            {
                var model = mapper.Map<Plan>(request.Model);

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
        /// Editar Plan Accion
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpPut]
        public ActionResult EditarPlan([FromBody] RequestModify<PlanCambiarEstadoDTO> request)
        {
            try
            {
                var model = mapper.Map<Plan>(request.Model);

                this.service.Modificar(model);

                return Ok(GenerarRespuestaExito());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Busqueda Plan Especifico
        /// </summary>
        //[Authorize]
        [Route("{idPlanAccion:int}")]
        [HttpGet]
        public ActionResult ObtenerPlanPorId(int idPlanAccion,bool activo = true)
        {
            try
            {
                var plan = ((PlanService) this.service).ObtenerPlanDetalle(new Plan() { Id = idPlanAccion/*, Activo = activo*/});

                return Ok(plan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }        
        
        /// <summary>
        /// Determinar si las visitas del plan estan cerradas
        /// </summary>
        //[Authorize]
        [Route("{idPlanAccion:int}/VisitasCerradas")]
        [HttpGet]
        public ActionResult DeterminarSiVisitasPlanEstanCerradas(int idPlanAccion)
        {
            try
            {
                bool visitasCerradas = ((PlanService) this.service).DeterminarSiVisitasPlanEstanCerradas(idPlanAccion);

                return Ok(JObject.FromObject(new
                {
                    resultado = visitasCerradas
                }).ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Tipos Planes
        /// </summary>
        //[Authorize]
        [Route("Tipos")]
        [HttpGet]
        public ActionResult ObtenerTiposPlanes()
        {
            try
            {
                return Ok(this.serviceTipoPlan.ObtenerListado());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Estados Plan
        /// </summary>
        //[Authorize]
        [Route("Estados")]
        [HttpGet]
        public ActionResult ObtenerEstadosPosibles()
        {
            try
            {
                return Ok(this.serviceEstadoPlan.ObtenerListado());
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