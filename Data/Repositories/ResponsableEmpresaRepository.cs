using Data.EF;
using Data.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class ResponsableEmpresaRepository : GenericRepository<ResponsableEmpresa>
    {
        public ResponsableEmpresaRepository(AplicationDbContext context, ILogger<GenericRepository<ResponsableEmpresa>> Logger) : base(context, Logger)
        {
        }

        protected override void CargarAtributosAModificar(ResponsableEmpresa editedEntity, ResponsableEmpresa entity)
        {
            editedEntity.Apellido = entity.Apellido;
            editedEntity.CorreoElectronico = entity.CorreoElectronico;
            editedEntity.Nombre = entity.Nombre;
            editedEntity.Telefono = entity.Telefono;
        }
    }
}
