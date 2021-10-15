using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class FechaIrregularidad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaDeteccion",
                table: "Irregularidad",
                nullable: false,
                defaultValue: DateTime.Now);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFinalizado",
                table: "Irregularidad",
                nullable: true,
                defaultValue:null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaDeteccion",
                table: "Irregularidad");

            migrationBuilder.DropColumn(
                name: "FechaFinalizado",
                table: "Irregularidad");
        }
    }
}
