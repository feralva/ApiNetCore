using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fkNullEmpleado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visita_Empleado_Empleado_FK",
                table: "Visita");

            migrationBuilder.AlterColumn<int>(
                name: "Empleado_FK",
                table: "Visita",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_Empleado_Empleado_FK",
                table: "Visita",
                column: "Empleado_FK",
                principalTable: "Empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visita_Empleado_Empleado_FK",
                table: "Visita");

            migrationBuilder.AlterColumn<int>(
                name: "Empleado_FK",
                table: "Visita",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_Empleado_Empleado_FK",
                table: "Visita",
                column: "Empleado_FK",
                principalTable: "Empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
