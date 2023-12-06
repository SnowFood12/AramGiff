﻿// <auto-generated />
using System;
using Aram.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aram.Migrations
{
    [DbContext(typeof(AramContext))]
    partial class AramContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("NgayTaoCuaHang")
                        .HasColumnType("Date");

                    b.Property<string>("SoDT")
                        .HasColumnType("char(10)");

                    b.HasKey("Id");

                    b.ToTable("CUA_HANG");
                });

            modelBuilder.Entity("Aram.Models.LoaiSP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
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
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Ten")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CuaHangId");

                    b.HasIndex("LoaiSPId");

                    b.ToTable("SAN_PHAM");
                });

            modelBuilder.Entity("Aram.Models.SanPham", b =>
                {
                    b.HasOne("Aram.Models.CuaHang", "CuaHang")
                        .WithMany("SanPham")
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
                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("Aram.Models.LoaiSP", b =>
                {
                    b.Navigation("SanPhams");
                });
#pragma warning restore 612, 618
        }
    }
}
