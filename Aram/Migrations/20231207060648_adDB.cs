﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class adDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LOAI_SP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(50)", nullable: true),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenTK = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    MatKhau = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SoDT = table.Column<string>(type: "char(10)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "date", nullable: false),
                    LoaiTK = table.Column<bool>(type: "bit", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAI_KHOAN", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CUA_HANG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SoDT = table.Column<string>(type: "char(10)", nullable: true),
                    NgayTaoCuaHang = table.Column<DateTime>(type: "Date", nullable: false),
                    DiaChi = table.Column<string>(type: "ntext", nullable: true),
                    LinkMap = table.Column<string>(type: "text", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    TaiKhoanId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUA_HANG", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CUA_HANG_TAI_KHOAN_TaiKhoanId",
                        column: x => x.TaiKhoanId,
                        principalTable: "TAI_KHOAN",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SAN_PHAM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gia = table.Column<int>(type: "int", nullable: true),
                    PicData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CuaHangId = table.Column<int>(type: "int", nullable: false),
                    LoaiSPId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_CUA_HANG_TaiKhoanId",
                table: "CUA_HANG",
                column: "TaiKhoanId");

            migrationBuilder.CreateIndex(
                name: "IX_SAN_PHAM_CuaHangId",
                table: "SAN_PHAM",
                column: "CuaHangId");

            migrationBuilder.CreateIndex(
                name: "IX_SAN_PHAM_LoaiSPId",
                table: "SAN_PHAM",
                column: "LoaiSPId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SAN_PHAM");

            migrationBuilder.DropTable(
                name: "CUA_HANG");

            migrationBuilder.DropTable(
                name: "LOAI_SP");

            migrationBuilder.DropTable(
                name: "TAI_KHOAN");
        }
    }
}