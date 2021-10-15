using AutoMapper;
using Data.Entities;
using Data.Repositories;
using Data.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using Model;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Services
{
    public class EquipoMedicionService : GenericService<EquipoMedicion>
    {
        public EquipoMedicionService(IGenericRepository<EquipoMedicion> Repository, IMapper mapper,
            ILogger<GenericService<EquipoMedicion>> Logger) : base(Repository, mapper, Logger)
        {
        }

        public IEnumerable<EquipoMedicionTotalizadoDTO> ObtenerEquiposMedicionTotalizados(EquipoMedicion equipoMedicion)
        {
            var repoEquipoMedicion = (EquipoMedicionRepository)this.repository;

           return mapper.Map<List<EquipoMedicionTotalizadoDTO>>(repoEquipoMedicion.ObtenerEquiposMedicionTotalizados(equipoMedicion));
        }

        public void ActualizarCantidadEquiposMedicionEmpresa(int idEmpresa, string equipoMedicionNombre, int deltaCantidad)
        {

            var tipoEquipo = this.ObtenerListado(new EquipoMedicion() { Nombre = equipoMedicionNombre }).FirstOrDefault().TipoEquipoMedicionId;
            
            for (int i = 0; i < deltaCantidad; i++)
            {
                this.Alta(new EquipoMedicion()
                {
                    Activo = true,
                    Nombre = equipoMedicionNombre,
                    EmpresaId = idEmpresa,
                    TipoEquipoMedicionId = tipoEquipo
                }); 
            }
        }
    }
}
