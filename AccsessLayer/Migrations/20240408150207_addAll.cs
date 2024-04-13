using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccsessLayer.Migrations
{
    public partial class addAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_BoPhans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_BP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenLD_BP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoNguoi_BP = table.Column<int>(type: "int", nullable: false),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_BoPhans", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_ChucVus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_CV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ChucVus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_FileCVDENs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CVDEN = table.Column<int>(type: "int", nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_FileCVDENs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_FileCVDIs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_CVDI = table.Column<int>(type: "int", nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_FileCVDIs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Kes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_Ke = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Kes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Khos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_Kho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Khos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_LinhVuc",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_LV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_LinhVuc", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_LoaiSoCVs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_LSCV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_LoaiSoCVs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_LoaiVBs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_LVB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_LoaiVBs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_MDKhans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_MDKhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_MDKhans", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_MDMats",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_MDMat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_MDMats", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tb_PhongBan",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_PB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_PhongBan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_PTNhans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_PTNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_PTNhans", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Hops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_Hop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false),
                    ID_Kho = table.Column<int>(type: "int", nullable: true),
                    ID_Ke = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Hops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_Hops_tb_Kes_ID_Ke",
                        column: x => x.ID_Ke,
                        principalTable: "tb_Kes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_Hops_tb_Khos_ID_Kho",
                        column: x => x.ID_Kho,
                        principalTable: "tb_Khos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_SoCVs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_SoCV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ngay_SoCV = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false),
                    ID_LSCV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_SoCVs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_SoCVs_tb_LoaiSoCVs_ID_LSCV",
                        column: x => x.ID_LSCV,
                        principalTable: "tb_LoaiSoCVs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_NhanViens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hoten_NV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi_NV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT_NV = table.Column<int>(type: "int", nullable: false),
                    NgaySinh_NV = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_ChucVu = table.Column<int>(type: "int", nullable: false),
                    ID_PhongBan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_NhanViens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_NhanViens_tb_ChucVus_ID_ChucVu",
                        column: x => x.ID_ChucVu,
                        principalTable: "tb_ChucVus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_NhanViens_Tb_PhongBan_ID_PhongBan",
                        column: x => x.ID_PhongBan,
                        principalTable: "Tb_PhongBan",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_DanhMucCVs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma_HS = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten_HS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_Kho = table.Column<int>(type: "int", nullable: true),
                    ID_Ke = table.Column<int>(type: "int", nullable: true),
                    ID_Hop = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_DanhMucCVs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_DanhMucCVs_tb_Hops_ID_Hop",
                        column: x => x.ID_Hop,
                        principalTable: "tb_Hops",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_DanhMucCVs_tb_Kes_ID_Ke",
                        column: x => x.ID_Ke,
                        principalTable: "tb_Kes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_DanhMucCVs_tb_Khos_ID_Kho",
                        column: x => x.ID_Kho,
                        principalTable: "tb_Khos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_CVDENs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Skh_CVDEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayBH_CVDEN = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayNhan_CVDEN = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SLTrang_CVDEN = table.Column<int>(type: "int", nullable: false),
                    SL_BPH = table.Column<int>(type: "int", nullable: false),
                    TrichYeu_CVDEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HanTL_CVDEN = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GhiChu_CVDEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhanCongXLVB_CVDEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    File_CVDEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrangThai_CVDI = table.Column<bool>(type: "bit", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false),
                    ngay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_LVB = table.Column<int>(type: "int", nullable: false),
                    ID_NV = table.Column<int>(type: "int", nullable: false),
                    ID_MDMat = table.Column<int>(type: "int", nullable: false),
                    ID_MDKhan = table.Column<int>(type: "int", nullable: false),
                    ID_PTNhan = table.Column<int>(type: "int", nullable: false),
                    ID_SoCV = table.Column<int>(type: "int", nullable: false),
                    ID_LV = table.Column<int>(type: "int", nullable: false),
                    ID_BP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_CVDENs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_CVDENs_tb_BoPhans_ID_BP",
                        column: x => x.ID_BP,
                        principalTable: "tb_BoPhans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_CVDENs_tb_LinhVuc_ID_LV",
                        column: x => x.ID_LV,
                        principalTable: "tb_LinhVuc",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_CVDENs_tb_LoaiVBs_ID_LVB",
                        column: x => x.ID_LVB,
                        principalTable: "tb_LoaiVBs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_CVDENs_tb_MDKhans_ID_MDKhan",
                        column: x => x.ID_MDKhan,
                        principalTable: "tb_MDKhans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_CVDENs_tb_MDMats_ID_MDMat",
                        column: x => x.ID_MDMat,
                        principalTable: "tb_MDMats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_CVDENs_tb_NhanViens_ID_NV",
                        column: x => x.ID_NV,
                        principalTable: "tb_NhanViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_CVDENs_tb_PTNhans_ID_PTNhan",
                        column: x => x.ID_PTNhan,
                        principalTable: "tb_PTNhans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_CVDENs_tb_SoCVs_ID_SoCV",
                        column: x => x.ID_SoCV,
                        principalTable: "tb_SoCVs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_CVDIs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Skh_CVDI = table.Column<int>(type: "int", nullable: false),
                    NgayBH_CVDI = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrichYeu_CVDI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SL_BPH = table.Column<int>(type: "int", nullable: false),
                    SLTrang_CVDI = table.Column<int>(type: "int", nullable: false),
                    SLBanluu_CVDI = table.Column<int>(type: "int", nullable: false),
                    NoiNhan_BL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    File_CVDI = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai_CVDI = table.Column<bool>(type: "bit", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false),
                    GhiChu_CVDI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_LVB = table.Column<int>(type: "int", nullable: false),
                    ID_NV = table.Column<int>(type: "int", nullable: false),
                    ID_MDMat = table.Column<int>(type: "int", nullable: false),
                    ID_MDKhan = table.Column<int>(type: "int", nullable: false),
                    ID_SoCV = table.Column<int>(type: "int", nullable: false),
                    ID_LV = table.Column<int>(type: "int", nullable: false),
                    ID_BP = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_CVDIs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_CVDIs_tb_BoPhans_ID_BP",
                        column: x => x.ID_BP,
                        principalTable: "tb_BoPhans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_CVDIs_tb_LinhVuc_ID_LV",
                        column: x => x.ID_LV,
                        principalTable: "tb_LinhVuc",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_CVDIs_tb_LoaiVBs_ID_LVB",
                        column: x => x.ID_LVB,
                        principalTable: "tb_LoaiVBs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_CVDIs_tb_MDKhans_ID_MDKhan",
                        column: x => x.ID_MDKhan,
                        principalTable: "tb_MDKhans",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_CVDIs_tb_MDMats_ID_MDMat",
                        column: x => x.ID_MDMat,
                        principalTable: "tb_MDMats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_CVDIs_tb_NhanViens_ID_NV",
                        column: x => x.ID_NV,
                        principalTable: "tb_NhanViens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_CVDIs_tb_SoCVs_ID_SoCV",
                        column: x => x.ID_SoCV,
                        principalTable: "tb_SoCVs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_DMCV_CVs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_DanhMucCV = table.Column<int>(type: "int", nullable: true),
                    TrangThai_Xoa = table.Column<bool>(type: "bit", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_CVDEN = table.Column<int>(type: "int", nullable: true),
                    ID_CVDi = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_DMCV_CVs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_DMCV_CVs_tb_CVDENs_ID_CVDEN",
                        column: x => x.ID_CVDEN,
                        principalTable: "tb_CVDENs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_tb_DMCV_CVs_tb_CVDIs_ID_CVDi",
                        column: x => x.ID_CVDi,
                        principalTable: "tb_CVDIs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_tb_DMCV_CVs_tb_DanhMucCVs_ID_DanhMucCV",
                        column: x => x.ID_DanhMucCV,
                        principalTable: "tb_DanhMucCVs",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDENs_ID_BP",
                table: "tb_CVDENs",
                column: "ID_BP");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDENs_ID_LV",
                table: "tb_CVDENs",
                column: "ID_LV");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDENs_ID_LVB",
                table: "tb_CVDENs",
                column: "ID_LVB");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDENs_ID_MDKhan",
                table: "tb_CVDENs",
                column: "ID_MDKhan");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDENs_ID_MDMat",
                table: "tb_CVDENs",
                column: "ID_MDMat");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDENs_ID_NV",
                table: "tb_CVDENs",
                column: "ID_NV");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDENs_ID_PTNhan",
                table: "tb_CVDENs",
                column: "ID_PTNhan");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDENs_ID_SoCV",
                table: "tb_CVDENs",
                column: "ID_SoCV");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDIs_ID_BP",
                table: "tb_CVDIs",
                column: "ID_BP");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDIs_ID_LV",
                table: "tb_CVDIs",
                column: "ID_LV");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDIs_ID_LVB",
                table: "tb_CVDIs",
                column: "ID_LVB");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDIs_ID_MDKhan",
                table: "tb_CVDIs",
                column: "ID_MDKhan");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDIs_ID_MDMat",
                table: "tb_CVDIs",
                column: "ID_MDMat");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDIs_ID_NV",
                table: "tb_CVDIs",
                column: "ID_NV");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDIs_ID_SoCV",
                table: "tb_CVDIs",
                column: "ID_SoCV");

            migrationBuilder.CreateIndex(
                name: "IX_tb_DanhMucCVs_ID_Hop",
                table: "tb_DanhMucCVs",
                column: "ID_Hop");

            migrationBuilder.CreateIndex(
                name: "IX_tb_DanhMucCVs_ID_Ke",
                table: "tb_DanhMucCVs",
                column: "ID_Ke");

            migrationBuilder.CreateIndex(
                name: "IX_tb_DanhMucCVs_ID_Kho",
                table: "tb_DanhMucCVs",
                column: "ID_Kho");

            migrationBuilder.CreateIndex(
                name: "IX_tb_DMCV_CVs_ID_CVDEN",
                table: "tb_DMCV_CVs",
                column: "ID_CVDEN");

            migrationBuilder.CreateIndex(
                name: "IX_tb_DMCV_CVs_ID_CVDi",
                table: "tb_DMCV_CVs",
                column: "ID_CVDi");

            migrationBuilder.CreateIndex(
                name: "IX_tb_DMCV_CVs_ID_DanhMucCV",
                table: "tb_DMCV_CVs",
                column: "ID_DanhMucCV");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Hops_ID_Ke",
                table: "tb_Hops",
                column: "ID_Ke");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Hops_ID_Kho",
                table: "tb_Hops",
                column: "ID_Kho");

            migrationBuilder.CreateIndex(
                name: "IX_tb_NhanViens_ID_ChucVu",
                table: "tb_NhanViens",
                column: "ID_ChucVu");

            migrationBuilder.CreateIndex(
                name: "IX_tb_NhanViens_ID_PhongBan",
                table: "tb_NhanViens",
                column: "ID_PhongBan");

            migrationBuilder.CreateIndex(
                name: "IX_tb_SoCVs_ID_LSCV",
                table: "tb_SoCVs",
                column: "ID_LSCV");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "tb_DMCV_CVs");

            migrationBuilder.DropTable(
                name: "tb_FileCVDENs");

            migrationBuilder.DropTable(
                name: "tb_FileCVDIs");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tb_CVDENs");

            migrationBuilder.DropTable(
                name: "tb_CVDIs");

            migrationBuilder.DropTable(
                name: "tb_DanhMucCVs");

            migrationBuilder.DropTable(
                name: "tb_PTNhans");

            migrationBuilder.DropTable(
                name: "tb_BoPhans");

            migrationBuilder.DropTable(
                name: "tb_LinhVuc");

            migrationBuilder.DropTable(
                name: "tb_LoaiVBs");

            migrationBuilder.DropTable(
                name: "tb_MDKhans");

            migrationBuilder.DropTable(
                name: "tb_MDMats");

            migrationBuilder.DropTable(
                name: "tb_NhanViens");

            migrationBuilder.DropTable(
                name: "tb_SoCVs");

            migrationBuilder.DropTable(
                name: "tb_Hops");

            migrationBuilder.DropTable(
                name: "tb_ChucVus");

            migrationBuilder.DropTable(
                name: "Tb_PhongBan");

            migrationBuilder.DropTable(
                name: "tb_LoaiSoCVs");

            migrationBuilder.DropTable(
                name: "tb_Kes");

            migrationBuilder.DropTable(
                name: "tb_Khos");
        }
    }
}
