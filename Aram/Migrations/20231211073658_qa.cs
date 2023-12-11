using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class qa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DON_HANG_TAI_KHOAN_TaiKhoanTenTK",
                table: "DON_HANG");

            migrationBuilder.DropIndex(
                name: "IX_DON_HANG_TaiKhoanTenTK",
                table: "DON_HANG");

            migrationBuilder.DropColumn(
                name: "TaiKhoanTenTK",
                table: "DON_HANG");

            migrationBuilder.AddColumn<string>(
                name: "TenTK",
                table: "DON_HANG",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_DON_HANG_TenTK",
                table: "DON_HANG",
                column: "TenTK");

            migrationBuilder.AddForeignKey(
                name: "FK_DON_HANG_TAI_KHOAN_TenTK",
                table: "DON_HANG",
                column: "TenTK",
                principalTable: "TAI_KHOAN",
                principalColumn: "TenTK",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DON_HANG_TAI_KHOAN_TenTK",
                table: "DON_HANG");

            migrationBuilder.DropIndex(
                name: "IX_DON_HANG_TenTK",
                table: "DON_HANG");

            migrationBuilder.DropColumn(
                name: "TenTK",
                table: "DON_HANG");

            migrationBuilder.AddColumn<string>(
                name: "TaiKhoanTenTK",
                table: "DON_HANG",
                type: "nvarchar(15)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DON_HANG_TaiKhoanTenTK",
                table: "DON_HANG",
                column: "TaiKhoanTenTK");

            migrationBuilder.AddForeignKey(
                name: "FK_DON_HANG_TAI_KHOAN_TaiKhoanTenTK",
                table: "DON_HANG",
                column: "TaiKhoanTenTK",
                principalTable: "TAI_KHOAN",
                principalColumn: "TenTK");
        }
    }
}
