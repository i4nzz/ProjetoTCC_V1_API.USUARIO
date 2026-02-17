using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Usuarios.API.Infra.Migrations
{
    /// <inheritdoc />
    public partial class newCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "usuario",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "SenhaHash",
                table: "usuario",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "SenhaHash",
                table: "usuario");
        }
    }
}
