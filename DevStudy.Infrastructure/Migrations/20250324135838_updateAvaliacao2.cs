using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevStudy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateAvaliacao2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvaliacoesFisicas_Alunos_AlunoId",
                table: "AvaliacoesFisicas");

            migrationBuilder.DropIndex(
                name: "IX_AvaliacoesFisicas_AlunoId",
                table: "AvaliacoesFisicas");

            migrationBuilder.DropColumn(
                name: "Plano",
                table: "Alunos");

            migrationBuilder.AlterColumn<string>(
                name: "PercentualGordura",
                table: "AvaliacoesFisicas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "PlanoId",
                table: "Alunos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_PlanoId",
                table: "Alunos",
                column: "PlanoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alunos_Planos_PlanoId",
                table: "Alunos",
                column: "PlanoId",
                principalTable: "Planos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alunos_Planos_PlanoId",
                table: "Alunos");

            migrationBuilder.DropIndex(
                name: "IX_Alunos_PlanoId",
                table: "Alunos");

            migrationBuilder.DropColumn(
                name: "PlanoId",
                table: "Alunos");

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentualGordura",
                table: "AvaliacoesFisicas",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Plano",
                table: "Alunos",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

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
    }
}
