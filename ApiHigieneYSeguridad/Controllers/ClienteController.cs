using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Entities.EntidadesNoPersistidas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Model.Cliente;
using Model.RequestHttp;
using Newtonsoft.Json.Linq;
using Services;
using Services.Contracts;

namespace ApiHigieneYSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private ILogger<EmpresaController> _logger;
        private IGenericService<Cliente> service;
        private IGenericService<Plan> servicePlan;
        private IGenericService<Establecimiento> serviceEstablecimiento;
        private IGenericService<Partido> servicePartido;
        protected IMapper mapper;

        public ClienteController(ILogger<EmpresaController> logger, IGenericService<Cliente> Service, IGenericService<Partido> ServicePartido,
            IMapper Mapper, IGenericService<Plan> ServicePlan, IGenericService<Establecimiento> ServiceEstablecimiento)
        {
            _logger = logger;
            this.mapper = Mapper;
            this.servicePlan = ServicePlan;
            this.service = Service;
            this.servicePartido = ServicePartido;
            this.serviceEstablecimiento = ServiceEstablecimiento;
        }

        /// <summary>
        /// Alta de Cliente
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpPost]
        public ActionResult AltaCliente([FromBody] RequestAlta<ClienteDTO> request)
        {
            try
            {
                var entidad = mapper.Map<Cliente>(request.Model);
                var id = this.service.Alta(entidad);

                return Ok(JObject.FromObject(new
                {
                    id = id
                }).ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Modificar Cliente
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpPut]
        public ActionResult ModificarCliente([FromBody] RequestModify<ClienteDTO> request)
        {
            try
            {
                var entidad = mapper.Map<Cliente>(request.Model);
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

        /// <summary>
        /// Obtener Visitas Por estado de cliente
        /// </summary>
        //[Authorize]
        [Route("{idCliente:int}/VisitasPorEstado")]
        [HttpGet]
        public ActionResult ObtenerVisitasPorEstadoEmpresa(int idCliente)
        {
            try
            {
                var resultado = ((ClienteService)service).ObtenerTotalizadoVisitasPorEstado(idCliente);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Todos los Establecimientos Empresa
        /// </summary>
        //[Authorize]
        [Route("{idCliente:int}/Establecimientos")]
        [HttpGet]
        public ActionResult ObtenerEstablecimientos(int idCliente, bool? activo)
        {
            try
            {
                var establecimientos =((EstablecimientoService) this.serviceEstablecimiento).ObtenerEstablecimientosCliente(new Establecimiento() { ClienteId = idCliente, Activo = activo });

                return Ok(establecimientos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Todos los Planes accion Cliente
        /// </summary>
        //[Authorize]
        [Route("{idCliente:int}/Planes")]
        [HttpGet]
        public ActionResult ObtenerPlanes(int idCliente, bool? activo, int idEstado = 0)
        {
            try
            {
                List<PlanesPorCliente> planesTotalizados = ((PlanService)this.servicePlan).ObtenerPlanesClienteTotalizados(new Plan() { ClienteId = idCliente, 
                    Activo = activo, EstadoId = idEstado});
                
                var planes = mapper.Map<List<PlanesPorCliente>,List<PlanesPorClienteDTO>>(planesTotalizados);

                return Ok(planes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Todos los Clientes
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpGet]
        public ActionResult ObtenerClientes()
        {
            try
            {
                var clientes = this.service.ObtenerListado();

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Cliente Resumen
        /// </summary>
        //[Authorize]
        [Route("{idCliente:int}/resumen")]
        [HttpGet]
        public ActionResult ObtenerClienteResumen(int idCliente)
        {
            try
            {
                var cliente = this.service.ObtenerListado(new Cliente() { Id = idCliente}, "Establecimientos,Planes").FirstOrDefault();

                return Ok(this.mapper.Map<ClienteSummaryDTO>(cliente));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }        

        /// <summary>
        /// Obtener Cliente
        /// </summary>
        //[Authorize]
        [Route("{idCliente:int}")]
        [HttpGet]
        public ActionResult ObtenerCliente(int idCliente)
        {
            try
            {
                //var cliente = this.service.ObtenerPorId(idCliente);

                var cliente = this.service.ObtenerListado(new Cliente() { Id = idCliente }, "Direccion,Responsable").FirstOrDefault();

                cliente.Direccion.Partido = servicePartido
                    .ObtenerListado(new Partido() { Id = cliente.Direccion.PartidoId }, "Provincia").FirstOrDefault();

                cliente.Direccion.Partido.Provincia.Partidos = null;

                var clienteDTO = this.mapper.Map<ClienteDTO>(cliente);

                return Ok(clienteDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Todos los Planes de Establecimiento de Cliente
        /// </summary>
        //[Authorize]
        [Route("{idCliente:int}/Establecimientos/{idEstablecimiento:int}/Planes")]
        [HttpGet]
        public ActionResult ObtenerPlanesEstablecimientoCliente(int idCliente, int idEstablecimiento)
        {
            try
            {
                var clientes = this.servicePlan.ObtenerListado(new Plan() { ClienteId = idCliente, 
                    PlanesEstablecimientos = new List<PlanEstablecimiento>() { 
                        new PlanEstablecimiento() { EstablecimientoId = idEstablecimiento} } });

                return Ok(clientes);
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