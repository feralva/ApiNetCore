using Data.EF;
using Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class TipoVisitaRepository : GenericRepository<TipoVisita>
    {
        public TipoVisitaRepository(AplicationDbContext context, ILogger<GenericRepository<TipoVisita>> Logger) : base(context, Logger)
        {
        }
    }
}
