using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Irregularidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estado_Irregularidad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado_Irregularidad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Irregularidad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ubicacion_FK = table.Column<int>(nullable: false),
                    Estado_FK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Irregularidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Irregularidad_Estado_Irregularidad_Estado_FK",
                        column: x => x.Estado_FK,
                        principalTable: "Estado_Irregularidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Irregularidad_Ubicacion_Ubicacion_FK",
                        column: x => x.Ubicacion_FK,
                        principalTable: "Ubicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Irregularidad_Estado_FK",
                table: "Irregularidad",
                column: "Estado_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Irregularidad_Ubicacion_FK",
                table: "Irregularidad",
                column: "Ubicacion_FK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Irregularidad");

            migrationBuilder.DropTable(
                name: "Estado_Irregularidad");
        }
    }
}
