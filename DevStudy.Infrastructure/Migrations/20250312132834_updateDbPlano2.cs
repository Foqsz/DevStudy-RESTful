using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevStudy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDbPlano2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Plano",
                table: "Alunos");

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

            migrationBuilder.AddColumn<string>(
                name: "Plano",
                table: "Alunos",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
