using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bures.Migrations
{
    /// <inheritdoc />
    public partial class AddResponseDialogToChoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponseDialog",
                table: "Choices",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserTaskResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    ActNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCorrect = table.Column<bool>(type: "INTEGER", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTaskResults", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTaskResults");

            migrationBuilder.DropColumn(
                name: "ResponseDialog",
                table: "Choices");
        }
    }
}
