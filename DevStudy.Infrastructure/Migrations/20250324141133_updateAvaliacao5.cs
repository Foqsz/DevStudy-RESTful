﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevStudy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateAvaliacao5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PercentualGordura",
                table: "AvaliacoesFisicas",
                newName: "AvaliacaoPeso");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvaliacaoPeso",
                table: "AvaliacoesFisicas",
                newName: "PercentualGordura");
        }
    }
}
