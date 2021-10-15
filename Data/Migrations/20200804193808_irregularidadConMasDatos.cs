using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class irregularidadConMasDatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cliente_FK",
                table: "Irregularidad",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Empleado_FK",
                table: "Irregularidad",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Empresa_FK",
                table: "Irregularidad",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Irregularidad_Cliente_FK",
                table: "Irregularidad",
                column: "Cliente_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Irregularidad_Empleado_FK",
                table: "Irregularidad",
                column: "Empleado_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Irregularidad_Empresa_FK",
                table: "Irregularidad",
                column: "Empresa_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_Irregularidad_Cliente_Cliente_FK",
                table: "Irregularidad",
                column: "Cliente_FK",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Irregularidad_Empleado_Empleado_FK",
                table: "Irregularidad",
                column: "Empleado_FK",
                principalTable: "Empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Irregularidad_Empresa_Empresa_FK",
                table: "Irregularidad",
                column: "Empresa_FK",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Irregularidad_Cliente_Cliente_FK",
                table: "Irregularidad");

            migrationBuilder.DropForeignKey(
                name: "FK_Irregularidad_Empleado_Empleado_FK",
                table: "Irregularidad");

            migrationBuilder.DropForeignKey(
                name: "FK_Irregularidad_Empresa_Empresa_FK",
                table: "Irregularidad");

            migrationBuilder.DropIndex(
                name: "IX_Irregularidad_Cliente_FK",
                table: "Irregularidad");

            migrationBuilder.DropIndex(
                name: "IX_Irregularidad_Empleado_FK",
                table: "Irregularidad");

            migrationBuilder.DropIndex(
                name: "IX_Irregularidad_Empresa_FK",
                table: "Irregularidad");

            migrationBuilder.DropColumn(
                name: "Cliente_FK",
                table: "Irregularidad");

            migrationBuilder.DropColumn(
                name: "Empleado_FK",
                table: "Irregularidad");

            migrationBuilder.DropColumn(
                name: "Empresa_FK",
                table: "Irregularidad");
        }
    }
}
