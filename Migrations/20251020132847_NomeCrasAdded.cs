using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SisCras.Migrations
{
    /// <inheritdoc />
    public partial class NomeCrasAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Cras",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Cras");
        }
    }
}
