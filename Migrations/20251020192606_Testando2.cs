using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SisCras.Migrations
{
    /// <inheritdoc />
    public partial class Testando2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prontuarios_Familias_Id",
                table: "Prontuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Prontuarios_Tecnicos_Id",
                table: "Prontuarios");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Prontuarios",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateIndex(
                name: "IX_Prontuarios_FamiliaId",
                table: "Prontuarios",
                column: "FamiliaId");

            migrationBuilder.CreateIndex(
                name: "IX_Prontuarios_TecnicoId",
                table: "Prontuarios",
                column: "TecnicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prontuarios_Familias_FamiliaId",
                table: "Prontuarios",
                column: "FamiliaId",
                principalTable: "Familias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prontuarios_Tecnicos_TecnicoId",
                table: "Prontuarios",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prontuarios_Familias_FamiliaId",
                table: "Prontuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Prontuarios_Tecnicos_TecnicoId",
                table: "Prontuarios");

            migrationBuilder.DropIndex(
                name: "IX_Prontuarios_FamiliaId",
                table: "Prontuarios");

            migrationBuilder.DropIndex(
                name: "IX_Prontuarios_TecnicoId",
                table: "Prontuarios");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Prontuarios",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_Prontuarios_Familias_Id",
                table: "Prontuarios",
                column: "Id",
                principalTable: "Familias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prontuarios_Tecnicos_Id",
                table: "Prontuarios",
                column: "Id",
                principalTable: "Tecnicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
