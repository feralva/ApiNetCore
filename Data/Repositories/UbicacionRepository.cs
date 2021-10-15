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
    public class UbicacionRepository : GenericRepository<Ubicacion>
    {
        public UbicacionRepository(AplicationDbContext context, ILogger<UbicacionRepository> Logger) : base(context, Logger)
        {
        }

        protected override IEnumerable<Ubicacion> AplicarFiltrado(IQueryable<Ubicacion> entities, Ubicacion parameters)
        {
            try
            {
                if (parameters.Id != 0)
                {
                    entities = FiltrarPor(entities, x => x.Id.Equals(parameters.Id));
                }
                if (parameters.EstablecimientoId != 0)
                {
                    entities = FiltrarPor(entities, x => x.EstablecimientoId.Equals(parameters.EstablecimientoId));
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
