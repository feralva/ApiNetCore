using Data.EF;
using Data.Entities.EntidadesNoPersistidas;
using Data.Entities.Seguridad;
using Data.Repositories.Contracts;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class SeguridadRepository : ISeguridadRepository
    {
        protected ILogger<SeguridadRepository> logger;
        private readonly AplicationDbContext context;
        //protected DbSet<T> entities;

        public SeguridadRepository(AplicationDbContext context, ILogger<SeguridadRepository> Logger)
        {
            this.context = context;
            this.logger = Logger;
        }

        public IEnumerable<Familia> ObtenerRolesPosibles()
        {
            try
            {
                return this.context.Familia.ToList().Where(f => f.IdFamilia != "Administrador");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
            
        }

        public IEnumerable<Familia> ObtenerRolesUsuario(string idUsuario)
        {
            return this.context.Familia.AsQueryable()
                    .Where(f => f.UsuarioFamilia.Any(uf => uf.IdUsuario == idUsuario)).ToList();
        }

        public IEnumerable<Patente> ObtenerPatentesUsuario(string idUsuario)
        {
            return context.Patente
                      .FromSqlInterpolated($"Seguridad.ObtenerPatentesUsuario {idUsuario}")
                      .ToList();
        }

        public void ActualizarRolesUsuario(string idUsuario, ICollection<UsuarioFamilia> listaUsuarioFamilia)
        {
            this.context.UsuarioFamilia.RemoveRange(this.context.UsuarioFamilia.AsQueryable().Where(uf => uf.IdUsuario == idUsuario));

            this.context.UsuarioFamilia.AddRange(listaUsuarioFamilia);

            this.context.SaveChanges();
        }

        /// <summary>
        /// Reviso si el usuario que quiere loguearse existe, coincide su contraseña y se encuentra habilitado.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passEncriptada"></param>
        /// <returns></returns>
        public void ComprobarUsuario(string username, string passEncriptada)
        {
            var empleado = context.Empleado.AsQueryable().Where(emp => emp.UsuarioId.ToLower() == username.ToLower()).FirstOrDefault();

            var usuario = context.Empleado.AsQueryable().Where(emp => emp.UsuarioId.ToLower() == username.ToLower() && emp.Activo == true)
                .Include("Empresa").Select(emp => emp.Usuario).FirstOrDefault();

            var usuarioSinEmpresa = context.Usuario.AsQueryable().Where(u => u.IdUsuario.ToLower() == username.ToLower()).FirstOrDefault();

            if (usuarioSinEmpresa == null) throw new UsuarioOContraseniaIncorrectaException();
            if (usuarioSinEmpresa.Contraseña != passEncriptada) throw new UsuarioOContraseniaIncorrectaException();

            if (usuario != null)
            {
                var empresa = context.Empresa.AsQueryable().Where(e => e.Id == empleado.EmpresaId).FirstOrDefault();

                if (empresa.Activo == false) throw new EmpresaDeshabilitadaException();

                var licencia = context.Licencia.AsQueryable().Where(e => e.EmpresaId == empleado.EmpresaId).FirstOrDefault();

                if (licencia.FechaFin < DateTime.Now) throw new LicenciaVencidaException();
            }
        }

        public UsuarioDetalle ObtenerInformacionUsuario(string idUsuario)
        {
            try
            {

                var detalleEmpleado = (this.context.Empleado
                .Include(e => e.Usuario)
                .Include(e => e.Empresa)
                .Where(e => e.UsuarioId == idUsuario)
                    .Select(e => new UsuarioDetalle()
                    {
                        EmpleadoId = e.Id,
                        EmpresaId = e.EmpresaId,
                        EmpresaNombre = e.Empresa.Nombre,
                        Name = e.Apellido + ", " + e.Nombre,
                        UrlFotoEmpleado = e.urlFoto,
                        urlFotoEmpresa = e.Empresa.UrlFoto,
                        roles = this.ObtenerRolesUsuario(idUsuario).Select(r => r.Nombre).ToList(),
                        patentes = this.ObtenerPatentesUsuario(idUsuario).Select(r => r.Nombre).Distinct().ToList(),
                        Id = idUsuario
                    })).FirstOrDefault();
                if (detalleEmpleado == null)
                {
                    var usuario = idUsuario.Split('@')[0];
                    detalleEmpleado = (this.context.Usuario.AsQueryable()
                        .Where(e => e.IdUsuario == idUsuario)
                            .Select(e => new UsuarioDetalle()
                            {
                                EmpleadoId = 0,
                                EmpresaId = 0,
                                EmpresaNombre = "Safetify",
                                Name = usuario,
                                UrlFotoEmpleado = "",
                                urlFotoEmpresa = "https://firebasestorage.googleapis.com/v0/b/higiene-y-seguridad-feaf5.appspot.com/o/logo-removebg-preview.png?alt=media&token=a2f6afe5-453a-426c-b120-c428d036aa1a",
                                roles = this.ObtenerRolesUsuario(idUsuario).Select(r => r.Nombre).ToList(),
                                patentes = this.ObtenerPatentesUsuario(idUsuario).Select(r => r.Nombre).Distinct().ToList(),
                                Id = idUsuario
                            })).FirstOrDefault();
                }


                return detalleEmpleado;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                throw;
            }

        }

        public void AlmacenarRfreshToken(string idUsuario, string refreshToken, DateTime refreshTokenExpirationTime)
        {
            var usuario = this.context.Usuario.AsQueryable().Where(u => u.IdUsuario == idUsuario).FirstOrDefault();

            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenExpiryTime = refreshTokenExpirationTime;

            this.context.SaveChanges();
        }

        public Token ObtenerRefreshTokenUsuario(string username)
        {
            var usuario = this.context.Usuario.AsQueryable().Where(u => u.IdUsuario == username).FirstOrDefault();

            return new Token()
            {
                RefreshToken = usuario.RefreshToken,
                RefreshTokenExpirationTime = usuario.RefreshTokenExpiryTime
            };
        }

        public void GuardarNuevaContraseniaUser(string idUsuario, string nuevaContraseniaEncriptada)
        {
            var usuario = this.context.Usuario.AsQueryable().Where(u => u.IdUsuario == idUsuario).FirstOrDefault();
            usuario.Contraseña = nuevaContraseniaEncriptada;

            this.context.SaveChanges();
        }

        /// <summary>
        /// Compruebo si el usuario Enviado Se encuentra Registrado
        /// </summary>
        /// <param name="username"></param>
        public void ComprobarSiUsuarioExiste(string username)
        {
            var usuario = this.context.Usuario.AsQueryable().Where(u => u.IdUsuario == username).FirstOrDefault();

            if (usuario == null) throw new UsuarioNoExisteException();
        }
    }
}
