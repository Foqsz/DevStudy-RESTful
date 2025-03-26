using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevStudy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newUpdateee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PercentualGordura",
                table: "AvaliacoesFisicas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PercentualGordura",
                table: "AvaliacoesFisicas",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
