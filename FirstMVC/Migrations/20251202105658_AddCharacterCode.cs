using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddCharacterCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choice_StoryActs_StoryActId",
                table: "Choice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Choice",
                table: "Choice");

            migrationBuilder.RenameTable(
                name: "Choice",
                newName: "Choices");

            migrationBuilder.RenameIndex(
                name: "IX_Choice_StoryActId",
                table: "Choices",
                newName: "IX_Choices_StoryActId");

            migrationBuilder.AddColumn<string>(
                name: "CharacterCode",
                table: "Characters",
                type: "TEXT",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Choices",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choices",
                table: "Choices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_StoryActs_StoryActId",
                table: "Choices",
                column: "StoryActId",
                principalTable: "StoryActs",
                principalColumn: "StoryActId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Choices_StoryActs_StoryActId",
                table: "Choices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Choices",
                table: "Choices");

            migrationBuilder.DropColumn(
                name: "CharacterCode",
                table: "Characters");

            migrationBuilder.RenameTable(
                name: "Choices",
                newName: "Choice");

            migrationBuilder.RenameIndex(
                name: "IX_Choices_StoryActId",
                table: "Choice",
                newName: "IX_Choice_StoryActId");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Choice",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choice",
                table: "Choice",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Choice_StoryActs_StoryActId",
                table: "Choice",
                column: "StoryActId",
                principalTable: "StoryActs",
                principalColumn: "StoryActId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
