using AutoMapper;
using Data.Entities;
using Data.Entities.EntidadesNoPersistidas;
using Data.Repositories;
using Data.Repositories.Contracts;
using Exceptions;
using Microsoft.Extensions.Logging;
using Model.PlanModels;
using Model.Visita;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class VisitaService : GenericService<Visita>
    {
        protected IGenericRepository<Cliente> clienteRepository;
        public VisitaService(IGenericRepository<Visita> Repository, IMapper mapper, ILogger<GenericService<Visita>> Logger,
            IGenericRepository<Cliente> ClienteRepository) : base(Repository, mapper, Logger)
        {
            this.clienteRepository = ClienteRepository;
        }

        public List<VisitaSummaryDTO> ObtenerPlanesVisitasEmpresa(int idEmpresa, bool activo, int estadoVisitaId, int clienteId)
        {
            try
            {
                var visitas = ((VisitaRepository) this.repository).ObtenerPlanesPorEmpresa(idEmpresa, activo, estadoVisitaId, clienteId);

                return this.mapper.Map<List<VisitaSummaryDTO>>(visitas.OrderBy(v => v.Fecha).OrderBy(v => v.FechaPactada));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }        
        public List<VisitaSummaryDTO> ObtenerVisitasEmpleado(Visita visita)
        {
            try
            {
                var visitas =this.repository.GetFiltered(visita, "Establecimiento,TipoVisita,Empleado,Estado");

                foreach (Visita unaVisita in visitas)
                {
                    unaVisita.Establecimiento.Cliente = this.clienteRepository.Get(unaVisita.Establecimiento.ClienteId);
                }

                return this.mapper.Map<List<VisitaSummaryDTO>>(visitas.OrderBy(v => v.Fecha));
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }

        public void ReasignarVisita(Visita visita)
        {
            try
            {
                if(!EmpleadoDisponible(visita.EmpleadoId, visita.Fecha, visita.Id)) throw new EmpleadoNoDisponibleException();           
                
                ((VisitaRepository)this.repository).ReasignarVisita(visita);
             
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }

        private bool EmpleadoDisponible(int? empleadoId, DateTime? fecha, int idVisita)
        {
            if (fecha == null) return true;
            if (empleadoId == null) return true;

            var visitasEmpledoDiaEspecifico = this.ObtenerListado(new Visita() { EmpleadoId = empleadoId, Fecha = fecha, EstadoId = 1 })
                .Where(v => v.Id != idVisita);

            var visitasEnConflicto = visitasEmpledoDiaEspecifico.Where(v =>
                        //para nueva fecha inicio
                      ((v.Fecha.Value <= fecha && v.Fecha.Value.AddMinutes(v.Duracion.Value) > fecha)
                        ||
                        //para nueva fecha fin
                      (fecha.Value.AddMinutes(v.Duracion.Value) <= v.Fecha.Value.AddMinutes(v.Duracion.Value) &&
                      (fecha.Value.AddMinutes(v.Duracion.Value) > v.Fecha.Value))));

            return (visitasEnConflicto.Count() == 0);
        }

        public void CambiarFechaVisita(Visita visita)
        {
            try
            {
                if (!EmpleadoDisponible(visita.EmpleadoId, visita.Fecha, visita.Id)) throw new EmpleadoNoDisponibleException();

                ((VisitaRepository)this.repository).CambiarFechaVisita(visita);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }        
        public void CompletarVisita(Visita visita)
        {
            try
            {
                ((VisitaRepository)this.repository).CompletarVisita(visita);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }        
        public void CancelarVisita(Visita visita)
        {
            try
            {
                if (visita.EstadoId == 3)
                {
                    throw new VisitaCompletaNoPuedeCancelarseException();
                }
                ((VisitaRepository)this.repository).CancelarVisita(visita);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
