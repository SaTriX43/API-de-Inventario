using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_de_Inventario.Migrations
{
    /// <inheritdoc />
    public partial class agregeNameEnUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Usuarios");
        }
    }
}
