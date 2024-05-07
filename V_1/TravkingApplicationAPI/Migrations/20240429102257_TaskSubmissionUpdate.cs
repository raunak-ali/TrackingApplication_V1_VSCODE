using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravkingApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class TaskSubmissionUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "TaskSubmissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "TaskSubmissions");
        }
    }
}
