using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca_Virtual.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarModeloLibro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Archivo",
                table: "Libros",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutaArchivoPDF",
                table: "Libros",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archivo",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "RutaArchivoPDF",
                table: "Libros");
        }
    }
}
