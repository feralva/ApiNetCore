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
    public class TipoIrregularidadRepository : GenericRepository<TipoIrregularidad>
    {
        public TipoIrregularidadRepository(AplicationDbContext context, ILogger<GenericRepository<TipoIrregularidad>> Logger) : base(context, Logger)
        {
        }

        protected override IEnumerable<TipoIrregularidad> AplicarFiltrado(IQueryable<TipoIrregularidad> entities, TipoIrregularidad parameters)
        {
            try
            {
                if (parameters.Id != 0)
                {
                    entities = FiltrarPor(entities, x => x.Id.Equals(parameters.Id));
                }

                return entities.ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
        }
    }
}
