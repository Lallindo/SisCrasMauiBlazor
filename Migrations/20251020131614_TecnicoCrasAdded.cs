using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SisCras.Migrations
{
    /// <inheritdoc />
    public partial class TecnicoCrasAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamiliaUsuario_Familias__FamiliaId",
                table: "FamiliaUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_FamiliaUsuario_Usuario__UsuarioId",
                table: "FamiliaUsuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FamiliaUsuario",
                table: "FamiliaUsuario");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "FamiliaUsuario",
                newName: "FamiliaUsuarios");

            migrationBuilder.RenameColumn(
                name: "_UsuarioId",
                table: "FamiliaUsuarios",
                newName: "UsuarioId");

            migrationBuilder.RenameColumn(
                name: "_FamiliaId",
                table: "FamiliaUsuarios",
                newName: "FamiliaId");

            migrationBuilder.RenameIndex(
                name: "IX_FamiliaUsuario__UsuarioId",
                table: "FamiliaUsuarios",
                newName: "IX_FamiliaUsuarios_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamiliaUsuarios",
                table: "FamiliaUsuarios",
                columns: new[] { "FamiliaId", "UsuarioId" });

            migrationBuilder.CreateTable(
                name: "Cras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TecnicoCras",
                columns: table => new
                {
                    CrasId = table.Column<int>(type: "INTEGER", nullable: false),
                    TecnicoId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataEntrada = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DataSaida = table.Column<DateOnly>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TecnicoCras", x => new { x.CrasId, x.TecnicoId });
                    table.ForeignKey(
                        name: "FK_TecnicoCras_Cras_CrasId",
                        column: x => x.CrasId,
                        principalTable: "Cras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TecnicoCras_Tecnicos_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "Tecnicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TecnicoCras_TecnicoId",
                table: "TecnicoCras",
                column: "TecnicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_FamiliaUsuarios_Familias_FamiliaId",
                table: "FamiliaUsuarios",
                column: "FamiliaId",
                principalTable: "Familias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FamiliaUsuarios_Usuarios_UsuarioId",
                table: "FamiliaUsuarios",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamiliaUsuarios_Familias_FamiliaId",
                table: "FamiliaUsuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_FamiliaUsuarios_Usuarios_UsuarioId",
                table: "FamiliaUsuarios");

            migrationBuilder.DropTable(
                name: "TecnicoCras");

            migrationBuilder.DropTable(
                name: "Cras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FamiliaUsuarios",
                table: "FamiliaUsuarios");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "FamiliaUsuarios",
                newName: "FamiliaUsuario");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "FamiliaUsuario",
                newName: "_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "FamiliaId",
                table: "FamiliaUsuario",
                newName: "_FamiliaId");

            migrationBuilder.RenameIndex(
                name: "IX_FamiliaUsuarios_UsuarioId",
                table: "FamiliaUsuario",
                newName: "IX_FamiliaUsuario__UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FamiliaUsuario",
                table: "FamiliaUsuario",
                columns: new[] { "_FamiliaId", "_UsuarioId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FamiliaUsuario_Familias__FamiliaId",
                table: "FamiliaUsuario",
                column: "_FamiliaId",
                principalTable: "Familias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FamiliaUsuario_Usuario__UsuarioId",
                table: "FamiliaUsuario",
                column: "_UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
