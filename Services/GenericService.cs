using AutoMapper;
using Data.Entities;
using Data.Repositories.Contracts;
using Exceptions;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseEntity
    {
        protected IGenericRepository<T> repository;
        protected IMapper mapper;
        protected ILogger<GenericService<T>> logger;
        public GenericService(IGenericRepository<T> Repository, IMapper mapper, ILogger<GenericService<T>> Logger)
        {
            this.repository = Repository;
            this.mapper = mapper;
            logger = Logger;
        }

        public virtual int Alta(T entidad)
        {
            try
            {
                return this.repository.Insert(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public virtual void Borrar(T entidad)
        {
            try
            {
                this.repository.Delete(entidad);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
            
        }

        public virtual void Modificar(T entidad, string propiedadesAIncluir = "")
        {            
            try
            {
                this.repository.Update(entidad, propiedadesAIncluir);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }


        public virtual IEnumerable<T> ObtenerListado(T parametros=null, string propiedadesAIncluir = null)
        {
            try
            {
                return this.repository.GetFiltered(parametros, propiedadesAIncluir);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }

        public virtual T ObtenerPorId(int id)
        {            
            try
            {
                return this.repository.Get(id);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
