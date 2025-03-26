using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevStudy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateAvaliacao6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesFisicas_AlunoId",
                table: "AvaliacoesFisicas",
                column: "AlunoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AvaliacoesFisicas_Alunos_AlunoId",
                table: "AvaliacoesFisicas",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvaliacoesFisicas_Alunos_AlunoId",
                table: "AvaliacoesFisicas");

            migrationBuilder.DropIndex(
                name: "IX_AvaliacoesFisicas_AlunoId",
                table: "AvaliacoesFisicas");
        }
    }
}
