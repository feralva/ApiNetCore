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
    public class IrregularidadRepository : GenericRepository<Irregularidad>
    {
        public IrregularidadRepository(AplicationDbContext context, ILogger<GenericRepository<Irregularidad>> Logger) : base(context, Logger)
        {
        }

        protected override IEnumerable<Irregularidad> AplicarFiltrado(IQueryable<Irregularidad> entities, Irregularidad parameters)
        {
            try
            {
                if (parameters.Id != 0)
                {
                    entities = FiltrarPor(entities, x => x.Id.Equals(parameters.Id));
                }
                if (parameters.UbicacionId != 0)
                {
                    entities = FiltrarPor(entities, x => x.UbicacionId.Equals(parameters.UbicacionId));
                }
                if (parameters.TipoId != 0)
                {
                    entities = FiltrarPor(entities, x => x.TipoId.Equals(parameters.TipoId));
                }                
                if (parameters.EstadoId != 0)
                {
                    entities = FiltrarPor(entities, x => x.EstadoId.Equals(parameters.EstadoId));
                }
                if (parameters.ClienteId != 0)
                {
                    entities = FiltrarPor(entities, x => x.ClienteId.Equals(parameters.ClienteId));
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

        protected override void CargarAtributosAModificar(Irregularidad editedEntity, Irregularidad entity)
        {
            editedEntity.DescripcionResolucion = entity.DescripcionResolucion;
            editedEntity.FechaFinalizado = DateTime.Now;
            editedEntity.EstadoId = 2;
            editedEntity.UrlEvidenciaResultado = entity.UrlEvidenciaResultado;

        }

    }
}
