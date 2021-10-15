using AutoMapper;
using Data.Entities;
using Data.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class PartidoService : GenericService<Partido>
    {
        public PartidoService(IGenericRepository<Partido> Repository, IMapper mapper, ILogger<GenericService<Partido>> Logger) : base(Repository, mapper, Logger)
        {
        }


    }
}
