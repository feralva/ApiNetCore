using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AnioVisita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Anio_Pactado",
                table: "Visita",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anio_Pactado",
                table: "Visita");
        }
    }
}
