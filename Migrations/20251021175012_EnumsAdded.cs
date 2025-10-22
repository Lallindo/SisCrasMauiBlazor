using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SisCras.Migrations
{
    /// <inheritdoc />
    public partial class EnumsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataNascimento",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "Escolaridade",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstadoCivil",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FonteRenda",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nis",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NomeSocial",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ocupacao",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OrientacaoSexual",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Profissao",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Raca",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "RendaBruta",
                table: "Usuarios",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Rg",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Sexo",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConfiguracaoFamiliar",
                table: "Familias",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Escolaridade",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EstadoCivil",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "FonteRenda",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Nis",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "NomeSocial",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Ocupacao",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "OrientacaoSexual",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Profissao",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Raca",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "RendaBruta",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Rg",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ConfiguracaoFamiliar",
                table: "Familias");
        }
    }
}
