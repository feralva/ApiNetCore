using Data.EF;
using Data.Entities;
using Data.Repositories.Contracts;
using Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Repositories
{
    public class GenericRepository<T> :  IGenericRepository<T> where T : BaseEntity
    {
        protected ILogger<GenericRepository<T>> logger;
        protected readonly AplicationDbContext context;
        protected DbSet<T> entities;

        public GenericRepository(AplicationDbContext context, ILogger<GenericRepository<T>> Logger)
        {
            this.context = context;
            entities = context.Set<T>();
            this.logger = Logger;
        }
        
        public virtual T Get(int id)
        {
            try
            {
                return entities.SingleOrDefault(s => s.Id == id);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }

        }
        public virtual int Insert(T entity)
        {
            try
            {
                entities.Add(entity);
                context.SaveChanges();
                return entity.Id;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
            
        }
        public virtual void Update(T entity,string propiedadesAIncluir = "")
        {
            
            try
            {
                context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                IQueryable<T> query = context.Set<T>();

                foreach (var includeProperty in propiedadesAIncluir.Split
                            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                var editedEntity = query.FirstOrDefault(e => e.Id == entity.Id);
                CargarAtributosAModificar(editedEntity, entity);
                this.context.Entry(editedEntity).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
            
        }

        protected virtual void CargarAtributosAModificar(T editedEntity, T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {
            try
            {
                entities.Remove(entity);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
            
        }

        /// <summary>
        /// Metodo generico, que a aplica una Expresion de filtrado a un Iquerable
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected virtual IQueryable<T> FiltrarPor(IQueryable<T> query, Expression<Func<T, bool>> filter = null)
        {
            //IQueryable<TEntity> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }
        public virtual IEnumerable<T> GetFiltered(T parametros = null, string includeProperties = null)
        {
            try
            {
                includeProperties = includeProperties ?? string.Empty;
                //agrego el .AsNoTracking() para que no trackee las entidades
                IQueryable<T> query = context.Set<T>().AsNoTracking();

                foreach (var includeProperty in includeProperties.Split
                            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (parametros != null)
                {
                    return AplicarFiltrado(query, parametros);
                }
                else
                {
                    return query.AsEnumerable();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
        }
        protected virtual IEnumerable<T> AplicarFiltrado(IQueryable<T> queryable, T parametros)
        {
            throw new NotImplementedException();
        }
    }
}
