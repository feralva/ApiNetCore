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
    public class ClienteService: GenericService<Cliente>
    {
        public ClienteService(IGenericRepository<Cliente> Repository, IMapper mapper, ILogger<GenericService<Cliente>> Logger) : base(Repository, mapper, Logger)
        {
        }

        public Dictionary<string, int> ObtenerTotalizadoVisitasPorEstado(int idCliente)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (var totalizado in ((ClienteRepository)this.repository).ObtenerTotalizadoVisitasPorEstado(idCliente))
            {
                result.Add(totalizado.Estado, totalizado.Cantidad);
            }
            return result;
        }
    }
}
