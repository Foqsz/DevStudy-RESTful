using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevStudy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDbv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercicios_Alunos_AlunoId",
                table: "Exercicios");

            migrationBuilder.RenameColumn(
                name: "AlunoId",
                table: "Exercicios",
                newName: "TreinoId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercicios_AlunoId",
                table: "Exercicios",
                newName: "IX_Exercicios_TreinoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercicios_Treinos_TreinoId",
                table: "Exercicios",
                column: "TreinoId",
                principalTable: "Treinos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercicios_Treinos_TreinoId",
                table: "Exercicios");

            migrationBuilder.RenameColumn(
                name: "TreinoId",
                table: "Exercicios",
                newName: "AlunoId");

            migrationBuilder.RenameIndex(
                name: "IX_Exercicios_TreinoId",
                table: "Exercicios",
                newName: "IX_Exercicios_AlunoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercicios_Alunos_AlunoId",
                table: "Exercicios",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id");
        }
    }
}
