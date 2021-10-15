using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class urlFoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "urlFoto",
                table: "Empleado",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "urlFoto",
                table: "Empleado");
        }
    }
}
