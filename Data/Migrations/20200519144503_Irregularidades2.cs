using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Irregularidades2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "Irregularidad",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tipo_FK",
                table: "Irregularidad",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Evicencia",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    IrregularidadId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evicencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evicencia_Irregularidad_IrregularidadId",
                        column: x => x.IrregularidadId,
                        principalTable: "Irregularidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Irregularidad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Irregularidad", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Irregularidad_Tipo_FK",
                table: "Irregularidad",
                column: "Tipo_FK");

            migrationBuilder.CreateIndex(
                name: "IX_Evicencia_IrregularidadId",
                table: "Evicencia",
                column: "IrregularidadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Irregularidad_Tipo_Irregularidad_Tipo_FK",
                table: "Irregularidad",
                column: "Tipo_FK",
                principalTable: "Tipo_Irregularidad",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Irregularidad_Tipo_Irregularidad_Tipo_FK",
                table: "Irregularidad");

            migrationBuilder.DropTable(
                name: "Evicencia");

            migrationBuilder.DropTable(
                name: "Tipo_Irregularidad");

            migrationBuilder.DropIndex(
                name: "IX_Irregularidad_Tipo_FK",
                table: "Irregularidad");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "Irregularidad");

            migrationBuilder.DropColumn(
                name: "Tipo_FK",
                table: "Irregularidad");
        }
    }
}
