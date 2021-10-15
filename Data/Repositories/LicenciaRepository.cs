using Data.EF;
using Data.Entities;
using Data.Repositories.Contracts;
using Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class LicenciaRepository : GenericRepository<Licencia>
    {
        IGenericRepository<TasaConversionTipoLicencia> repoTasa;
        public LicenciaRepository(AplicationDbContext context, ILogger<GenericRepository<Licencia>> Logger,
            IGenericRepository<TasaConversionTipoLicencia> RepoTasa) : base(context, Logger)
        {
            this.repoTasa = RepoTasa;
        }

        protected override IEnumerable<Licencia> AplicarFiltrado(IQueryable<Licencia> entities, Licencia parameters)
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
                if (parameters.EstadoId != 0)
                {
                    entities = FiltrarPor(entities, x => x.EstadoId.Equals(parameters.EstadoId));
                }

                return entities.ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
        }

        protected override void CargarAtributosAModificar(Licencia editedEntity, Licencia entity)
        {
            editedEntity.TipoLicenciaId = entity.TipoLicenciaId;
            editedEntity.FechaFin = entity.FechaFin;
            editedEntity.EstadoId = entity.EstadoId;

        }
    }
}
