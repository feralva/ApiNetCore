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
    public class PagoRepository : GenericRepository<Pago>
    {
        public PagoRepository(AplicationDbContext context, ILogger<GenericRepository<Pago>> Logger) : base(context, Logger)
        {
        }

        protected override IEnumerable<Pago> AplicarFiltrado(IQueryable<Pago> entities, Pago parameters)
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

                return entities.ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
        }

        protected override void CargarAtributosAModificar(Pago editedEntity, Pago entity)
        {
            //editedEntity.Nombre = entity.Nombre;
            //editedEntity.UrlFoto = entity.UrlFoto;

            ////Hijos
            //entity.Direccion.Id = editedEntity.DireccionId;
            //repoDireccion.Update(entity.Direccion);
            //entity.Responsable.Id = editedEntity.ResponsableEmpresaId;
            //repoResponsable.Update(entity.Responsable);
        }
    }
}
