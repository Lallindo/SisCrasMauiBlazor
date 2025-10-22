using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SisCras.Migrations
{
    /// <inheritdoc />
    public partial class PrimaryKeySetada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TecnicoCras",
                table: "TecnicoCras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FamiliaUsuarios",
                table: "FamiliaUsuarios");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TecnicoCras",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FamiliaUsuarios",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TecnicoCras",
                table: "TecnicoCras",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamiliaUsuarios",
                table: "FamiliaUsuarios",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TecnicoCras_CrasId",
                table: "TecnicoCras",
                column: "CrasId");

            migrationBuilder.CreateIndex(
                name: "IX_FamiliaUsuarios_FamiliaId",
                table: "FamiliaUsuarios",
                column: "FamiliaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TecnicoCras",
                table: "TecnicoCras");

            migrationBuilder.DropIndex(
                name: "IX_TecnicoCras_CrasId",
                table: "TecnicoCras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FamiliaUsuarios",
                table: "FamiliaUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_FamiliaUsuarios_FamiliaId",
                table: "FamiliaUsuarios");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TecnicoCras",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FamiliaUsuarios",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TecnicoCras",
                table: "TecnicoCras",
                columns: new[] { "CrasId", "TecnicoId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamiliaUsuarios",
                table: "FamiliaUsuarios",
                columns: new[] { "FamiliaId", "UsuarioId" });
        }
    }
}
