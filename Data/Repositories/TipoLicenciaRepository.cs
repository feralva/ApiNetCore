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
    public class TipoLicenciaRepository : GenericRepository<TipoLicencia>
    {
        public TipoLicenciaRepository(AplicationDbContext context, ILogger<GenericRepository<TipoLicencia>> Logger) : base(context, Logger)
        {
        }

        protected override IEnumerable<TipoLicencia> AplicarFiltrado(IQueryable<TipoLicencia> entities, TipoLicencia parameters)
        {
            try
            {
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

        protected override void CargarAtributosAModificar(TipoLicencia editedEntity, TipoLicencia entity)
        {
            editedEntity.Nombre = entity.Nombre;
            editedEntity.Descripcion = entity.Descripcion;
            editedEntity.Cantidad_Maxima_Usuarios = entity.Cantidad_Maxima_Usuarios;
            editedEntity.Activo = entity.Activo;

            //repoPrecio..Update(entity.Direccion);

        }
    }
}
