using Data.EF;
using Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class PrecioLicenciaRepository : GenericRepository<PrecioLicencia>
    {
        public PrecioLicenciaRepository(AplicationDbContext context, ILogger<GenericRepository<PrecioLicencia>> Logger) : base(context, Logger)
        {
        }


    }
}
