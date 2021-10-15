using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class urlFoto2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlFoto",
                table: "Empresa",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlFoto",
                table: "Cliente",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlFoto",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "UrlFoto",
                table: "Cliente");
        }
    }
}
