using AutoMapper;
using Data.Entities;
using Data.Repositories;
using Data.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class DireccionService : GenericService<Direccion>
    {
        public DireccionService(IGenericRepository<Direccion> Repository, IMapper mapper, ILogger<GenericService<Direccion>> Logger) : base(Repository, mapper, Logger)
        {
        }
    
        public List<Partido> ObtenerPartidosProvincia(int idProvincia)
        {
            return ((DireccionRepository)repository).ObtenerPartidosProvincia(idProvincia);
        }

        public List<Provincia> ObtenerProvincias()
        {
            return ((DireccionRepository)repository).ObtenerProvincias();
        }
    }
}
