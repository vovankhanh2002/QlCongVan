﻿// <auto-generated />
using System;
using AccsessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccsessLayer.Migrations
{
    [DbContext(typeof(DBContextCV))]
    [Migration("20240317102745_AddIdenTity")]
    partial class AddIdenTity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataLayer.Model.Tb_BoPhan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("SoNguoi_BP")
                        .HasColumnType("int");

                    b.Property<string>("TenLD_BP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_BP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("tb_BoPhans");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_ChucVu", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Ghichu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_CV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("tb_ChucVus");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_CoQuanBH", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Ghichu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenCoQuan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("tb_CoQuanBHs");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_CVDEN", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("File_CVDEN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GhiChu_CVDEN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("HanTL_CVDEN")
                        .HasColumnType("datetime2");

                    b.Property<int>("ID_CoQuanBH")
                        .HasColumnType("int");

                    b.Property<int>("ID_LV")
                        .HasColumnType("int");

                    b.Property<int>("ID_LVB")
                        .HasColumnType("int");

                    b.Property<int>("ID_MDKhan")
                        .HasColumnType("int");

                    b.Property<int>("ID_MDMat")
                        .HasColumnType("int");

                    b.Property<int>("ID_NV")
                        .HasColumnType("int");

                    b.Property<int>("ID_PTNhan")
                        .HasColumnType("int");

                    b.Property<int?>("ID_PhongBan")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("ID_SoCV")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayBH_CVDEN")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayNhan_CVDEN")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhanCongXLVB_CVDEN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SLTrang_CVDEN")
                        .HasColumnType("int");

                    b.Property<int>("SL_BPH")
                        .HasColumnType("int");

                    b.Property<string>("Skh_CVDEN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_CVDI")
                        .HasColumnType("bit");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.Property<string>("TrichYeu_CVDEN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ID_CoQuanBH");

                    b.HasIndex("ID_LV");

                    b.HasIndex("ID_LVB");

                    b.HasIndex("ID_MDKhan");

                    b.HasIndex("ID_MDMat");

                    b.HasIndex("ID_NV");

                    b.HasIndex("ID_PTNhan");

                    b.HasIndex("ID_PhongBan");

                    b.HasIndex("ID_SoCV");

                    b.ToTable("tb_CVDENs");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_CVDI", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("File_CVDI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_LV")
                        .HasColumnType("int");

                    b.Property<int>("ID_LVB")
                        .HasColumnType("int");

                    b.Property<int>("ID_MDKhan")
                        .HasColumnType("int");

                    b.Property<int>("ID_MDMat")
                        .HasColumnType("int");

                    b.Property<int>("ID_NV")
                        .HasColumnType("int");

                    b.Property<int?>("ID_PhongBan")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("ID_SoCV")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayBH_CVDI")
                        .HasColumnType("datetime2");

                    b.Property<string>("NoiNhan_BL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SLBanluu_CVDI")
                        .HasColumnType("int");

                    b.Property<int>("SLTrang_CVDI")
                        .HasColumnType("int");

                    b.Property<int>("SL_BPH")
                        .HasColumnType("int");

                    b.Property<int>("Skh_CVDI")
                        .HasColumnType("int");

                    b.Property<bool>("TrangThai_CVDI")
                        .HasColumnType("bit");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.Property<string>("TrichYeu_CVDI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ID_LV");

                    b.HasIndex("ID_LVB");

                    b.HasIndex("ID_MDKhan");

                    b.HasIndex("ID_MDMat");

                    b.HasIndex("ID_NV");

                    b.HasIndex("ID_PhongBan");

                    b.HasIndex("ID_SoCV");

                    b.ToTable("tb_CVDIs");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_DanhMucCV", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("ID_Hop")
                        .HasColumnType("int");

                    b.Property<int?>("ID_Ke")
                        .HasColumnType("int");

                    b.Property<int?>("ID_Kho")
                        .HasColumnType("int");

                    b.Property<string>("Ma_HS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_HS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("ID_Hop");

                    b.HasIndex("ID_Ke");

                    b.HasIndex("ID_Kho");

                    b.ToTable("tb_DanhMucCVs");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_DMCV_CV", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("ID_CVDEN")
                        .HasColumnType("int");

                    b.Property<int?>("ID_CVDi")
                        .HasColumnType("int");

                    b.Property<int?>("ID_DanhMucCV")
                        .HasColumnType("int");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("ID_CVDEN");

                    b.HasIndex("ID_CVDi");

                    b.HasIndex("ID_DanhMucCV");

                    b.ToTable("tb_DMCV_CVs");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_Hop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ID_Ke")
                        .HasColumnType("int");

                    b.Property<int?>("ID_Kho")
                        .HasColumnType("int");

                    b.Property<string>("Ten_Hop")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ID_Ke");

                    b.HasIndex("ID_Kho");

                    b.ToTable("tb_Hops");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_Ke", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_Ke")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("tb_Kes");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_Kho", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_Kho")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("tb_Khos");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_LinhVuc", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Ghichu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_LV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("tb_LinhVuc");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_LoaiSoCV", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_LSCV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("tb_LoaiSoCVs");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_LoaiVB", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_LVB")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("tb_LoaiVBs");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_MDKhan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_MDKhan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("tb_MDKhans");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_MDMat", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_MDMat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("tb_MDMats");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_NhanVien", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("DiaChi_NV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hoten_NV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_ChucVu")
                        .HasColumnType("int");

                    b.Property<int>("ID_PhongBan")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgaySinh_NV")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SDT_NV")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("ID_ChucVu");

                    b.HasIndex("ID_PhongBan");

                    b.ToTable("tb_NhanViens");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_PhongBan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_PB")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("tb_PhongBans");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_PTNhan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ten_PTNhan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("tb_PTNhans");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_SoCV", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Ghichu")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_LSCV")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Ngay_SoCV")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ten_SoCV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.Property<bool>("TrangThai_Xoa")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.HasIndex("ID_LSCV");

                    b.ToTable("tb_SoCVs");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DataLayer.Model.Tb_CVDEN", b =>
                {
                    b.HasOne("DataLayer.Model.Tb_CoQuanBH", "Tb_CoQuanBH")
                        .WithMany()
                        .HasForeignKey("ID_CoQuanBH")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_LinhVuc", "Tb_LinhVuc")
                        .WithMany()
                        .HasForeignKey("ID_LV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_LoaiVB", "Tb_LoaiVB")
                        .WithMany()
                        .HasForeignKey("ID_LVB")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_MDKhan", "Tb_MDKhan")
                        .WithMany()
                        .HasForeignKey("ID_MDKhan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_MDMat", "Tb_MDMat")
                        .WithMany()
                        .HasForeignKey("ID_MDMat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_NhanVien", "Tb_NhanVien")
                        .WithMany()
                        .HasForeignKey("ID_NV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_PTNhan", "Tb_PTNhan")
                        .WithMany()
                        .HasForeignKey("ID_PTNhan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_PhongBan", "Tb_PhongBan")
                        .WithMany()
                        .HasForeignKey("ID_PhongBan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_SoCV", "tb_SoCV")
                        .WithMany()
                        .HasForeignKey("ID_SoCV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tb_CoQuanBH");

                    b.Navigation("Tb_LinhVuc");

                    b.Navigation("Tb_LoaiVB");

                    b.Navigation("Tb_MDKhan");

                    b.Navigation("Tb_MDMat");

                    b.Navigation("Tb_NhanVien");

                    b.Navigation("Tb_PTNhan");

                    b.Navigation("Tb_PhongBan");

                    b.Navigation("tb_SoCV");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_CVDI", b =>
                {
                    b.HasOne("DataLayer.Model.Tb_LinhVuc", "Tb_LinhVuc")
                        .WithMany()
                        .HasForeignKey("ID_LV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_LoaiVB", "Tb_LoaiVB")
                        .WithMany()
                        .HasForeignKey("ID_LVB")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_MDKhan", "Tb_MDKhan")
                        .WithMany()
                        .HasForeignKey("ID_MDKhan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_MDMat", "Tb_MDMat")
                        .WithMany()
                        .HasForeignKey("ID_MDMat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_NhanVien", "Tb_NhanVien")
                        .WithMany()
                        .HasForeignKey("ID_NV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_PhongBan", "Tb_PhongBan")
                        .WithMany()
                        .HasForeignKey("ID_PhongBan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_SoCV", "tb_SoCV")
                        .WithMany()
                        .HasForeignKey("ID_SoCV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tb_LinhVuc");

                    b.Navigation("Tb_LoaiVB");

                    b.Navigation("Tb_MDKhan");

                    b.Navigation("Tb_MDMat");

                    b.Navigation("Tb_NhanVien");

                    b.Navigation("Tb_PhongBan");

                    b.Navigation("tb_SoCV");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_DanhMucCV", b =>
                {
                    b.HasOne("DataLayer.Model.Tb_Hop", "Tb_Hop")
                        .WithMany()
                        .HasForeignKey("ID_Hop");

                    b.HasOne("DataLayer.Model.Tb_Ke", "Tb_Ke")
                        .WithMany()
                        .HasForeignKey("ID_Ke");

                    b.HasOne("DataLayer.Model.Tb_Kho", "Tb_Kho")
                        .WithMany()
                        .HasForeignKey("ID_Kho");

                    b.Navigation("Tb_Hop");

                    b.Navigation("Tb_Ke");

                    b.Navigation("Tb_Kho");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_DMCV_CV", b =>
                {
                    b.HasOne("DataLayer.Model.Tb_CVDEN", "Tb_CVDEN")
                        .WithMany()
                        .HasForeignKey("ID_CVDEN");

                    b.HasOne("DataLayer.Model.Tb_CVDI", "Tb_CVDI")
                        .WithMany()
                        .HasForeignKey("ID_CVDi");

                    b.HasOne("DataLayer.Model.Tb_DanhMucCV", "Tb_DanhMucCV")
                        .WithMany()
                        .HasForeignKey("ID_DanhMucCV");

                    b.Navigation("Tb_CVDEN");

                    b.Navigation("Tb_CVDI");

                    b.Navigation("Tb_DanhMucCV");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_Hop", b =>
                {
                    b.HasOne("DataLayer.Model.Tb_Ke", "Tb_Ke")
                        .WithMany()
                        .HasForeignKey("ID_Ke");

                    b.HasOne("DataLayer.Model.Tb_Kho", "Tb_Kho")
                        .WithMany()
                        .HasForeignKey("ID_Kho");

                    b.Navigation("Tb_Ke");

                    b.Navigation("Tb_Kho");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_NhanVien", b =>
                {
                    b.HasOne("DataLayer.Model.Tb_ChucVu", "Tb_ChucVu")
                        .WithMany()
                        .HasForeignKey("ID_ChucVu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Model.Tb_PhongBan", "Tb_PhongBan")
                        .WithMany()
                        .HasForeignKey("ID_PhongBan")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tb_ChucVu");

                    b.Navigation("Tb_PhongBan");
                });

            modelBuilder.Entity("DataLayer.Model.Tb_SoCV", b =>
                {
                    b.HasOne("DataLayer.Model.Tb_LoaiSoCV", "Tb_LoaiSoCV")
                        .WithMany()
                        .HasForeignKey("ID_LSCV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tb_LoaiSoCV");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
