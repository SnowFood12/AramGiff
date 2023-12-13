using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class ttnh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "GioiTinh",
                table: "TAI_KHOAN",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "TenTK",
                table: "DON_HANG",
                type: "nvarchar(15)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "THONGTIN_NHANHANG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoDT = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    DiaChi = table.Column<string>(type: "ntext", nullable: false),
                    GhiChu = table.Column<string>(type: "ntext", nullable: true),
                    DonHangId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THONGTIN_NHANHANG", x => x.Id);
                    table.ForeignKey(
                        name: "FK_THONGTIN_NHANHANG_DON_HANG_DonHangId",
                        column: x => x.DonHangId,
                        principalTable: "DON_HANG",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DON_HANG_TenTK",
                table: "DON_HANG",
                column: "TenTK");

            migrationBuilder.CreateIndex(
                name: "IX_THONGTIN_NHANHANG_DonHangId",
                table: "THONGTIN_NHANHANG",
                column: "DonHangId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DON_HANG_TAI_KHOAN_TenTK",
                table: "DON_HANG",
                column: "TenTK",
                principalTable: "TAI_KHOAN",
                principalColumn: "TenTK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DON_HANG_TAI_KHOAN_TenTK",
                table: "DON_HANG");

            migrationBuilder.DropTable(
                name: "THONGTIN_NHANHANG");

            migrationBuilder.DropIndex(
                name: "IX_DON_HANG_TenTK",
                table: "DON_HANG");

            migrationBuilder.AlterColumn<bool>(
                name: "GioiTinh",
                table: "TAI_KHOAN",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenTK",
                table: "DON_HANG",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldNullable: true);
        }
    }
}
