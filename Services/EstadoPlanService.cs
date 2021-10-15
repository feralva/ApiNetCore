using AutoMapper;
using Data.Entities;
using Data.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class EstadoPlanService : GenericService<EstadoPlan>
    {
        public EstadoPlanService(IGenericRepository<EstadoPlan> Repository, IMapper mapper, ILogger<GenericService<EstadoPlan>> Logger) : base(Repository, mapper, Logger)
        {
        }
    }
}
