using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class PrecioLicencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Precio_Licencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Precio = table.Column<double>(nullable: false),
                    FechaDesde = table.Column<DateTime>(nullable: false),
                    TipoLicencia_FK = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Precio_Licencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Precio_Licencia_Tipo_Licencia_TipoLicencia_FK",
                        column: x => x.TipoLicencia_FK,
                        principalTable: "Tipo_Licencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Precio_Licencia_TipoLicencia_FK",
                table: "Precio_Licencia",
                column: "TipoLicencia_FK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Precio_Licencia");
        }
    }
}
