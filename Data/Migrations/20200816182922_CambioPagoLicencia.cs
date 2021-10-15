using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class CambioPagoLicencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licencia_Pago_Pago_FK",
                table: "Licencia");

            migrationBuilder.DropIndex(
                name: "IX_Licencia_Pago_FK",
                table: "Licencia");

            migrationBuilder.DropColumn(
                name: "Pago_FK",
                table: "Licencia");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad_Meses",
                table: "Pago",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PrecioLicencia",
                table: "Pago",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Tipo_Licencia_FK",
                table: "Pago",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pago_Tipo_Licencia_FK",
                table: "Pago",
                column: "Tipo_Licencia_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_Pago_Tipo_Licencia_Tipo_Licencia_FK",
                table: "Pago",
                column: "Tipo_Licencia_FK",
                principalTable: "Tipo_Licencia",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pago_Tipo_Licencia_Tipo_Licencia_FK",
                table: "Pago");

            migrationBuilder.DropIndex(
                name: "IX_Pago_Tipo_Licencia_FK",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "Cantidad_Meses",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "PrecioLicencia",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "Tipo_Licencia_FK",
                table: "Pago");

            migrationBuilder.AddColumn<int>(
                name: "Pago_FK",
                table: "Licencia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Licencia_Pago_FK",
                table: "Licencia",
                column: "Pago_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_Licencia_Pago_Pago_FK",
                table: "Licencia",
                column: "Pago_FK",
                principalTable: "Pago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
