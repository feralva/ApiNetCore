using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class latLongEstablecimiento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitud",
                table: "Establecimiento",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitud",
                table: "Establecimiento",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Latitud",
                table: "Establecimiento");

            migrationBuilder.DropColumn(
                name: "Longitud",
                table: "Establecimiento");
        }
    }
}
