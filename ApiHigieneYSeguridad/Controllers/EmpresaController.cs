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
using Model.Cliente;
using Model.Empresa;
using Model.PlanModels;
using Model.RequestHttp;
using Newtonsoft.Json.Linq;
using Services;
using Services.Contracts;

namespace ApiHigieneYSeguridad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private ILogger<EmpresaController> _logger;
        private IGenericService<Empresa> service;
        private IGenericService<Empleado> serviceEmpleado;
        private IGenericService<Plan> servicePlan;
        private IGenericService<Cliente> serviceCliente;
        private IGenericService<EquipoMedicion> serviceEquipoMedicion;
        private IGenericService<Visita> serviceVisita;
        private IGenericService<Partido> servicePartido;
        private IGenericService<Pago> servicePago;
        private IGenericService<Licencia> serviceLicencia;

        protected IMapper mapper;

        public EmpresaController(ILogger<EmpresaController> logger, IGenericService<Empresa> empresaService,
            IMapper Mapper, IGenericService<Empleado> ServiceEmpleado, IGenericService<Plan> ServicePlan,
            IGenericService<Cliente> ServiceCliente, IGenericService<EquipoMedicion> ServiceEquipoMedicion,
            IGenericService<Visita> ServiceVisita, IGenericService<Partido> ServicePartido,
            IGenericService<Pago> ServicePago, IGenericService<Licencia> ServiceLicencia)
        {
            _logger = logger;
            this.service = empresaService;
            this.mapper = Mapper;
            this.serviceEmpleado = ServiceEmpleado;
            this.servicePlan = ServicePlan;
            this.serviceCliente = ServiceCliente;
            this.serviceEquipoMedicion = ServiceEquipoMedicion;
            this.serviceVisita = ServiceVisita;
            this.servicePartido = ServicePartido;
            this.servicePago = ServicePago;
            this.serviceLicencia = ServiceLicencia;
        }

        /// <summary>
        /// Alta de Empresa
        /// </summary>
        //[Authorize]
        [Route("")]
        // post: api/Empresa
        [HttpPost]
        public ActionResult AltaEmpresa([FromBody] RequestAlta<EmpresaDTO> request)
        {
            try
            {
                var entidad = mapper.Map<Empresa>(request.Model);

                return Ok(GenerarRespuestaCreacionExitosa(this.service.Alta(entidad)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Modificar Empresa
        /// </summary>
        //[Authorize]
        [Route("")]
        // post: api/Empresa
        [HttpPut]
        public ActionResult ModificarEmpresa([FromBody] RequestModify<EmpresaDTO> request)
        {
            try
            {
                var entidad = mapper.Map<Empresa>(request.Model);
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
        /// Modificar Cantidad Equipos Medición Empresa
        /// </summary>
        //[Authorize]
        [Route("{idEmpresa:int}/EquipoMedicion/ActualizacionCantidad")]
        [HttpPut]
        public ActionResult ModificarCantidadEquiposMedicionEmpresa([FromBody] RequestModify<EquipoMedicionTotalizadoDTO> request, int idEmpresa)
        {
            try
            {
                ((EquipoMedicionService)this.serviceEquipoMedicion).ActualizarCantidadEquiposMedicionEmpresa(idEmpresa, request.Model.EquipoMedicionNombre, 
                    request.Model.Cantidad);

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
        /// Obtener Todos los Empleados Empresa
        /// </summary>
        [Authorize]
        [Route("{idEmpresa:int}/Empleados")]
        [HttpGet]
        public ActionResult ObtenerEmpleados(int idEmpresa, bool activo = true)
        {
            try
            {
                var empleados = this.serviceEmpleado.ObtenerListado(new Empleado() { EmpresaId = idEmpresa, Activo = activo});

                return Ok(this.mapper.Map<List<EmpleadoDTO>>(empleados));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Todas las visitas de empresa, segun estado.
        /// </summary>
        //[Authorize]
        [Route("{idEmpresa:int}/Visitas")]
        [HttpGet]
        public ActionResult ObtenerVisitasEmpresa(int idEmpresa, bool activo = true, int estadoVisitaId = 0, int clienteId = 0)
        {
            try
            {
                var visitas = ((VisitaService) serviceVisita).ObtenerPlanesVisitasEmpresa(idEmpresa, activo, estadoVisitaId, clienteId);

                return Ok(visitas);
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
        [Route("{idEmpresa:int}/VisitasPorEstado")]
        [HttpGet]
        public ActionResult ObtenerVisitasPorEstadoEmpresa(int idEmpresa)
        {
            try
            {
                var resultado = ((EmpresaService) service).ObtenerTotalizadoVisitasPorEstado(idEmpresa);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Todos los Planes Empresa
        /// </summary>
        //[Authorize]
        [Route("{idEmpresa:int}/Planes")]
        [HttpGet]
        public ActionResult ObtenerPlanes(int idEmpresa,bool? activo, int estadoPlan = 0)
        {
            try
            {
                var planes = ((PlanService)this.servicePlan).ObtenerPlanesEmpresaTotalizados(new Plan() { EmpresaId =idEmpresa, Activo = activo, EstadoId = estadoPlan });

                return Ok(planes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Empresas
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpGet]
        public ActionResult ObtenerEmpresas(bool activo = true)
        {
            try
            {
                var empresas = this.service.ObtenerListado(new Empresa() { Activo = activo }, propiedadesAIncluir: "Direccion,Responsable");
                return Ok(this.mapper.Map<List<EmpresaSummaryDTO>>(empresas));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Empresa por Id
        /// </summary>
        //[Authorize]
        [Route("{idEmpresa:int}")]
        // post: api/Empresa
        [HttpGet]
        public ActionResult ObtenerEmpresaPorId(int idEmpresa)
        {
            try
            {
                var empresa = this.service.ObtenerListado(
                    new Empresa() { Id = idEmpresa }, "Direccion,Responsable").FirstOrDefault();

                empresa.Direccion.Partido = this.servicePartido.ObtenerListado
                                                (new Partido() { Id = empresa.Direccion.PartidoId }).FirstOrDefault(); 
                return Ok(empresa);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Clientes Empresa
        /// </summary>
        //[Authorize]
        [Route("{idEmpresa:int}/Clientes")]
        // post: api/Empresa
        [HttpGet]
        public ActionResult ObtenerClientesEmpresa(int idEmpresa)
        {
            try
            {
                return Ok(this.serviceCliente.ObtenerListado(new Cliente() { EmpresaId = idEmpresa }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }             
        
        /// <summary>
        /// Obtener Clientes Empresa Resumen
        /// </summary>
        //[Authorize]
        [Route("{idEmpresa:int}/ClientesResumen")]
        // post: api/Empresa
        [HttpGet]
        public ActionResult ObtenerClientesEmpresaResumen(int idEmpresa)
        {
            try
            {
                var clientes = this.serviceCliente.ObtenerListado(new Cliente() { EmpresaId = idEmpresa }, "Establecimientos,Planes");

                return Ok(this.mapper.Map<List<ClienteSummaryDTO>>(clientes));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }        
        
        /// <summary>
        /// Obtener Equipos Medicion Empresa
        /// </summary>
        //[Authorize]
        [Route("{idEmpresa:int}/EquiposMedicion")]
        // post: api/Empresa
        [HttpGet]
        public ActionResult ObtenerEquiposMedicionEmpresa(int idEmpresa)
        {
            try
            {
                return Ok(this.serviceEquipoMedicion.ObtenerListado(new EquipoMedicion() { EmpresaId = idEmpresa }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Equipos Medicion Empresa Totalizado
        /// </summary>
        //[Authorize]
        [Route("{idEmpresa:int}/EquiposMedicionTotalizado")]
        [HttpGet]
        public ActionResult ObtenerEquiposMedicionEmpresaTotalizado(int idEmpresa)
        {
            try
            {
                var servicioEquipoMedicion = (EquipoMedicionService)this.serviceEquipoMedicion;
                return Ok(servicioEquipoMedicion.ObtenerEquiposMedicionTotalizados(new EquipoMedicion() { EmpresaId = idEmpresa }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Pagos empresa
        /// </summary>
        //[Authorize]
        [Route("{idEmpresa:int}/Pagos")]
        [HttpGet]
        public ActionResult ObtenerPagos(int idEmpresa)
        {
            try
            {
                var pagos = this.servicePago.ObtenerListado(new Pago() { EmpresaId = idEmpresa }, "MedioPago");

                return Ok(pagos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Obtener Licencia Empresa
        /// </summary>
        //[Authorize]
        [Route("{idEmpresa:int}/Licencia")]
        [HttpGet]
        public ActionResult ObtenerLicencia(int idEmpresa)
        {
            try
            {
                var licencia = this.serviceLicencia.ObtenerListado(new Licencia() { EmpresaId = idEmpresa }, "Estado,Pago,TipoLicencia").FirstOrDefault();

                return Ok(licencia);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }

        private static string GenerarRespuestaCreacionExitosa(int id)
        {
            return JObject.FromObject(new
            {
                id = id
            }).ToString();
        }
    }
}