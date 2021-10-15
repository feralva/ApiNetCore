using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class EstablecimientoCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cliente_FK",
                table: "Establecimiento",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cliente_FK",
                table: "Establecimiento");
        }
    }
}
