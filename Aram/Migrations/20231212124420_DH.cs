using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class DH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DON_HANG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThoiGianTaoDon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThaiDH = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    TenTK = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DON_HANG", x => x.Id);
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

            migrationBuilder.CreateIndex(
                name: "IX_DONHANG_CHITIET_DonHangId",
                table: "DONHANG_CHITIET",
                column: "DonHangId");

            migrationBuilder.CreateIndex(
                name: "IX_DONHANG_CHITIET_SanPhamId",
                table: "DONHANG_CHITIET",
                column: "SanPhamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DONHANG_CHITIET");

            migrationBuilder.DropTable(
                name: "DON_HANG");
        }
    }
}
