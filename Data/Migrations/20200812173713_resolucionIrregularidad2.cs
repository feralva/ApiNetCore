using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class resolucionIrregularidad2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlEvidenciaResultado",
                table: "Irregularidad",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlEvidenciaResultado",
                table: "Irregularidad");
        }
    }
}
