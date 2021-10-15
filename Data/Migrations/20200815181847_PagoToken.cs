using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class PagoToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TokenPago",
                table: "Pago",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TokenPago",
                table: "Pago");
        }
    }
}
