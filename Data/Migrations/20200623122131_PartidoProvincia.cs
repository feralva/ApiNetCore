using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class PartidoProvincia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Partido",
                table: "Direccion");

            migrationBuilder.DropColumn(
                name: "Provincia",
                table: "Direccion");

            migrationBuilder.AddColumn<int>(
                name: "Partido_FK",
                table: "Direccion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Provincia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partido",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Provincia_FK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partido_Provincia_Provincia_FK",
                        column: x => x.Provincia_FK,
                        principalTable: "Provincia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Direccion_Partido_FK",
                table: "Direccion",
                column: "Partido_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Partido_Provincia_FK",
                table: "Partido",
                column: "Provincia_FK");

            migrationBuilder.AddForeignKey(
                name: "FK_Direccion_Partido_Partido_FK",
                table: "Direccion",
                column: "Partido_FK",
                principalTable: "Partido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Direccion_Partido_Partido_FK",
                table: "Direccion");

            migrationBuilder.DropTable(
                name: "Partido");

            migrationBuilder.DropTable(
                name: "Provincia");

            migrationBuilder.DropIndex(
                name: "IX_Direccion_Partido_FK",
                table: "Direccion");

            migrationBuilder.DropColumn(
                name: "Partido_FK",
                table: "Direccion");

            migrationBuilder.AddColumn<string>(
                name: "Partido",
                table: "Direccion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provincia",
                table: "Direccion",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
