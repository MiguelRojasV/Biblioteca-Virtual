using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca_Virtual.Migrations
{
    /// <inheritdoc />
    public partial class migracioncuatro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdUsuario",
                table: "Comentarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Comentarios");
        }
    }
}
