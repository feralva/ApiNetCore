using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class spObtenerVisitasPorEstadoEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE dbo.ObtenerVisitasPorEstadoEmpresa @idEmpresa int
                            AS
                            BEGIN
                                select isNull(estado.Descripcion, 'NA') Estado, COUNT(1) Cantidad from Cliente 
                                left JOIN [Plan] p on p.Cliente_FK = Cliente.Id
                                left join Visita v on p.Id = v.Plan_FK
                                left JOIN EstadoVisita estado on estado.Id = v.Estado_FK
                                WHERE cliente.Empresa_FK = @idEmpresa
                                GROUP BY estado.Descripcion
                            END";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
