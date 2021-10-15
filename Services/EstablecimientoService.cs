using AutoMapper;
using Data.Entities;
using Data.Repositories;
using Data.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Model.CaminoOptimo;
using Model.Establecimiento;
using Newtonsoft.Json;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EstablecimientoService : GenericService<Establecimiento>
    {
        private readonly IConfiguration _config;
        IGenericService<Partido> servicePartido;
        IGenericRepository<Ubicacion> repoUbicacion;
        public EstablecimientoService(IGenericRepository<Establecimiento> Repository, IMapper mapper, ILogger<GenericService<Establecimiento>> Logger,
            IGenericService<Partido> ServicePartido, IGenericRepository<Ubicacion> RepoUbicacion, IConfiguration config) : base(Repository, mapper, Logger)
        {
            this.servicePartido = ServicePartido;
            this.repoUbicacion = RepoUbicacion;
            _config = config;
        }

        public List<EstablecimientoSummaryDTO> ObtenerEstablecimientosCliente(Establecimiento establecimiento)
        {
            try
            {
                var establecimientos = ((EstablecimientoRepository)this.repository).ObtenerEstablecimientosPorCliente(establecimiento);

                foreach (var item in establecimientos)
                {
                    item.Direccion.Partido = servicePartido
                    .ObtenerListado(new Partido() { Id = item.Direccion.PartidoId }, "Provincia").FirstOrDefault();

                    item.Direccion.Partido.Provincia.Partidos = null;
                }
                
                return this.mapper.Map<List<EstablecimientoSummaryDTO>>(establecimientos);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<List<Tramo>> ObtenerCaminoOptimo(int idEstablecimiento)
        {
            var establecimiento = this.ObtenerListado(new Establecimiento() { Id = idEstablecimiento }).FirstOrDefault();

            List<Location> destinos = this.obtenerUbicacionesLocations(idEstablecimiento);

            var tramos = this.ObtenerTramos(new Location() { Id = establecimiento.Id, 
                                                            Latitud = double.Parse(establecimiento.Latitud, CultureInfo.InvariantCulture),
                                                            Longitud = double.Parse(establecimiento.Longitud, CultureInfo.InvariantCulture),
                                                            Nombre = establecimiento.Nombre
                                                            }, destinos);
            int cantidadLocations = destinos.Count + 1;

            #region generacion Diccionario Ciudades
            Dictionary<int, int> diccionarioCiudades = new Dictionary<int, int>();

            int loop = 0;
            foreach (var idLocation in tramos.Select(t => t.UbicacionOrigen.Id).Distinct())
            {
                diccionarioCiudades.Add(loop, idLocation);
                loop++;
            }
            #endregion

            double[,] matrizAdyacencia = this.ObtenerMatrizDeAdyacencia(tramos, cantidadLocations, diccionarioCiudades);

            var json = JsonConvert.SerializeObject(matrizAdyacencia);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            List<int> listaRecorridoLocations = new List<int>();

            //Obtener Recorrido Optimo
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync(_config.GetValue<string>(
                "RutasFunciones:RutaObtenerCaminoOptimo"), data))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    var recorridoOptimo = JsonConvert.DeserializeObject<List<int>>(apiResponse);

                    foreach (var refLocation in recorridoOptimo)
                    {
                        if (diccionarioCiudades.GetValueOrDefault(refLocation) == idEstablecimiento)
                        {
                            listaRecorridoLocations.Add(idEstablecimiento);
                        }
                        else
                        {
                            listaRecorridoLocations.Add(destinos.Select(d => d.Id).Where(d => d ==
                                    diccionarioCiudades.GetValueOrDefault(refLocation)).First());
                        }

                    }
                }
            }

            List<Tramo> tramosAUsar = new List<Tramo>();

            int posicion = 1;
            foreach (var location in listaRecorridoLocations)
            {
                if (posicion < listaRecorridoLocations.Count)
                {
                    tramosAUsar.Add(tramos.Where(t => t.UbicacionOrigen.Id == listaRecorridoLocations.ElementAt(posicion - 1)
                                    && t.UbicacionDestino.Id == listaRecorridoLocations.ElementAt(posicion)).First());

                }

                posicion++;
            }

            return tramosAUsar;
        }

        private List<Location> obtenerUbicacionesLocations(int idEstablecimiento)
        {
            List<Location> locations = new List<Location>();
            var ubicaciones = this.repoUbicacion.GetFiltered(new Ubicacion() { EstablecimientoId = idEstablecimiento });

            foreach (var ubicacion in ubicaciones)
            {
                locations.Add(new Location()
                {
                    Id = ubicacion.Id,
                    Latitud = double.Parse(ubicacion.Latitud, CultureInfo.InvariantCulture),
                    Longitud = double.Parse(ubicacion.Longitud, CultureInfo.InvariantCulture),
                    Nombre = ubicacion.Nombre
                });
            }
            return locations;
        }

        private List<Tramo> ObtenerTramos(Location locationOrigen, List<Location> locationsDestino)
        {
            List<Location> aux = new List<Location>();
            aux.Add(locationOrigen);
            aux.AddRange(locationsDestino);

            List<Tramo> resultado = new List<Tramo>();

            foreach (var origen in aux)
            {
                foreach (var destino in aux)
                {
                    resultado.Add(new Tramo() { 
                        UbicacionOrigen = origen, 
                        UbicacionDestino = destino,
                        Distancia = CalcularDistanciaLocations(origen.Latitud, origen.Longitud,
                                                                destino.Latitud, destino.Longitud)
                    });
                }
            }

            return resultado;
        }

        private int CalcularDistanciaLocations(double lat1, double lon1, double lat2, double lon2)
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515 * 1000;

            return (int)dist;
        }

        private double[,] ObtenerMatrizDeAdyacencia(List<Tramo> tramos, int cantidadLocations, Dictionary<int, int> diccionarioLocations)
        {

            double[,] matrizAdyacencia = new double[cantidadLocations, cantidadLocations];

            for (int i = 0; i < cantidadLocations; i++)
            {
                for (int j = 0; j < cantidadLocations; j++)
                {
                    matrizAdyacencia[i, j] = tramos.Where(t => t.UbicacionOrigen.Id == diccionarioLocations.GetValueOrDefault(i)
                                                             && t.UbicacionDestino.Id == diccionarioLocations.GetValueOrDefault(j))
                                                                .First().Distancia;
                }
            }

            return matrizAdyacencia;
        }
    }
}
