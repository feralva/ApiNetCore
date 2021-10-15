using Data.EF;
using Data.Entities;
using Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class TasaConversionTipoLicenciaRepository : GenericRepository<TasaConversionTipoLicencia>
    {
        public TasaConversionTipoLicenciaRepository(AplicationDbContext context, ILogger<GenericRepository<TasaConversionTipoLicencia>> Logger) : base(context, Logger)
        {
        }

        protected override IEnumerable<TasaConversionTipoLicencia> AplicarFiltrado(IQueryable<TasaConversionTipoLicencia> entities, TasaConversionTipoLicencia parameters)
        {
            try
            {
                if (parameters.Id != 0)
                {
                    entities = FiltrarPor(entities, x => x.Id.Equals(parameters.Id));
                }
                if (parameters.TipoLicenciaDestinoId != 0)
                {
                    entities = FiltrarPor(entities, x => x.TipoLicenciaDestinoId.Equals(parameters.TipoLicenciaDestinoId));
                }
                if (parameters.TipoLicenciaOrigenId != 0)
                {
                    entities = FiltrarPor(entities, x => x.TipoLicenciaOrigenId.Equals(parameters.TipoLicenciaOrigenId));
                }

                return entities.ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
        }

        protected override void CargarAtributosAModificar(TasaConversionTipoLicencia editedEntity, TasaConversionTipoLicencia entity)
        {
            editedEntity.RatioConversion = entity.RatioConversion;
        }

    }
}
