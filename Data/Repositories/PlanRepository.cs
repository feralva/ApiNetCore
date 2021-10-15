using Data.EF;
using Data.Entities;
using Data.Entities.EntidadesNoPersistidas;
using Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PlanRepository : GenericRepository<Plan>
    {
        public PlanRepository(AplicationDbContext context, ILogger<GenericRepository<Plan>> Logger) : base(context, Logger)
        {
        }

        protected override IEnumerable<Plan> AplicarFiltrado(IQueryable<Plan> entities, Plan parameters)
        {
            try
            {
                if (parameters.Activo != null)
                {
                    entities = FiltrarPor(entities, x => x.Activo == parameters.Activo);
                }                               
                if (parameters.ClienteId != 0)
                {
                    entities = FiltrarPor(entities, x => x.ClienteId == parameters.ClienteId);
                }                
                if (parameters.Id != 0)
                {
                    entities = FiltrarPor(entities, x => x.Id == parameters.Id);
                }
                if (parameters.EmpleadoId != 0)
                {
                    entities = FiltrarPor(entities, x => x.EmpleadoId == parameters.EmpleadoId);
                }                
                if (parameters.EmpresaId != 0)
                {
                    entities = FiltrarPor(entities, x => x.EmpresaId == parameters.EmpresaId);
                }                
                if (parameters.EstadoId != 0)
                {
                    entities = FiltrarPor(entities, x => x.EstadoId == parameters.EstadoId);
                } 
                if (parameters.FechaCreacion != null)
                {
                    entities = FiltrarPor(entities, x => x.FechaCreacion >= parameters.FechaCreacion);
                } 
                if (parameters.TipoPlanId != 0)
                {
                    entities = FiltrarPor(entities, x => x.TipoPlanId == parameters.TipoPlanId);
                }
                if (parameters.PlanesEstablecimientos != null && parameters.PlanesEstablecimientos.Count > 0)
                {
                    var planEstablecimiento = parameters.PlanesEstablecimientos.First();
                    entities = FiltrarPor(entities, x => x.PlanesEstablecimientos.Any(y => y.EstablecimientoId == planEstablecimiento.EstablecimientoId));
                }

                return entities.ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
        }

        public List<PlanesPorCliente> ObtenerPlanesPorCliente(Plan plan)
        {
            List<PlanesPorCliente> planesTotalizados = new List<PlanesPorCliente>();

            var planes = this.GetFiltered(plan, "Empleado,TipoPlan,Estado");

            foreach (Plan unPlan in planes)
            {
                TotalizadoPlanes totalizados = this.ObtenerTotalizadosPlanCliente(unPlan.Id);
                planesTotalizados.Add(new PlanesPorCliente() { Plan = unPlan, Totalizados = totalizados });
            }

            return planesTotalizados;
        }        
        public List<Plan> ObtenerPlanesPorEmpresa(Plan plan)
        {

            var planes = this.GetFiltered(plan, "Empleado,TipoPlan,Estado,PlanesEstablecimientos,Cliente,Visitas");

            return planes.ToList();
        }

        public override int Insert(Plan plan)
        {
            try
            {

                var establecimientos = plan.PlanesEstablecimientos.Select(pe => pe.EstablecimientoId).Distinct();
                plan.PlanesEstablecimientos = null;

                entities.Add(plan);
                context.SaveChanges();

                foreach (var establecimientoId in establecimientos)
                {
                    this.context.PlanEstablecimiento.Add(new PlanEstablecimiento() 
                        {PlanId = plan.Id, EstablecimientoId = establecimientoId });
                }

                context.SaveChanges();
                return plan.Id;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }

        }

        protected override void CargarAtributosAModificar(Plan editedEntity, Plan entity)
        {
            editedEntity.EstadoId = entity.EstadoId;
        }

        private TotalizadoPlanes ObtenerTotalizadosPlanCliente(int id)
        {
            var cantidadVisitasPendientes = this.entities.Include(a=> a.Visitas).Where(p => p.Id.Equals(id)).FirstOrDefault().Visitas.Count();
            var CantidadEstablecimientos = this.entities.Include(a => a.PlanesEstablecimientos).Where(p => p.Id.Equals(id)).FirstOrDefault().PlanesEstablecimientos.Count();
            
            //hackeada para traer sub-entidades
            //var empleado = this.entities.Include(plan => plan.Empleado).Where(p => p.Id.Equals(id)).FirstOrDefault().Empleado;
            //var tipo = this.entities.Include(plan => plan.TipoPlan).Where(p => p.Id.Equals(id)).FirstOrDefault().TipoPlan;
            //var estado = this.entities.Include(plan => plan.Estado).Where(p => p.Id.Equals(id)).FirstOrDefault().Estado;

            return new TotalizadoPlanes()
            {
                CantidadEstablecimientos = CantidadEstablecimientos,
                CantidadVisitasPendientes = cantidadVisitasPendientes
            };
        }
    }
}
