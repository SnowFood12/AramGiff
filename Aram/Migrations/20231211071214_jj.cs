﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class jj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CUA_HANG_TAI_KHOAN_TaiKhoanTenTK",
                table: "CUA_HANG");

            migrationBuilder.DropForeignKey(
                name: "FK_DON_HANG_TAI_KHOAN_TenTK",
                table: "DON_HANG");

            migrationBuilder.DropIndex(
                name: "IX_DON_HANG_TenTK",
                table: "DON_HANG");

            migrationBuilder.DropIndex(
                name: "IX_CUA_HANG_TaiKhoanTenTK",
                table: "CUA_HANG");

            migrationBuilder.DropColumn(
                name: "TaiKhoanTenTK",
                table: "CUA_HANG");

            migrationBuilder.AlterColumn<string>(
                name: "TenTK",
                table: "GIO_HANG",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenTK",
                table: "DON_HANG",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaiKhoanTenTK",
                table: "DON_HANG",
                type: "nvarchar(15)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TenTK",
                table: "CUA_HANG",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DON_HANG_TaiKhoanTenTK",
                table: "DON_HANG",
                column: "TaiKhoanTenTK");

            migrationBuilder.AddForeignKey(
                name: "FK_DON_HANG_TAI_KHOAN_TaiKhoanTenTK",
                table: "DON_HANG",
                column: "TaiKhoanTenTK",
                principalTable: "TAI_KHOAN",
                principalColumn: "TenTK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "TenTK",
                table: "GIO_HANG",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "TenTK",
                table: "DON_HANG",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "TenTK",
                table: "CUA_HANG",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<string>(
                name: "TaiKhoanTenTK",
                table: "CUA_HANG",
                type: "nvarchar(15)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DON_HANG_TenTK",
                table: "DON_HANG",
                column: "TenTK");

            migrationBuilder.CreateIndex(
                name: "IX_CUA_HANG_TaiKhoanTenTK",
                table: "CUA_HANG",
                column: "TaiKhoanTenTK");

            migrationBuilder.AddForeignKey(
                name: "FK_CUA_HANG_TAI_KHOAN_TaiKhoanTenTK",
                table: "CUA_HANG",
                column: "TaiKhoanTenTK",
                principalTable: "TAI_KHOAN",
                principalColumn: "TenTK");

            migrationBuilder.AddForeignKey(
                name: "FK_DON_HANG_TAI_KHOAN_TenTK",
                table: "DON_HANG",
                column: "TenTK",
                principalTable: "TAI_KHOAN",
                principalColumn: "TenTK");
        }
    }
}
