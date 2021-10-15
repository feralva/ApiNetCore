using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ClienteEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Empresa_EmpresaId",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "EmpresaId",
                table: "Cliente",
                newName: "Empresa_FK");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_EmpresaId",
                table: "Cliente",
                newName: "IX_Cliente_Empresa_FK");

            migrationBuilder.AlterColumn<int>(
                name: "Empresa_FK",
                table: "Cliente",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Empresa_Empresa_FK",
                table: "Cliente",
                column: "Empresa_FK",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Empresa_Empresa_FK",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "Empresa_FK",
                table: "Cliente",
                newName: "EmpresaId");

            migrationBuilder.RenameIndex(
                name: "IX_Cliente_Empresa_FK",
                table: "Cliente",
                newName: "IX_Cliente_EmpresaId");

            migrationBuilder.AlterColumn<int>(
                name: "EmpresaId",
                table: "Cliente",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Empresa_EmpresaId",
                table: "Cliente",
                column: "EmpresaId",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
