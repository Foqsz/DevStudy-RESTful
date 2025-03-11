using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevStudy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDbPlano : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Instrutores_InstrutorId",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "DuracaoMeses",
                table: "Planos");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Planos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "Planos",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "InstrutorId",
                table: "Alunos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Instrutores_InstrutorId",
                table: "Alunos",
                column: "InstrutorId",
                principalTable: "Instrutores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Instrutores_InstrutorId",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Planos");

            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "Planos");

            migrationBuilder.AddColumn<int>(
                name: "DuracaoMeses",
                table: "Planos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "InstrutorId",
                table: "Alunos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Instrutores_InstrutorId",
                table: "Alunos",
                column: "InstrutorId",
                principalTable: "Instrutores",
                principalColumn: "Id");
        }
    }
}
