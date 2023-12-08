using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aram.Migrations
{
    /// <inheritdoc />
    public partial class rr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "THONGTIN_NHANHANG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SoDT = table.Column<string>(type: "varchar(10)", nullable: true),
                    DiaChi = table.Column<string>(type: "ntext", nullable: true),
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
                name: "IX_THONGTIN_NHANHANG_DonHangId",
                table: "THONGTIN_NHANHANG",
                column: "DonHangId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "THONGTIN_NHANHANG");
        }
    }
}
