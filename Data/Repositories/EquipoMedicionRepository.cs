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
    public class EquipoMedicionRepository : GenericRepository<EquipoMedicion>
    {
        public EquipoMedicionRepository(AplicationDbContext context, ILogger<GenericRepository<EquipoMedicion>> Logger) : base(context, Logger)
        {
        }

        protected override IEnumerable<EquipoMedicion> AplicarFiltrado(IQueryable<EquipoMedicion> entities, EquipoMedicion parameters)
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
                if (parameters.TipoEquipoMedicionId != 0)
                {
                    entities = FiltrarPor(entities, x => x.TipoEquipoMedicionId.Equals(parameters.TipoEquipoMedicionId));
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
        public IEnumerable<EquipoMedicionTotalizado> ObtenerEquiposMedicionTotalizados(EquipoMedicion equipoMedicion)
        {
            try
            {
                var equiposMedicion = this.GetFiltered(equipoMedicion);

                var results = (from p in equiposMedicion
                              group p by p.Nombre into g
                              select new { Nombre = g.Key, EquiposMedicion = g.ToList(), Cantidad = g.Count()})
                                .Select(x => new EquipoMedicionTotalizado()
                                {
                                    Cantidad = x.Cantidad,
                                    EquipoMedicionNombre = x.Nombre
                                });

                return results;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
            
        }
    }
}
