using AutoMapper;
using Data.Entities;
using Data.Repositories.Contracts;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class LicenciaService : GenericService<Licencia>
    {
        IGenericService<TasaConversionTipoLicencia> servicioTasaConversion;
        public LicenciaService(IGenericRepository<Licencia> Repository, IMapper mapper, ILogger<GenericService<Licencia>> Logger,
            IGenericService<TasaConversionTipoLicencia> ServicioTasaConversion) : base(Repository, mapper, Logger)
        {
            this.servicioTasaConversion = ServicioTasaConversion;
        }

        public void ProcesarAdquisicionLicencia(Licencia licencia, int cantidadMeses)
        {
            try
            {
                var licenciaActual = this.ObtenerListado(new Licencia() { EmpresaId = licencia.EmpresaId }, "TipoLicencia").FirstOrDefault();

                if (licenciaActual != null && !licenciaActual.TipoLicencia.Descripcion.Contains("Prueba"))
                {
                    //El tipo Licencia 5 es la de prueba
                    if (licenciaActual.TipoLicenciaId != licencia.TipoLicenciaId && licenciaActual.TipoLicenciaId !=5)
                    {
                        var ratioConversion = this.servicioTasaConversion.ObtenerListado(
                            new TasaConversionTipoLicencia()
                            {
                                TipoLicenciaDestinoId = licencia.TipoLicenciaId,
                                TipoLicenciaOrigenId = licenciaActual.TipoLicenciaId
                            }).FirstOrDefault().RatioConversion;
                        licenciaActual.TipoLicenciaId = licencia.TipoLicenciaId;
                        licenciaActual.FechaFin = CalcularFechaFinEnBaseRatio(licenciaActual.FechaFin.Value, ratioConversion).AddDays(cantidadMeses * 30);
                    }
                    else
                    {
                        licenciaActual.TipoLicenciaId = licencia.TipoLicenciaId;
                        licenciaActual.FechaFin = licenciaActual.FechaFin.Value.AddDays(cantidadMeses * 30);
                    }

                    this.Modificar(licenciaActual);
                }
                else
                {
                    this.Alta(licencia);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                throw ex;
            }

        }

        private DateTime CalcularFechaFinEnBaseRatio(DateTime fechaFinActual, double ratioConversion)
        {
            var cantidadDiasrestantes = (fechaFinActual - DateTime.Now).TotalDays;

            var cantidadRestantesEnNuevoTipoLicencia = cantidadDiasrestantes * ratioConversion;

            return DateTime.Now.AddDays(cantidadRestantesEnNuevoTipoLicencia);
        }
    }
}
