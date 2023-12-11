using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class aa : Migration
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

		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
