﻿// <auto-generated />
using System;
using Aram.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aram.Migrations
{
    [DbContext(typeof(AramContext))]
    [Migration("20231208101250_rr")]
    partial class rr
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Aram.Models.CuaHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DiaChi")
                        .HasColumnType("ntext");

                    b.Property<string>("LinkMap")
                        .HasColumnType("text");

                    b.Property<DateTime>("NgayTaoCuaHang")
                        .HasColumnType("Date");

                    b.Property<string>("SoDT")
                        .HasColumnType("char(10)");

                    b.Property<string>("TaiKhoanTenTK")
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Ten")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TenTK")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("TaiKhoanTenTK");

                    b.ToTable("CUA_HANG");
                });

            modelBuilder.Entity("Aram.Models.DonHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("TaiKhoanTenTK")
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("TenTK")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ThoiGianTaoDon")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.Property<string>("TrangThaiDonHang")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThaiGioHang")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("TaiKhoanTenTK");

                    b.ToTable("DON_HANG");
                });

            modelBuilder.Entity("Aram.Models.DonHang_ChiTiet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DonHangId")
                        .HasColumnType("int");

                    b.Property<int>("SanPhamId")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("DonHangId");

                    b.HasIndex("SanPhamId");

                    b.ToTable("DONHANG_CHITIET");
                });

            modelBuilder.Entity("Aram.Models.LoaiSP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ten")
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("LOAI_SP");
                });

            modelBuilder.Entity("Aram.Models.SanPham", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CuaHangId")
                        .HasColumnType("int");

                    b.Property<int?>("Gia")
                        .HasColumnType("int");

                    b.Property<int>("LoaiSPId")
                        .HasColumnType("int");

                    b.Property<byte[]>("PicData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Ten")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CuaHangId");

                    b.HasIndex("LoaiSPId");

                    b.ToTable("SAN_PHAM");
                });

            modelBuilder.Entity("Aram.Models.TaiKhoan", b =>
                {
                    b.Property<string>("TenTK")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<string>("HoTen")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LoaiTK")
                        .HasColumnType("bit");

                    b.Property<string>("MatKhau")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("NgayTao")
                        .HasColumnType("date");

                    b.Property<string>("SoDT")
                        .HasColumnType("char(10)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("TenTK");

                    b.ToTable("TAI_KHOAN");
                });

            modelBuilder.Entity("Aram.Models.ThongTin_NhanHang", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DiaChi")
                        .HasColumnType("ntext");

                    b.Property<int>("DonHangId")
                        .HasColumnType("int");

                    b.Property<string>("GhiChu")
                        .HasColumnType("ntext");

                    b.Property<string>("HoTen")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SoDT")
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("DonHangId")
                        .IsUnique();

                    b.ToTable("THONGTIN_NHANHANG");
                });

            modelBuilder.Entity("Aram.Models.CuaHang", b =>
                {
                    b.HasOne("Aram.Models.TaiKhoan", "TaiKhoan")
                        .WithMany("CuaHangs")
                        .HasForeignKey("TaiKhoanTenTK");

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("Aram.Models.DonHang", b =>
                {
                    b.HasOne("Aram.Models.TaiKhoan", "TaiKhoan")
                        .WithMany("DonHangs")
                        .HasForeignKey("TaiKhoanTenTK");

                    b.Navigation("TaiKhoan");
                });

            modelBuilder.Entity("Aram.Models.DonHang_ChiTiet", b =>
                {
                    b.HasOne("Aram.Models.DonHang", "DonHang")
                        .WithMany("DonHang_ChiTiets")
                        .HasForeignKey("DonHangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aram.Models.SanPham", "SanPham")
                        .WithMany()
                        .HasForeignKey("SanPhamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonHang");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("Aram.Models.SanPham", b =>
                {
                    b.HasOne("Aram.Models.CuaHang", "CuaHang")
                        .WithMany("SanPhams")
                        .HasForeignKey("CuaHangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aram.Models.LoaiSP", "LoaiSP")
                        .WithMany("SanPhams")
                        .HasForeignKey("LoaiSPId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CuaHang");

                    b.Navigation("LoaiSP");
                });

            modelBuilder.Entity("Aram.Models.ThongTin_NhanHang", b =>
                {
                    b.HasOne("Aram.Models.DonHang", "DonHang")
                        .WithOne("TT_NH")
                        .HasForeignKey("Aram.Models.ThongTin_NhanHang", "DonHangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DonHang");
                });

            modelBuilder.Entity("Aram.Models.CuaHang", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("Aram.Models.DonHang", b =>
                {
                    b.Navigation("DonHang_ChiTiets");

                    b.Navigation("TT_NH");
                });

            modelBuilder.Entity("Aram.Models.LoaiSP", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("Aram.Models.TaiKhoan", b =>
                {
                    b.Navigation("CuaHangs");

                    b.Navigation("DonHangs");
                });
#pragma warning restore 612, 618
        }
    }
}