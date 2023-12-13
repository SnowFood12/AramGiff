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
    [Migration("20231212123343_DB")]
    partial class DB
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
                        .IsRequired()
                        .HasColumnType("ntext");

                    b.Property<string>("LinkMap")
                        .HasColumnType("text");

                    b.Property<DateTime>("NgayTaoCuaHang")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoDT")
                        .HasMaxLength(10)
                        .HasColumnType("char(10)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TenTK")
                        .HasColumnType("varchar(15)");

                    b.Property<bool?>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("CUA_HANG");
                });

            modelBuilder.Entity("Aram.Models.LoaiSP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(50)
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
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("LoaiSPId")
                        .HasColumnType("int");

                    b.Property<byte[]>("PicData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

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
                        .HasColumnType("datetime2");

                    b.Property<string>("SoDT")
                        .HasColumnType("char(10)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.HasKey("TenTK");

                    b.ToTable("TAI_KHOAN");
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

            modelBuilder.Entity("Aram.Models.CuaHang", b =>
                {
                    b.Navigation("SanPhams");
                });

            modelBuilder.Entity("Aram.Models.LoaiSP", b =>
                {
                    b.Navigation("SanPhams");
                });
#pragma warning restore 612, 618
        }
    }
}