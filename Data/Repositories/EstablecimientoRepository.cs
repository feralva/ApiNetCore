using Data.EF;
using Data.Entities;
using Data.Repositories.Contracts;
using Exceptions;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class EstablecimientoRepository : GenericRepository<Establecimiento>
    {
        IGenericRepository<Direccion> repoDireccion;
        IGenericRepository<Ubicacion> repoUbicacion;
        public EstablecimientoRepository(AplicationDbContext context, ILogger<GenericRepository<Establecimiento>> Logger,
            IGenericRepository<Direccion> RepoDireccion, IGenericRepository<Ubicacion> RepoUbicacion) : base(context, Logger)
        {
            repoDireccion = RepoDireccion;
            repoUbicacion = RepoUbicacion;
        }

        protected override IEnumerable<Establecimiento> AplicarFiltrado(IQueryable<Establecimiento> entities, Establecimiento parameters)
        {
            try
            {
                if (parameters.Id != 0)
                {
                    entities = FiltrarPor(entities, x => x.Id.Equals(parameters.Id));
                }
                if (parameters.ClienteId != 0)
                {
                    entities = FiltrarPor(entities, x => x.ClienteId.Equals(parameters.ClienteId));
                }
                //if (parameters.PlanesEstablecimientos.First().EstablecimientoId != 0)
                //{
                //    entities = FiltrarPor(entities, x =>  x.PlanesEstablecimientos EstablecimientoId == parameters.PlanesEstablecimientos.First().EstablecimientoId);
                //}

                return entities.ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
        }

        public List<Establecimiento> ObtenerEstablecimientosPorCliente(Establecimiento establecimiento)
        {

            var establecimientos = this.GetFiltered(establecimiento, "Direccion,Ubicaciones");

            return establecimientos.ToList();
        }

        protected override void CargarAtributosAModificar(Establecimiento editedEntity, Establecimiento entity)
        {
            editedEntity.Nombre = entity.Nombre;

            //Hijos
            entity.Direccion.Id = editedEntity.DireccionId;
            repoDireccion.Update(entity.Direccion);
            //entity.Responsable.Id = editedEntity.ResponsableEmpresaId;
            //first.Where(item => !second.Any(e => item == e));

            foreach (var ubicacion in entity.Ubicaciones.Where(ub => !editedEntity.Ubicaciones.Any(e => e.Id == ub.Id)))
            {
                ubicacion.EstablecimientoId = entity.Id;
                repoUbicacion.Insert(ubicacion);
            }
            
            ////Codigo para borrar Ubicaciones cuando se implemente el delete
            //foreach (var ubicacionABorrar in editedEntity.Ubicaciones.Where(ub => !entity.Ubicaciones.Any(e => e.Id == ub.Id)))
            //{
            //    repoUbicacion.Delete(ubicacionABorrar);
            //}
            

            //repoResponsable.Update(entity.Responsable);
        }
    }
}
