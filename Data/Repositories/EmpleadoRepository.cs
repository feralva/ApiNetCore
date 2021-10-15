using Data.EF;
using Data.Entities;
using Data.Repositories.Contracts;
using Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class EmpleadoRepository : GenericRepository<Empleado>
    {
        ISeguridadRepository seguridadRepository;
        public EmpleadoRepository(AplicationDbContext context, ILogger<EmpleadoRepository> Logger,
            ISeguridadRepository SeguridadRepository) : base(context, Logger)
        {
            this.seguridadRepository = SeguridadRepository;
        }
        protected override IEnumerable<Empleado> AplicarFiltrado(IQueryable<Empleado> entities, Empleado parameters)
        {
            try
            {
                if (parameters.EmpresaId != 0)
                {
                    entities = FiltrarPor(entities, x => x.EmpresaId.Equals(parameters.EmpresaId));
                }
                
                if (parameters.Id != 0)
                {
                    entities = FiltrarPor(entities, x => x.Id.Equals(parameters.Id));
                }          
                
                if (parameters.Activo != null)
                {
                    entities = FiltrarPor(entities, x => x.Activo.Equals(parameters.Activo));
                }

                return entities.ToList();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw new BaseDeDatosExcepcion();
            }
        }
        protected override void CargarAtributosAModificar(Empleado editedEntity, Empleado entity)
        {
            editedEntity.Nombre = entity.Nombre;
            editedEntity.Apellido = entity.Apellido;
            editedEntity.urlFoto = entity.urlFoto;
            editedEntity.Activo = entity.Activo;

            if (entity.Usuario != null)
            {
                //hijos
                this.seguridadRepository.ActualizarRolesUsuario(entity.Usuario.IdUsuario, entity.Usuario.UsuarioFamilia);
            }


        }
    }
}
