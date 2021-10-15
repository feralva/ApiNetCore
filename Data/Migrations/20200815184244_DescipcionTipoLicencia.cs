using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class DescipcionTipoLicencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Tipo_Licencia",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Tipo_Licencia");
        }
    }
}
