using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class PlanEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Empresa_FK",
                table: "Plan",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Plan_Empresa_FK",
                table: "Plan",
                column: "Empresa_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_Plan_Empresa_Empresa_FK",
                table: "Plan",
                column: "Empresa_FK",
                principalTable: "Empresa",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plan_Empresa_Empresa_FK",
                table: "Plan");

            migrationBuilder.DropIndex(
                name: "IX_Plan_Empresa_FK",
                table: "Plan");

            migrationBuilder.DropColumn(
                name: "Empresa_FK",
                table: "Plan");
        }
    }
}
