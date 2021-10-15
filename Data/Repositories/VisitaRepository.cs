using Data.EF;
using Data.Entities;
using Data.Entities.EntidadesNoPersistidas;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Data.Repositories
{
    public class VisitaRepository : GenericRepository<Visita>
    {
        public VisitaRepository(AplicationDbContext context, ILogger<GenericRepository<Visita>> Logger) : base(context, Logger)
        {
        }

        protected override IEnumerable<Visita> AplicarFiltrado(IQueryable<Visita> entities, Visita parameters)
        {
            try
            {
                if (parameters.Id != 0)
                {
                    entities = FiltrarPor(entities, x => x.Id.Equals(parameters.Id));
                } 
                
                if (parameters.EmpleadoId != null)
                {
                    entities = FiltrarPor(entities, x => x.EmpleadoId.Equals(parameters.EmpleadoId));
                }                
                
                if (parameters.EstablecimientoId != 0)
                {
                    entities = FiltrarPor(entities, x => x.EstablecimientoId.Equals(parameters.EstablecimientoId));
                }                
                if (parameters.PlanId != 0)
                {
                    entities = FiltrarPor(entities, x => x.PlanId.Equals(parameters.PlanId));
                }
                if (parameters.EstadoId != 0)
                {
                    entities = FiltrarPor(entities, x => x.EstadoId.Equals(parameters.EstadoId));
                }                
                if (parameters.Fecha != null)
                {
                    entities = FiltrarPor(entities, x => x.Fecha.Value.Date.Equals(parameters.Fecha.Value.Date));
                }
                //if (parameters.Activo != 0)
                //{
                //    entities = FiltrarPor(entities, x => x.Activo.Equals(parameters.Activo));
                //}

                return entities.ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
        }

        public void ReasignarVisita(Visita visita)
        {
            var entity = context.Visita.FirstOrDefault(item => item.Id == visita.Id);

            // Validate entity is not null
            if (entity != null)
            {

                entity.EmpleadoId = visita.EmpleadoId;

                // context.Products.Update(entity);

                context.SaveChanges();
            }
        }
        public void CambiarFechaVisita(Visita visita)
        {
            var entity = context.Visita.FirstOrDefault(item => item.Id == visita.Id);

            if (entity != null)
            {

                entity.Fecha = visita.Fecha;
                entity.Duracion = visita.Duracion;

                context.SaveChanges();
            }
        }        
        public void CompletarVisita(Visita visita)
        {
            var entity = context.Visita.FirstOrDefault(item => item.Id == visita.Id);

            if (entity != null)
            {

                entity.EstadoId = context.EstadoVisita.FirstOrDefault(v => v.Descripcion == "Completa").Id;

                context.SaveChanges();
            }
        }

        public void CancelarVisita(Visita visita)
        {
            var entity = context.Visita.FirstOrDefault(item => item.Id == visita.Id);

            if (entity != null)
            {

                entity.EstadoId = context.EstadoVisita.FirstOrDefault(v => v.Descripcion == "Cancelada").Id;

                context.SaveChanges();
            }
        }

        public List<VisitaSummary> ObtenerPlanesPorEmpresa(int idEmpresa, bool activo, int estadoVisitaId, int clienteId)
        {
            IQueryable<Visita> visitas = this.context.Visita.Include(v => v.Plan)
                                            .ThenInclude(p => p.Cliente)
                                        .Include(v => v.Establecimiento)
                                        .Include(v => v.TipoVisita)
                                        .Include(v => v.Empleado)
                                        .Include(v => v.Estado);
           
            if(idEmpresa != 0)
            {
                visitas = visitas.Where(x => x.Plan.EmpresaId.Equals(idEmpresa));
            }

            if (clienteId != 0)
            {
                visitas = visitas.Where(x => x.Plan.ClienteId.Equals(clienteId));
            }            
            
            if (estadoVisitaId != 0)
            {
                visitas = visitas.Where(x => x.EstadoId.Equals(estadoVisitaId));
            }

            return visitas.Select(v => new VisitaSummary()
                    {
                        EmpleadoAsignado = (v.Empleado != null) ? (v.Empleado.Apellido + ", " + v.Empleado.Nombre) : "NA",
                        Estado = v.Estado.Descripcion,
                        Fecha = v.Fecha,
                        FechaPactada = (new DateTime(v.AnioPactado, v.MesPactado, 01)),
                        id = v.Id,
                        TipoVisita = v.TipoVisita.Descripcion,
                        NombreCliente = (v.Plan.Cliente.Nombre),
                        NombreEstablecimiento = v.Establecimiento.Nombre
                    }).ToList();
        }        
        public List<Visita> ObtenerVisitasEmpleado(Visita visita)
        {
            var visitas = this.context.Visita.AsQueryable().Where(p => p.EmpleadoId == visita.EmpleadoId && p.Activo == visita.Activo)
                      .Include(p => p.Establecimiento)
                       .Include(e => e.Establecimiento.Cliente)
                      .Include(v => v.TipoVisita)
                      .Include(v => v.Empleado)
                      .Include(v => v.Estado);

            return visitas.ToList();
            //return visitas.OrderBy(v => v.FechaPactada).ToList();
        }
        /*protected override void CargarAtributosAModificar(Visita editedEntity, Visita entity)
        {
            editedEntity.EmpleadoId = entity.EmpleadoId;
            editedEntity. = entity.EmpleadoId;

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
        }*/
    }
}
