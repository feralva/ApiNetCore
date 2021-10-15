using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class equipoEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipo_Medicion_Empresa_EmpresaId",
                table: "Equipo_Medicion");

            migrationBuilder.RenameColumn(
                name: "EmpresaId",
                table: "Equipo_Medicion",
                newName: "Empresa_FK");

            migrationBuilder.RenameIndex(
                name: "IX_Equipo_Medicion_EmpresaId",
                table: "Equipo_Medicion",
                newName: "IX_Equipo_Medicion_Empresa_FK");

            migrationBuilder.AlterColumn<int>(
                name: "Empresa_FK",
                table: "Equipo_Medicion",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipo_Medicion_Empresa_Empresa_FK",
                table: "Equipo_Medicion",
                column: "Empresa_FK",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipo_Medicion_Empresa_Empresa_FK",
                table: "Equipo_Medicion");

            migrationBuilder.RenameColumn(
                name: "Empresa_FK",
                table: "Equipo_Medicion",
                newName: "EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_Equipo_Medicion_Empresa_FK",
                table: "Equipo_Medicion",
                newName: "IX_Equipo_Medicion_EmpresaId");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Equipo_Medicion",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Equipo_Medicion_Empresa_EmpresaId",
                table: "Equipo_Medicion",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
