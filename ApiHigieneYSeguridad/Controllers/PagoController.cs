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
    public class PagoController : ControllerBase
    {
        private ILogger<PagoController> _logger;
        private IGenericService<Pago> service;
        private IGenericService<Licencia> serviceLicencia;
        protected IMapper mapper;

        public PagoController(ILogger<PagoController> logger, IGenericService<Pago> Service,
            IMapper Mapper, IGenericService<Licencia> ServiceLicencia)
        {
            _logger = logger;
            service = Service;
            mapper = Mapper;
            serviceLicencia = ServiceLicencia;
        }

        /// <summary>
        /// Registro Pago
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpPost]
        public ActionResult RegistrarPago([FromBody] RequestAlta<PagoDTO> request)
        {
            try
            {
                request.Model.Fecha = DateTime.Now;
                var entidad = mapper.Map<Pago>(request.Model);
                var id = this.service.Alta(entidad);

                //((LicenciaService)serviceLicencia).ProcesarAdquisicionLicencia(
                //    new Licencia() 
                //    { 
                //        EmpresaId = request.Model.EmpresaId,
                //        EstadoId = 1,
                //        FechaFin = DateTime.Now.AddDays(request.Model.CantidadMeses * 30),
                //        TipoLicenciaId = request.Model.TipoLicenciaId
                //    }, request.Model.CantidadMeses);

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
        /// Obtener Pagos
        /// </summary>
        //[Authorize]
        [Route("")]
        [HttpGet]
        public ActionResult ObtenerPagos(int EmpresaId = 0, int idPago = 0)
        {
            try
            {
                var pagos = this.mapper.Map<List<PagoDTO>>(this.service.ObtenerListado(
                    new Pago() { EmpresaId = EmpresaId, Id = idPago}, "Empresa,MedioPago,TipoLicencia"));

                return Ok(pagos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(404, ex.Message);
            }
        }
    }
}