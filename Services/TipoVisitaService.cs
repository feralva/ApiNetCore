using AutoMapper;
using Data.Entities;
using Data.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class TipoVisitaService : GenericService<TipoVisita>
    {
        public TipoVisitaService(IGenericRepository<TipoVisita> Repository, IMapper mapper, ILogger<GenericService<TipoVisita>> Logger) : base(Repository, mapper, Logger)
        {
        }
    }
}
