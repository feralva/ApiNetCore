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
    public class EmpresaRepository : GenericRepository<Empresa>
    {
        private IGenericRepository<Direccion> repoDireccion;
        private IGenericRepository<ResponsableEmpresa> repoResponsable;
        public EmpresaRepository(AplicationDbContext context, ILogger<EmpresaRepository> Logger,
            IGenericRepository<Direccion> RepoDireccion, IGenericRepository<ResponsableEmpresa> RepoResponsable) : base(context, Logger)
        {
            repoDireccion = RepoDireccion;
            repoResponsable = RepoResponsable;
        }

        protected override IEnumerable<Empresa> AplicarFiltrado(IQueryable<Empresa> entities, Empresa parameters)
        {
            try
            {
                if (!string.IsNullOrEmpty(parameters.Nombre))
                {
                    entities = FiltrarPor(entities, x => x.Nombre.Contains(parameters.Nombre));
                }
                if (parameters.Id != 0)
                {
                    entities = FiltrarPor(entities, x => x.Id.Equals(parameters.Id));
                }                
                if (parameters.Activo != null)
                {
                    entities = FiltrarPor(entities, x => x.Activo.Equals(parameters.Activo));
                }

                return entities.ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
        }

        protected override void CargarAtributosAModificar(Empresa editedEntity, Empresa entity)
        {
            editedEntity.Nombre = entity.Nombre;
            editedEntity.UrlFoto = entity.UrlFoto;
            editedEntity.Activo = entity.Activo;

            //Hijos
            if (entity.Direccion != null)
            {
                entity.Direccion.Id = editedEntity.DireccionId;
                repoDireccion.Update(entity.Direccion);
            }

            if (entity.Responsable != null)
            {
                entity.Responsable.Id = editedEntity.ResponsableEmpresaId;
                repoResponsable.Update(entity.Responsable);
            }

        }

        public List<TotalizadoVisitasPorEstado> ObtenerTotalizadoVisitasPorEstado(int idEmpresa)
        {
            return context.TotalizadoVisitasPorEstado.FromSqlRaw($"dbo.ObtenerVisitasPorEstadoEmpresa {idEmpresa}").ToList();
        }
    }
}
