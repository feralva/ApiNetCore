using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Establecimiento_Cliente_ClienteId",
                table: "Establecimiento");

            migrationBuilder.DropIndex(
                name: "IX_Establecimiento_ClienteId",
                table: "Establecimiento");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Establecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_Establecimiento_Cliente_FK",
                table: "Establecimiento",
                column: "Cliente_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_Establecimiento_Cliente_Cliente_FK",
                table: "Establecimiento",
                column: "Cliente_FK",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Establecimiento_Cliente_Cliente_FK",
                table: "Establecimiento");

            migrationBuilder.DropIndex(
                name: "IX_Establecimiento_Cliente_FK",
                table: "Establecimiento");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Establecimiento",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Establecimiento_ClienteId",
                table: "Establecimiento",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Establecimiento_Cliente_ClienteId",
                table: "Establecimiento",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
