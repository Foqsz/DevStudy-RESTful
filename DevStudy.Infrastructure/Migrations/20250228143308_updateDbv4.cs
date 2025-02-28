using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevStudy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDbv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlunoId",
                table: "Exercicios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercicios_AlunoId",
                table: "Exercicios",
                column: "AlunoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercicios_Alunos_AlunoId",
                table: "Exercicios",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercicios_Alunos_AlunoId",
                table: "Exercicios");

            migrationBuilder.DropIndex(
                name: "IX_Exercicios_AlunoId",
                table: "Exercicios");

            migrationBuilder.DropColumn(
                name: "AlunoId",
                table: "Exercicios");
        }
    }
}
