using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SisCras.Migrations
{
    /// <inheritdoc />
    public partial class FamiliaAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Familias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Familias", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Prontuarios_Familias_Id",
                table: "Prontuarios",
                column: "Id",
                principalTable: "Familias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prontuarios_Familias_Id",
                table: "Prontuarios");

            migrationBuilder.DropTable(
                name: "Familias");
        }
    }
}
