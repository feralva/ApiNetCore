using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class TasaConversionTipoLicencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasa_Conversion_Licencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo_Licencia_Origen_FK = table.Column<int>(nullable: false),
                    Tipo_Licencia_Destino_FK = table.Column<int>(nullable: false),
                    RatioConversion = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasa_Conversion_Licencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasa_Conversion_Licencia_Tipo_Licencia_Tipo_Licencia_Destino_FK",
                        column: x => x.Tipo_Licencia_Destino_FK,
                        principalTable: "Tipo_Licencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Tasa_Conversion_Licencia_Tipo_Licencia_Tipo_Licencia_Origen_FK",
                        column: x => x.Tipo_Licencia_Origen_FK,
                        principalTable: "Tipo_Licencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasa_Conversion_Licencia_Tipo_Licencia_Destino_FK",
                table: "Tasa_Conversion_Licencia",
                column: "Tipo_Licencia_Destino_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Tasa_Conversion_Licencia_Tipo_Licencia_Origen_FK",
                table: "Tasa_Conversion_Licencia",
                column: "Tipo_Licencia_Origen_FK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasa_Conversion_Licencia");
        }
    }
}
