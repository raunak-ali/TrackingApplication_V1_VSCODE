using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravkingApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class sc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Daily_update_Excel",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "DailyUpdates",
                columns: table => new
                {
                    DailyUpdateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LearnedToday = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanForTomorrow = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChallengeToday = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OneDriveLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyUpdates", x => x.DailyUpdateId);
                    table.ForeignKey(
                        name: "FK_DailyUpdates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyUpdates_UserId",
                table: "DailyUpdates",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyUpdates");

            migrationBuilder.AddColumn<byte[]>(
                name: "Daily_update_Excel",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
