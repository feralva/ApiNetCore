using AutoMapper;
using Data.Entities;
using Data.Entities.Seguridad;
using Data.Repositories.Contracts;
using Exceptions;
using Microsoft.Extensions.Logging;
using Model;
using Model.RequestHttp;
using Model.Seguridad;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Services
{
    public class EmpleadoService : GenericService<Empleado>
    {
        private IGenericService<Visita> serviceVisita;
        private IGenericService<Licencia> serviceLicencia;
        private ISeguridadRepository seguridadRepository;
        private IEmailService emailService;
        public EmpleadoService(ISeguridadRepository SeguridadRepository, IGenericRepository<Empleado> Repository,
            IMapper mapper, ILogger<EmpleadoService> Logger, IGenericService<Visita> ServiceVisita,
            IGenericService<Licencia> ServiceLicencia, IEmailService EmailService) 
            : base(Repository, mapper, Logger)
        {
            this.serviceVisita = ServiceVisita;
            this.seguridadRepository = SeguridadRepository;
            this.serviceLicencia = ServiceLicencia;
            this.emailService = EmailService;
        }

        public override int Alta(Empleado empleado)
        {
            this.ValidarSiSeAlcanzoNumeroMaximoUsuarios(empleado.EmpresaId);

            var idEmpleado = base.Alta(empleado);

            if(this.CantidadActualEmpleadosEmpresa(empleado.EmpresaId) > 1) 
                this.emailService.enviarEmailBienvenidaUsuario(empleado.Usuario, "Contactar Coordinador");

            return idEmpleado;
        }

        private void ValidarSiSeAlcanzoNumeroMaximoUsuarios(int empresaId)
        {
            int cantEmpleadosActual = this.CantidadActualEmpleadosEmpresa(empresaId);
            int cantidadMaximaEmpleadosPorLicencia = serviceLicencia.ObtenerListado(new Licencia() { EmpresaId = empresaId }, "TipoLicencia")
                                                        .FirstOrDefault().TipoLicencia.Cantidad_Maxima_Usuarios;
            if (cantEmpleadosActual >= cantidadMaximaEmpleadosPorLicencia)
            {
                throw new NumeroMaximoUsuariosLicenciaAlcanzadosException();
            }
        }

        private int CantidadActualEmpleadosEmpresa(int empresaId)
        {
            return this.ObtenerListado(new Empleado() { EmpresaId = empresaId, Activo = true }).Count();
        }

        public IEnumerable<Visita> ObtenerVisitasEmpleado(int idEmpleado)
        {
            try
            {
                return this.serviceVisita.ObtenerListado(new Visita() { EmpleadoId = idEmpleado }); 
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EmpleadoDTO ObtenerDetalleEmpleado(int idEmpleado)
        {
            var empleadoEntity = ObtenerListado(new Empleado() { Id = idEmpleado }).First();

            var empleadoDTO =  this.mapper.Map<EmpleadoDTO>(empleadoEntity);

            empleadoDTO.Roles = this.seguridadRepository.ObtenerRolesUsuario(empleadoEntity.CorreoElectronico)
                                        .Select(f => f.Nombre).ToList();

            return empleadoDTO;
        }

    }
}
