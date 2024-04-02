using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravkingApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Batches_BatchId",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Batches_BatchId",
                table: "Tasks",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "BatchId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Batches_BatchId",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Batches_BatchId",
                table: "Tasks",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "BatchId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
