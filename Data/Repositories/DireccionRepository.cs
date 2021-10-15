using Data.EF;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class DireccionRepository : GenericRepository<Direccion>
    {
        public DireccionRepository(AplicationDbContext context, ILogger<DireccionRepository> Logger) : base(context, Logger){}

        protected override void CargarAtributosAModificar(Direccion editedEntity, Direccion entity)
        {
            editedEntity.Altura = entity.Altura;
            editedEntity.Calle = entity.Calle;
            editedEntity.Partido = entity.Partido;
        }
        public List<Partido> ObtenerPartidosProvincia(int idProvincia)
        {
            return this.context.Partido.AsQueryable().Where(p => p.ProvinciaId == idProvincia).ToList();
        }        
        public List<Provincia> ObtenerProvincias()
        {
            return this.context.Provincia.ToList();
        }

        public override Direccion Get(int id)
        {
            try
            {
                return this.entities
                            .Include(d => d.Partido).ThenInclude(p => p.Provincia)
                            .Where(d => d.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
