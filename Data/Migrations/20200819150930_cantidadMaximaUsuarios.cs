using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class cantidadMaximaUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cantidad_Maxima_Usuarios",
                table: "Tipo_Licencia",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cantidad_Maxima_Usuarios",
                table: "Tipo_Licencia",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
