using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccsessLayer.Migrations
{
    public partial class AddDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_BoPhans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_BP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenLD_BP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoNguoi_BP = table.Column<int>(type: "int", nullable: false)
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
                    Ten_CV = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ChucVus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_CoQuanBHs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCoQuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_CoQuanBHs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_Kes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_Ke = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Ghichu = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Ten_LVB = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_MDMats", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_PhongBans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_PB = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_PhongBans", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_PTNhans",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_PTNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_PTNhans", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tb_Hops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten_Hop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    TrangThai = table.Column<bool>(type: "bit", nullable: true),
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
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hoten_NV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi_NV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT_NV = table.Column<int>(type: "int", nullable: false),
                    NgaySinh_NV = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ID_ChucVu = table.Column<int>(type: "int", nullable: false),
                    ID_PhongBan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_NhanViens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_NhanViens_tb_ChucVus_ID_ChucVu",
                        column: x => x.ID_ChucVu,
                        principalTable: "tb_ChucVus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_NhanViens_tb_PhongBans_ID_PhongBan",
                        column: x => x.ID_PhongBan,
                        principalTable: "tb_PhongBans",
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
                    TrichYeu_CVDEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HanTL_CVDEN = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GhiChu_CVDEN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhanCongXLVB_CVDEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    File_CVDEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai_CVDI = table.Column<bool>(type: "bit", nullable: false),
                    ID_LVB = table.Column<int>(type: "int", nullable: false),
                    ID_NV = table.Column<int>(type: "int", nullable: false),
                    ID_MDMat = table.Column<int>(type: "int", nullable: false),
                    ID_MDKhan = table.Column<int>(type: "int", nullable: false),
                    ID_PTNhan = table.Column<int>(type: "int", nullable: false),
                    ID_SoCV = table.Column<int>(type: "int", nullable: false),
                    ID_LV = table.Column<int>(type: "int", nullable: false),
                    ID_CoQuanBH = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_CVDENs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tb_CVDENs_tb_CoQuanBHs_ID_CoQuanBH",
                        column: x => x.ID_CoQuanBH,
                        principalTable: "tb_CoQuanBHs",
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
                        principalColumn: "ID",
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
                    TrangThai_CVDI = table.Column<bool>(type: "bit", nullable: false),
                    ID_LVB = table.Column<int>(type: "int", nullable: false),
                    ID_NV = table.Column<int>(type: "int", nullable: false),
                    ID_MDMat = table.Column<int>(type: "int", nullable: false),
                    ID_MDKhan = table.Column<int>(type: "int", nullable: false),
                    ID_SoCV = table.Column<int>(type: "int", nullable: false),
                    ID_LV = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_CVDIs", x => x.ID);
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
                        principalColumn: "ID",
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
                name: "IX_tb_CVDENs_ID_CoQuanBH",
                table: "tb_CVDENs",
                column: "ID_CoQuanBH");

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
                name: "tb_BoPhans");

            migrationBuilder.DropTable(
                name: "tb_DMCV_CVs");

            migrationBuilder.DropTable(
                name: "tb_CVDENs");

            migrationBuilder.DropTable(
                name: "tb_CVDIs");

            migrationBuilder.DropTable(
                name: "tb_DanhMucCVs");

            migrationBuilder.DropTable(
                name: "tb_CoQuanBHs");

            migrationBuilder.DropTable(
                name: "tb_PTNhans");

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
                name: "tb_PhongBans");

            migrationBuilder.DropTable(
                name: "tb_LoaiSoCVs");

            migrationBuilder.DropTable(
                name: "tb_Kes");

            migrationBuilder.DropTable(
                name: "tb_Khos");
        }
    }
}
