using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class latlng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitud",
                table: "Ubicacion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitud",
                table: "Ubicacion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitud",
                table: "Ubicacion");

            migrationBuilder.DropColumn(
                name: "Longitud",
                table: "Ubicacion");
        }
    }
}
