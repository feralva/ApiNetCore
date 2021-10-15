using AutoMapper;
using Data.Entities;
using Data.Entities.EntidadesNoPersistidas;
using Data.Repositories;
using Data.Repositories.Contracts;
using Exceptions;
using Microsoft.Extensions.Logging;
using Model;
using Model.PlanModels;
using Model.Visita;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class PlanService : GenericService<Plan>
    {
        private IGenericRepository<Visita> repoVisita;
        //private IReportingService reportingService;
        public PlanService(IGenericRepository<Plan> Repository, IMapper mapper, ILogger<PlanService> Logger, 
            IGenericRepository<Visita> RepoVisita/*, IReportingService ReportingService*/) : base(Repository, mapper, Logger)
        {
            this.repoVisita = RepoVisita;
            //this.reportingService = ReportingService;
        }

        public List<PlanesPorCliente> ObtenerPlanesClienteTotalizados(Plan plan)
        {
            try
            {
                return ((PlanRepository)this.repository).ObtenerPlanesPorCliente(plan);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }        
        public List<PlanEmpresaDTO> ObtenerPlanesEmpresaTotalizados(Plan plan)
        {
            try
            {
                var planes = ((PlanRepository)this.repository).ObtenerPlanesPorEmpresa(plan);

                return mapper.Map<List<PlanEmpresaDTO>>(planes);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }

        public PlanDetalleDTO ObtenerPlanDetalle(Plan plan)
        {
            try
            {
                var planDetallado = this.repository.GetFiltered(plan, "TipoPlan,Cliente,Empleado,Estado,Visitas").FirstOrDefault();

                planDetallado.Visitas = this.repoVisita.GetFiltered(new Visita() { PlanId = planDetallado.Id, Activo = planDetallado.Activo.Value }, 
                    "Establecimiento,Empleado,Estado,TipoVisita").ToList();
                return mapper.Map<PlanDetalleDTO>(planDetallado);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Devuelve true si estan todas las visitas cerradas, caso contrario false
        /// </summary>
        /// <param name="idPlan"></param>
        /// <returns></returns>
        public bool DeterminarSiVisitasPlanEstanCerradas(int idPlan)
        {
            var visitas = this.repoVisita.GetFiltered(new Visita() { PlanId = idPlan });

            return !visitas.Any(v => v.EstadoId == 1 || v.EstadoId == 2);
        }

        public void CompletarPlan(int idPlan)
        {
            if (!this.DeterminarSiVisitasPlanEstanCerradas(idPlan)) throw new PlanAunTieneVisitasPendientesException();

            this.Modificar(new Plan() { Id = idPlan, EstadoId = 3 });

            //this.reportingService.GenerarReporteGeneralCliente(idPlan);
        }
    }
}
