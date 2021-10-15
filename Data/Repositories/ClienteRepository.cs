using Data.EF;
using Data.Entities;
using Data.Entities.EntidadesNoPersistidas;
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
    public class ClienteRepository : GenericRepository<Cliente>
    {
        private IGenericRepository<Direccion> repoDireccion;
        private IGenericRepository<ResponsableEmpresa> repoResponsable;
        public ClienteRepository(IGenericRepository<Direccion> RepoDireccion, AplicationDbContext context,
            IGenericRepository<ResponsableEmpresa> RepoResponsable, ILogger<GenericRepository<Cliente>> Logger) : base(context, Logger)
        {
            this.repoDireccion = RepoDireccion;
            this.repoResponsable = RepoResponsable;
        }

        protected override IEnumerable<Cliente> AplicarFiltrado(IQueryable<Cliente> entities, Cliente parameters)
        {
            try
            {
                if (parameters.Id != 0)
                {
                    entities = FiltrarPor(entities, x => x.Id.Equals(parameters.Id));
                }
                if (parameters.EmpresaId != 0)
                {
                    entities = FiltrarPor(entities, x => x.EmpresaId.Equals(parameters.EmpresaId));
                }

                return entities.ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
        }

        public override Cliente Get(int id)
        {
            try
            {
                var cliente = entities.Include(r => r.Responsable)
                    .Include(d => d.Direccion).ThenInclude(d=>d.Partido)
                    .SingleOrDefault(s => s.Id == id);

                var provincia = this.context.Partido.AsQueryable().Where(p => p.Id == cliente.Direccion.PartidoId)
                    .Include(p => p.Provincia).FirstOrDefault().Provincia;

                return cliente;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
        }

        protected override void CargarAtributosAModificar(Cliente editedEntity, Cliente entity)
        {
            editedEntity.Nombre = entity.Nombre;
            editedEntity.UrlFoto = entity.UrlFoto;

            //Hijos
            entity.Direccion.Id = editedEntity.DireccionId;
            repoDireccion.Update(entity.Direccion);
            entity.Responsable.Id = editedEntity.ResponsableEmpresaId;
            repoResponsable.Update(entity.Responsable);
        }
        public List<TotalizadoVisitasPorEstado> ObtenerTotalizadoVisitasPorEstado(int idCliente)
        {
            return context.TotalizadoVisitasPorEstado.FromSqlRaw($"dbo.ObtenerVisitasPorEstadoCliente {idCliente}").ToList();
        }
    }
}
