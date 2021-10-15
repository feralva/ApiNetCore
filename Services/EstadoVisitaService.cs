using AutoMapper;
using Data.Entities;
using Data.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class EstadoVisitaService : GenericService<EstadoVisita>
    {
        public EstadoVisitaService(IGenericRepository<EstadoVisita> Repository, IMapper mapper, ILogger<GenericService<EstadoVisita>> Logger) : base(Repository, mapper, Logger)
        {
        }
    }
}
