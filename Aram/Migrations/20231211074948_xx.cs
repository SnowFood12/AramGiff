using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class xx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GIO_HANG_TenTK",
                table: "GIO_HANG",
                column: "TenTK");

            migrationBuilder.CreateIndex(
                name: "IX_CUA_HANG_TenTK",
                table: "CUA_HANG",
                column: "TenTK");

            migrationBuilder.AddForeignKey(
                name: "FK_CUA_HANG_TAI_KHOAN_TenTK",
                table: "CUA_HANG",
                column: "TenTK",
                principalTable: "TAI_KHOAN",
                principalColumn: "TenTK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GIO_HANG_TAI_KHOAN_TenTK",
                table: "GIO_HANG",
                column: "TenTK",
                principalTable: "TAI_KHOAN",
                principalColumn: "TenTK",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CUA_HANG_TAI_KHOAN_TenTK",
                table: "CUA_HANG");

            migrationBuilder.DropForeignKey(
                name: "FK_GIO_HANG_TAI_KHOAN_TenTK",
                table: "GIO_HANG");

            migrationBuilder.DropIndex(
                name: "IX_GIO_HANG_TenTK",
                table: "GIO_HANG");

            migrationBuilder.DropIndex(
                name: "IX_CUA_HANG_TenTK",
                table: "CUA_HANG");
        }
    }
}
