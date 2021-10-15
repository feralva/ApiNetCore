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
    public class TipoPlanRepository : GenericRepository<TipoPlan>
    {
        public TipoPlanRepository(AplicationDbContext context, ILogger<GenericRepository<TipoPlan>> Logger) : base(context, Logger)
        {
        }

        protected override IEnumerable<TipoPlan> AplicarFiltrado(IQueryable<TipoPlan> entities, TipoPlan parameters)
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
