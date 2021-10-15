using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class irregularidad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evicencia");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Irregularidad",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Irregularidad");

            migrationBuilder.CreateTable(
                name: "Evicencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IrregularidadId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Evicencia_IrregularidadId",
                table: "Evicencia",
                column: "IrregularidadId");
        }
    }
}
