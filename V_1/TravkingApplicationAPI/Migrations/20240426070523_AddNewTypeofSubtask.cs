using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravkingApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTypeofSubtask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestCases",
                table: "SubTask",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isCodingProblem",
                table: "SubTask",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestCases",
                table: "SubTask");

            migrationBuilder.DropColumn(
                name: "isCodingProblem",
                table: "SubTask");
        }
    }
}
