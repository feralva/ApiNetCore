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
    public class PartidoRepository : GenericRepository<Partido>
    {
        public PartidoRepository(AplicationDbContext context, ILogger<GenericRepository<Partido>> Logger) : base(context, Logger)
        {
        }

        protected override IEnumerable<Partido> AplicarFiltrado(IQueryable<Partido> entities, Partido parameters)
        {
            try
            {
                if (parameters.Id != 0)
                {
                    entities = FiltrarPor(entities, x => x.Id.Equals(parameters.Id));
                }
                
                if (parameters.ProvinciaId != 0)
                {
                    entities = FiltrarPor(entities, x => x.Provincia.Id.Equals(parameters.ProvinciaId));
                }

                if (!string.IsNullOrEmpty(parameters.Nombre))
                {
                    entities = FiltrarPor(entities, x => x.Nombre.Equals(parameters.Nombre));
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
