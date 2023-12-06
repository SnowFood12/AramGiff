using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class addTKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaiKhoanId",
                table: "CUA_HANG",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TAI_KHOAN",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_CUA_HANG_TaiKhoanId",
                table: "CUA_HANG",
                column: "TaiKhoanId");

            migrationBuilder.AddForeignKey(
                name: "FK_CUA_HANG_TAI_KHOAN_TaiKhoanId",
                table: "CUA_HANG",
                column: "TaiKhoanId",
                principalTable: "TAI_KHOAN",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CUA_HANG_TAI_KHOAN_TaiKhoanId",
                table: "CUA_HANG");

            migrationBuilder.DropTable(
                name: "TAI_KHOAN");

            migrationBuilder.DropIndex(
                name: "IX_CUA_HANG_TaiKhoanId",
                table: "CUA_HANG");

            migrationBuilder.DropColumn(
                name: "TaiKhoanId",
                table: "CUA_HANG");
        }
    }
}
