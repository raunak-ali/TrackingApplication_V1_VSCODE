using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravkingApplicationAPI.Migrations
{
    /// <inheritdoc />
    public partial class f : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Employee_info_Excel",
                table: "Users");

            migrationBuilder.AlterColumn<byte[]>(
                name: "AttendanceExcel",
                table: "Batches",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AddColumn<byte[]>(
                name: "Employee_info_Excel",
                table: "Batches",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Employee_info_Excel",
                table: "Batches");

            migrationBuilder.AddColumn<byte[]>(
                name: "Employee_info_Excel",
                table: "Users",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "AttendanceExcel",
                table: "Batches",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }
    }
}
