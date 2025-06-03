using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TypingWebApi.Migrations
{
    /// <inheritdoc />
    public partial class LessonProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LessonProgress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    BestWpm = table.Column<int>(type: "integer", nullable: false),
                    BestRaw = table.Column<int>(type: "integer", nullable: false),
                    BestAccuracy = table.Column<int>(type: "integer", nullable: false),
                    ProgressPercent = table.Column<double>(type: "double precision", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonProgress", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonProgress");
        }
    }
}
