using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IGenericService<T> where T: Data.Entities.BaseEntity
    {
        int Alta(T entidad);
        void Borrar(T entidad);
        void Modificar(T entidad, string propiedadesAIncluir = "");
        IEnumerable<T> ObtenerListado(T parametros = null, string propiedadesAIncluir = null);
        T ObtenerPorId(int id);
    }
}