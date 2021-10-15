using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class tipoLicenciaActivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Tipo_Licencia",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Tipo_Licencia");
        }
    }
}
