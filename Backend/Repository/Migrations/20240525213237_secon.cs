using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypingWebApi.Migrations
{
    /// <inheritdoc />
    public partial class secon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Chars",
                table: "Records",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MatchTime",
                table: "Records",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chars",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "MatchTime",
                table: "Records");
        }
    }
}
