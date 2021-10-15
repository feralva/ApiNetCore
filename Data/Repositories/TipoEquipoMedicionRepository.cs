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
    public class TipoEquipoMedicionRepository : GenericRepository<TipoEquipoMedicion>
    {
        public TipoEquipoMedicionRepository(AplicationDbContext context, ILogger<GenericRepository<TipoEquipoMedicion>> Logger) : base(context, Logger)
        {
        }

        protected override IEnumerable<TipoEquipoMedicion> AplicarFiltrado(IQueryable<TipoEquipoMedicion> entities, TipoEquipoMedicion parameters)
        {
            try
            {
                if (parameters.Id != 0)
                {
                    entities = FiltrarPor(entities, x => x.Id.Equals(parameters.Id));
                }
                if (parameters.Descripcion != null)
                {
                    entities = FiltrarPor(entities, x => x.Descripcion.Equals(parameters.Descripcion));
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
