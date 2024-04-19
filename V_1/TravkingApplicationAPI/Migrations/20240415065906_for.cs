using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravkingApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class @for : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FeedBacks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "FeedBacks");
        }
    }
}
