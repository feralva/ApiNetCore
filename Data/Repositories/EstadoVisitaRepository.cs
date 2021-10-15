using Data.EF;
using Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class EstadoVisitaRepository : GenericRepository<EstadoVisita>
    {
        public EstadoVisitaRepository(AplicationDbContext context, ILogger<GenericRepository<EstadoVisita>> Logger) : base(context, Logger)
        {
        }


    }
}
