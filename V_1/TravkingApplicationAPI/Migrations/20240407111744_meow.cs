using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravkingApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class meow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_FeedBacks_FeedbackId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_TaskSubmissions_TaskSubmissionId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Users_RatedBy",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Users_RatedTo",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "Ratings");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_TaskSubmissionId",
                table: "Ratings",
                newName: "IX_Ratings_TaskSubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_RatedTo",
                table: "Ratings",
                newName: "IX_Ratings_RatedTo");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_RatedBy",
                table: "Ratings",
                newName: "IX_Ratings_RatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_FeedbackId",
                table: "Ratings",
                newName: "IX_Ratings_FeedbackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_FeedBacks_FeedbackId",
                table: "Ratings",
                column: "FeedbackId",
                principalTable: "FeedBacks",
                principalColumn: "FeedbackId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_TaskSubmissions_TaskSubmissionId",
                table: "Ratings",
                column: "TaskSubmissionId",
                principalTable: "TaskSubmissions",
                principalColumn: "TaskSubmissionsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_RatedBy",
                table: "Ratings",
                column: "RatedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_RatedTo",
                table: "Ratings",
                column: "RatedTo",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_FeedBacks_FeedbackId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_TaskSubmissions_TaskSubmissionId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_RatedBy",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_RatedTo",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "Rating");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_TaskSubmissionId",
                table: "Rating",
                newName: "IX_Rating_TaskSubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_RatedTo",
                table: "Rating",
                newName: "IX_Rating_RatedTo");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_RatedBy",
                table: "Rating",
                newName: "IX_Rating_RatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_FeedbackId",
                table: "Rating",
                newName: "IX_Rating_FeedbackId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                column: "RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_FeedBacks_FeedbackId",
                table: "Rating",
                column: "FeedbackId",
                principalTable: "FeedBacks",
                principalColumn: "FeedbackId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_TaskSubmissions_TaskSubmissionId",
                table: "Rating",
                column: "TaskSubmissionId",
                principalTable: "TaskSubmissions",
                principalColumn: "TaskSubmissionsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Users_RatedBy",
                table: "Rating",
                column: "RatedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Users_RatedTo",
                table: "Rating",
                column: "RatedTo",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
