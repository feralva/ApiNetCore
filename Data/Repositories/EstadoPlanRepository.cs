using Data.EF;
using Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class EstadoPlanRepository : GenericRepository<EstadoPlan>
    {
        public EstadoPlanRepository(AplicationDbContext context, ILogger<GenericRepository<EstadoPlan>> Logger) : base(context, Logger)
        {
        }
    }
}
