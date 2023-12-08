using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class suaDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CUA_HANG_TAI_KHOAN_TaiKhoanId",
                table: "CUA_HANG");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TAI_KHOAN",
                table: "TAI_KHOAN");

            migrationBuilder.DropIndex(
                name: "IX_CUA_HANG_TaiKhoanId",
                table: "CUA_HANG");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TAI_KHOAN");

            migrationBuilder.DropColumn(
                name: "TaiKhoanId",
                table: "CUA_HANG");

            migrationBuilder.AlterColumn<string>(
                name: "TenTK",
                table: "TAI_KHOAN",
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
                table: "CUA_HANG",
                type: "nvarchar(15)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TenTK",
                table: "CUA_HANG",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TAI_KHOAN",
                table: "TAI_KHOAN",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CUA_HANG_TAI_KHOAN_TaiKhoanTenTK",
                table: "CUA_HANG");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TAI_KHOAN",
                table: "TAI_KHOAN");

            migrationBuilder.DropIndex(
                name: "IX_CUA_HANG_TaiKhoanTenTK",
                table: "CUA_HANG");

            migrationBuilder.DropColumn(
                name: "TaiKhoanTenTK",
                table: "CUA_HANG");

            migrationBuilder.DropColumn(
                name: "TenTK",
                table: "CUA_HANG");

            migrationBuilder.AlterColumn<string>(
                name: "TenTK",
                table: "TAI_KHOAN",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TAI_KHOAN",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "TaiKhoanId",
                table: "CUA_HANG",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TAI_KHOAN",
                table: "TAI_KHOAN",
                column: "Id");

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
    }
}
