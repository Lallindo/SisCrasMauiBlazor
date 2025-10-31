using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SisCras.Migrations
{
    /// <inheritdoc />
    public partial class Testando : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CrasId",
                table: "Prontuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FamiliaId",
                table: "Prontuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TecnicoId",
                table: "Prontuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prontuarios_CrasId",
                table: "Prontuarios",
                column: "CrasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prontuarios_Cras_CrasId",
                table: "Prontuarios",
                column: "CrasId",
                principalTable: "Cras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prontuarios_Cras_CrasId",
                table: "Prontuarios");

            migrationBuilder.DropIndex(
                name: "IX_Prontuarios_CrasId",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "CrasId",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "FamiliaId",
                table: "Prontuarios");

            migrationBuilder.DropColumn(
                name: "TecnicoId",
                table: "Prontuarios");
        }
    }
}
