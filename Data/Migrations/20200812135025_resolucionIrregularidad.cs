using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class resolucionIrregularidad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescripcionResolucion",
                table: "Irregularidad",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescripcionResolucion",
                table: "Irregularidad");
        }
    }
}
