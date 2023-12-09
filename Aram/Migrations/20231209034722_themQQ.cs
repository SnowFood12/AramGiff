using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class themQQ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrangThaiDonHang",
                table: "DON_HANG",
                newName: "TrangThaiDH");

            migrationBuilder.CreateTable(
                name: "GIO_HANG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTK = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TaiKhoanTenTK = table.Column<string>(type: "nvarchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GIO_HANG", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GIO_HANG_TAI_KHOAN_TaiKhoanTenTK",
                        column: x => x.TaiKhoanTenTK,
                        principalTable: "TAI_KHOAN",
                        principalColumn: "TenTK");
                });

            migrationBuilder.CreateTable(
                name: "GIOHANG_CHITIET",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GioHangId = table.Column<int>(type: "int", nullable: false),
                    SanPhamId = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GIOHANG_CHITIET", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GIOHANG_CHITIET_GIO_HANG_GioHangId",
                        column: x => x.GioHangId,
                        principalTable: "GIO_HANG",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GIOHANG_CHITIET_SAN_PHAM_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "SAN_PHAM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GIO_HANG_TaiKhoanTenTK",
                table: "GIO_HANG",
                column: "TaiKhoanTenTK");

            migrationBuilder.CreateIndex(
                name: "IX_GIOHANG_CHITIET_GioHangId",
                table: "GIOHANG_CHITIET",
                column: "GioHangId");

            migrationBuilder.CreateIndex(
                name: "IX_GIOHANG_CHITIET_SanPhamId",
                table: "GIOHANG_CHITIET",
                column: "SanPhamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GIOHANG_CHITIET");

            migrationBuilder.DropTable(
                name: "GIO_HANG");

            migrationBuilder.RenameColumn(
                name: "TrangThaiDH",
                table: "DON_HANG",
                newName: "TrangThaiDonHang");
        }
    }
}
