using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class newDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CUA_HANG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoDT = table.Column<string>(type: "char(10)", maxLength: 10, nullable: true),
                    NgayTaoCuaHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChi = table.Column<string>(type: "ntext", nullable: false),
                    LinkMap = table.Column<string>(type: "text", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: true),
                    TenTK = table.Column<string>(type: "varchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUA_HANG", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LOAI_SP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOAI_SP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TAI_KHOAN",
                columns: table => new
                {
                    TenTK = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SoDT = table.Column<string>(type: "char(10)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaiTK = table.Column<bool>(type: "bit", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAI_KHOAN", x => x.TenTK);
                });

            migrationBuilder.CreateTable(
                name: "SAN_PHAM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Gia = table.Column<int>(type: "int", nullable: false),
                    PicData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CuaHangId = table.Column<int>(type: "int", nullable: false),
                    LoaiSPId = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAN_PHAM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SAN_PHAM_CUA_HANG_CuaHangId",
                        column: x => x.CuaHangId,
                        principalTable: "CUA_HANG",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SAN_PHAM_LOAI_SP_LoaiSPId",
                        column: x => x.LoaiSPId,
                        principalTable: "LOAI_SP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DON_HANG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThoiGianTaoDon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThaiDH = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    TenTK = table.Column<string>(type: "nvarchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DON_HANG", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DON_HANG_TAI_KHOAN_TenTK",
                        column: x => x.TenTK,
                        principalTable: "TAI_KHOAN",
                        principalColumn: "TenTK");
                });

            migrationBuilder.CreateTable(
                name: "DONHANG_CHITIET",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    DonHangId = table.Column<int>(type: "int", nullable: false),
                    SanPhamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DONHANG_CHITIET", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DONHANG_CHITIET_DON_HANG_DonHangId",
                        column: x => x.DonHangId,
                        principalTable: "DON_HANG",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DONHANG_CHITIET_SAN_PHAM_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "SAN_PHAM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_DONHANG_CHITIET_DonHangId",
                table: "DONHANG_CHITIET",
                column: "DonHangId");

            migrationBuilder.CreateIndex(
                name: "IX_DONHANG_CHITIET_SanPhamId",
                table: "DONHANG_CHITIET",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_SAN_PHAM_CuaHangId",
                table: "SAN_PHAM",
                column: "CuaHangId");

            migrationBuilder.CreateIndex(
                name: "IX_SAN_PHAM_LoaiSPId",
                table: "SAN_PHAM",
                column: "LoaiSPId");

            migrationBuilder.CreateIndex(
                name: "IX_THONGTIN_NHANHANG_DonHangId",
                table: "THONGTIN_NHANHANG",
                column: "DonHangId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DONHANG_CHITIET");

            migrationBuilder.DropTable(
                name: "THONGTIN_NHANHANG");

            migrationBuilder.DropTable(
                name: "SAN_PHAM");

            migrationBuilder.DropTable(
                name: "DON_HANG");

            migrationBuilder.DropTable(
                name: "CUA_HANG");

            migrationBuilder.DropTable(
                name: "LOAI_SP");

            migrationBuilder.DropTable(
                name: "TAI_KHOAN");
        }
    }
}
