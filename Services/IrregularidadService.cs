using AutoMapper;
using Data.Entities;
using Data.Repositories;
using Data.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using Model;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class IrregularidadService : GenericService<Irregularidad>
    {
        private IGenericService<Ubicacion> serviceUbicacion;
        private IGenericService<Establecimiento> serviceEstablecimiento;
        private IGenericService<Cliente> serviceCliente;
        public IrregularidadService(IGenericRepository<Irregularidad> Repository, IMapper mapper, ILogger<GenericService<Irregularidad>> Logger,
            IGenericService<Ubicacion> ServiceUbicacion, IGenericService<Establecimiento> ServiceEstablecimiento, IGenericService<Cliente> ServiceCliente) : base(Repository, mapper, Logger)
        {
            this.serviceUbicacion = ServiceUbicacion;
            this.serviceEstablecimiento = ServiceEstablecimiento;
            this.serviceCliente = ServiceCliente;
        }

        public IEnumerable<IrregularidadDTO> ObtenerIrregularidades(Irregularidad irregularidadParam, string propiedadesAIncluir = null)
        {
            try
            {
                var irregularidadesDTO = mapper.Map<IEnumerable<IrregularidadDTO>>(base.ObtenerListado(irregularidadParam, propiedadesAIncluir));

                foreach (var irregularidad in irregularidadesDTO)
                {
                    irregularidad.Ubicacion.Irregularidades = null;

                    var ubicacion = serviceUbicacion.ObtenerListado(new Ubicacion() { Id = irregularidad.UbicacionId }, "Establecimiento").FirstOrDefault();
                    var establecimiento = serviceEstablecimiento.ObtenerListado(new Establecimiento() { Id = ubicacion.EstablecimientoId }).FirstOrDefault();
                    var cliente = serviceCliente.ObtenerListado(new Cliente() { Id = establecimiento.ClienteId }).FirstOrDefault();
                    irregularidad.Establecimiento = establecimiento.Nombre;
                    irregularidad.Cliente = cliente.Nombre;
                }

                return irregularidadesDTO;

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }        
    }
}
