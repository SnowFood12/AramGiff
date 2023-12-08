using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class DHchitiet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonHang_TAI_KHOAN_TaiKhoanTenTK",
                table: "DonHang");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DonHang",
                table: "DonHang");

            migrationBuilder.RenameTable(
                name: "DonHang",
                newName: "DON_HANG");

            migrationBuilder.RenameIndex(
                name: "IX_DonHang_TaiKhoanTenTK",
                table: "DON_HANG",
                newName: "IX_DON_HANG_TaiKhoanTenTK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DON_HANG",
                table: "DON_HANG",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DON_HANG_TAI_KHOAN_TaiKhoanTenTK",
                table: "DON_HANG",
                column: "TaiKhoanTenTK",
                principalTable: "TAI_KHOAN",
                principalColumn: "TenTK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DON_HANG_TAI_KHOAN_TaiKhoanTenTK",
                table: "DON_HANG");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DON_HANG",
                table: "DON_HANG");

            migrationBuilder.RenameTable(
                name: "DON_HANG",
                newName: "DonHang");

            migrationBuilder.RenameIndex(
                name: "IX_DON_HANG_TaiKhoanTenTK",
                table: "DonHang",
                newName: "IX_DonHang_TaiKhoanTenTK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DonHang",
                table: "DonHang",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DonHang_TAI_KHOAN_TaiKhoanTenTK",
                table: "DonHang",
                column: "TaiKhoanTenTK",
                principalTable: "TAI_KHOAN",
                principalColumn: "TenTK");
        }
    }
}
