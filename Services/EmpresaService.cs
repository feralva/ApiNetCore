using AutoMapper;
using Data.Entities;
using Data.Entities.Seguridad;
using Data.Repositories;
using Data.Repositories.Contracts;
using Encriptacion;
using Exceptions;
using Microsoft.Extensions.Logging;
using Model;
using Model.RequestHttp;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class EmpresaService : GenericService<Empresa>
    {
        IGenericService<Licencia> servicioLicencia;
        IGenericService<Empleado> servicioEmpleado;
        IEmailService emailService;
        public EmpresaService(IGenericRepository<Empresa> Repository, IMapper mapper, ILogger<EmpresaService> Logger,
            IGenericService<Licencia> ServicioLicencia, IGenericService<Empleado> ServicioEmpleado,
            IEmailService EmailService) 
            : base(Repository, mapper, Logger)
        {
            servicioLicencia = ServicioLicencia;
            servicioEmpleado = ServicioEmpleado;
            emailService = EmailService;
        }

        public override int Alta(Empresa empresa)
        {
            try
            {
                Validaciones(empresa);

                int idEmpresa = base.Alta(empresa);

                //Se genera por Default una licencia de 14 dias de prueba
                this.servicioLicencia.Alta(new Licencia()
                {
                    EmpresaId = idEmpresa,
                    EstadoId = 1,
                    FechaFin = DateTime.Now.AddDays(14),
                    TipoLicenciaId = 5,
                });

                var contrasenia = RandomPasswordService.RandomString(8);
                var empleado = new Empleado()
                {
                    Apellido = empresa.Responsable.Apellido,
                    Nombre = empresa.Responsable.Nombre,
                    Activo = true,
                    CorreoElectronico = empresa.Responsable.CorreoElectronico,
                    EmpresaId = idEmpresa,
                    urlFoto = "",
                    Usuario = new Usuario()
                    {
                        IdUsuario = empresa.Responsable.CorreoElectronico,
                        Contraseña = new HashingService().CreateSha256(contrasenia),
                        UsuarioFamilia = new List<UsuarioFamilia>() { new UsuarioFamilia()
                            {
                                IdUsuario = empresa.Responsable.CorreoElectronico,
                                IdFamilia = "Gerente"
                            }
                        }
                    }
                };

                this.servicioEmpleado.Alta(empleado);
 
                this.emailService.enviarEmailBienvenidaEmpresa(empresa.Responsable.CorreoElectronico, empresa.Nombre);
                this.emailService.enviarEmailBienvenidaUsuario(empleado.Usuario,contrasenia);

                return idEmpresa;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private void Validaciones(Empresa empresa)
        {
            YaEstaRegistradaEmpresa(empresa.Nombre);
        }

        private void YaEstaRegistradaEmpresa(string nombreEmpresa)
        {
            if (this.ObtenerListado(new Empresa() { Nombre = nombreEmpresa }).ToList().Count > 0)
                throw new EmpresaYaRegistradaException();
        }

        public Dictionary<string, int> ObtenerTotalizadoVisitasPorEstado(int idEmpresa)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (var totalizado in ((EmpresaRepository)this.repository).ObtenerTotalizadoVisitasPorEstado(idEmpresa))
            {
                result.Add(totalizado.Estado, totalizado.Cantidad);
            }
            return result;
        }
    }
}
