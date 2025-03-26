using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevStudy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newUpdateeee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AvaliacoesFisicas_AlunoId",
                table: "AvaliacoesFisicas");

            migrationBuilder.AddColumn<int>(
                name: "AvaliacaoId",
                table: "Alunos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesFisicas_AlunoId",
                table: "AvaliacoesFisicas",
                column: "AlunoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AvaliacoesFisicas_AlunoId",
                table: "AvaliacoesFisicas");

            migrationBuilder.DropColumn(
                name: "AvaliacaoId",
                table: "Alunos");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacoesFisicas_AlunoId",
                table: "AvaliacoesFisicas",
                column: "AlunoId");
        }
    }
}
