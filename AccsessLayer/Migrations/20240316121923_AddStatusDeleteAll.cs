using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccsessLayer.Migrations
{
    public partial class AddStatusDeleteAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_SoCVs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_PTNhans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_PhongBans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_NhanViens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_MDMats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_MDKhans",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_LoaiSoCVs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_LinhVuc",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_Khos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_Kes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_Hops",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_DMCV_CVs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_DanhMucCVs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_CVDIs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_CVDENs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_CoQuanBHs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Ghichu",
                table: "tb_ChucVus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_ChucVus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TrangThai_Xoa",
                table: "tb_BoPhans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_SoCVs");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_PTNhans");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_PhongBans");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_NhanViens");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_MDMats");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_MDKhans");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_LoaiSoCVs");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_LinhVuc");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_Khos");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_Kes");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_Hops");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_DMCV_CVs");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_DanhMucCVs");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_CVDIs");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_CVDENs");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_CoQuanBHs");

            migrationBuilder.DropColumn(
                name: "Ghichu",
                table: "tb_ChucVus");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_ChucVus");

            migrationBuilder.DropColumn(
                name: "TrangThai_Xoa",
                table: "tb_BoPhans");
        }
    }
}
