using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class themDHH : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonHang",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThoiGianTaoDon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrangThaiGioHang = table.Column<bool>(type: "bit", nullable: false),
                    TrangThaiDonHang = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    TenTK = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaiKhoanTenTK = table.Column<string>(type: "nvarchar(15)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DonHang_TAI_KHOAN_TaiKhoanTenTK",
                        column: x => x.TaiKhoanTenTK,
                        principalTable: "TAI_KHOAN",
                        principalColumn: "TenTK");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonHang_TaiKhoanTenTK",
                table: "DonHang",
                column: "TaiKhoanTenTK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonHang");
        }
    }
}
