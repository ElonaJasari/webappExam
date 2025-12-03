using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bures.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserCharacterSelectionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndingType",
                table: "UserProgress",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SelectedCharacterId",
                table: "UserProgress",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SelectedCharacterName",
                table: "UserProgress",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Trust",
                table: "UserProgress",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrustChange",
                table: "Choices",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserCharacterSelection",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    CharacterId = table.Column<int>(type: "INTEGER", nullable: false),
                    CustomName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCharacterSelection", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCharacterSelection");

            migrationBuilder.DropColumn(
                name: "EndingType",
                table: "UserProgress");

            migrationBuilder.DropColumn(
                name: "SelectedCharacterId",
                table: "UserProgress");

            migrationBuilder.DropColumn(
                name: "SelectedCharacterName",
                table: "UserProgress");

            migrationBuilder.DropColumn(
                name: "Trust",
                table: "UserProgress");

            migrationBuilder.DropColumn(
                name: "TrustChange",
                table: "Choices");
        }
    }
}
