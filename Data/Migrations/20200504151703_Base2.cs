using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Base2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleado_Usuario_UsuarioIdUsuario",
                table: "Empleado");

            migrationBuilder.DropIndex(
                name: "IX_Empleado_UsuarioIdUsuario",
                table: "Empleado");

            migrationBuilder.DropColumn(
                name: "UsuarioIdUsuario",
                table: "Empleado");

            migrationBuilder.AddColumn<double>(
                name: "Monto",
                table: "Pago",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Establecimiento",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Usuario_FK",
                table: "Empleado",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_Usuario_FK",
                table: "Empleado",
                column: "Usuario_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleado_Usuario_Usuario_FK",
                table: "Empleado",
                column: "Usuario_FK",
                principalSchema: "Seguridad",
                principalTable: "Usuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleado_Usuario_Usuario_FK",
                table: "Empleado");

            migrationBuilder.DropIndex(
                name: "IX_Empleado_Usuario_FK",
                table: "Empleado");

            migrationBuilder.DropColumn(
                name: "Monto",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Establecimiento");

            migrationBuilder.AlterColumn<int>(
                name: "Usuario_FK",
                table: "Empleado",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioIdUsuario",
                table: "Empleado",
                type: "varchar(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Empleado_UsuarioIdUsuario",
                table: "Empleado",
                column: "UsuarioIdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleado_Usuario_UsuarioIdUsuario",
                table: "Empleado",
                column: "UsuarioIdUsuario",
                principalSchema: "Seguridad",
                principalTable: "Usuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
