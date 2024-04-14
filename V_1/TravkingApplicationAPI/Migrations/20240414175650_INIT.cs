using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravkingApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedBacks_Tasks_TaskId",
                table: "FeedBacks");

            migrationBuilder.DropIndex(
                name: "IX_FeedBacks_TaskId",
                table: "FeedBacks");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBacks_TaskId",
                table: "FeedBacks",
                column: "TaskId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FeedBacks_Tasks_TaskId",
                table: "FeedBacks",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "UserTaskID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedBacks_Tasks_TaskId",
                table: "FeedBacks");

            migrationBuilder.DropIndex(
                name: "IX_FeedBacks_TaskId",
                table: "FeedBacks");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBacks_TaskId",
                table: "FeedBacks",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedBacks_Tasks_TaskId",
                table: "FeedBacks",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "UserTaskID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
