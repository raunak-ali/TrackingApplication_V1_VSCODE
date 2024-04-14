using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravkingApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class Sec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FeedBacks_TaskId",
                table: "FeedBacks");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBacks_TaskId",
                table: "FeedBacks",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FeedBacks_TaskId",
                table: "FeedBacks");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBacks_TaskId",
                table: "FeedBacks",
                column: "TaskId",
                unique: true);
        }
    }
}
