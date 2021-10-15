using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class estadoVisita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado_FK",
                table: "Visita",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EstadoVisita",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoVisita", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visita_Estado_FK",
                table: "Visita",
                column: "Estado_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_Visita_EstadoVisita_Estado_FK",
                table: "Visita",
                column: "Estado_FK",
                principalTable: "EstadoVisita",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visita_EstadoVisita_Estado_FK",
                table: "Visita");

            migrationBuilder.DropTable(
                name: "EstadoVisita");

            migrationBuilder.DropIndex(
                name: "IX_Visita_Estado_FK",
                table: "Visita");

            migrationBuilder.DropColumn(
                name: "Estado_FK",
                table: "Visita");
        }
    }
}
