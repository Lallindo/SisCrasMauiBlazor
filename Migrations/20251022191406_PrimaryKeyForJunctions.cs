using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SisCras.Migrations
{
    /// <inheritdoc />
    public partial class PrimaryKeyForJunctions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TecnicoCras",
                table: "TecnicoCras");

            migrationBuilder.DropIndex(
                name: "IX_TecnicoCras_TecnicoId",
                table: "TecnicoCras");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TecnicoCras",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TecnicoCras",
                table: "TecnicoCras",
                columns: new[] { "TecnicoId", "CrasId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TecnicoCras",
                table: "TecnicoCras");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TecnicoCras",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TecnicoCras",
                table: "TecnicoCras",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TecnicoCras_TecnicoId",
                table: "TecnicoCras",
                column: "TecnicoId");
        }
    }
}
