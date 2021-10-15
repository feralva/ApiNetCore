using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class CampoActivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Visita",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Plan",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Establecimiento",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Equipo_Medicion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Empresa",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Empleado",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Cliente",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Visita");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Plan");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Establecimiento");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Equipo_Medicion");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Empleado");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Cliente");
        }
    }
}
