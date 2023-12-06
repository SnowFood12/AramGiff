using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class addTrangThai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DiaChi",
                table: "CUA_HANG",
                type: "ntext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "ntext");

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai",
                table: "CUA_HANG",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "CUA_HANG");

            migrationBuilder.AlterColumn<string>(
                name: "DiaChi",
                table: "CUA_HANG",
                type: "ntext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "ntext",
                oldNullable: true);
        }
    }
}
