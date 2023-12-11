using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class suaLaiFKTK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GIO_HANG_TAI_KHOAN_TaiKhoanTenTK",
                table: "GIO_HANG");

            migrationBuilder.DropIndex(
                name: "IX_GIO_HANG_TaiKhoanTenTK",
                table: "GIO_HANG");

            migrationBuilder.DropColumn(
                name: "TaiKhoanTenTK",
                table: "GIO_HANG");

            migrationBuilder.AlterColumn<string>(
                name: "TenTK",
                table: "DON_HANG",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaiKhoanTenTK",
                table: "GIO_HANG",
                type: "nvarchar(15)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenTK",
                table: "DON_HANG",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GIO_HANG_TaiKhoanTenTK",
                table: "GIO_HANG",
                column: "TaiKhoanTenTK");

            migrationBuilder.AddForeignKey(
                name: "FK_GIO_HANG_TAI_KHOAN_TaiKhoanTenTK",
                table: "GIO_HANG",
                column: "TaiKhoanTenTK",
                principalTable: "TAI_KHOAN",
                principalColumn: "TenTK");
        }
    }
}
