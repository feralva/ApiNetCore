using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetFiltered(T parametros = null, string includeProperties = null);
        T Get(int id);
        int Insert(T entity);
        void Update(T entity, string propiedadesAIncluir = "");
        void Delete(T entity);
    }

}
