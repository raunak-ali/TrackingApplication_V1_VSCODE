using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravkingApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class INI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FeedBacks_TaskId",
                table: "FeedBacks");

            migrationBuilder.CreateIndex(
                name: "IX_TaskSubmissions_UserId",
                table: "TaskSubmissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBacks_TaskId",
                table: "FeedBacks",
                column: "TaskId",
                unique: false);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskSubmissions_Users_UserId",
                table: "TaskSubmissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskSubmissions_Users_UserId",
                table: "TaskSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_TaskSubmissions_UserId",
                table: "TaskSubmissions");

            migrationBuilder.DropIndex(
                name: "IX_FeedBacks_TaskId",
                table: "FeedBacks");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBacks_TaskId",
                table: "FeedBacks",
                column: "TaskId");
        }
    }
}
