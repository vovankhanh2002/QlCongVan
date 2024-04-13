using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccsessLayer.Migrations
{
    public partial class Update_CVDI_File : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoiNhan_BL",
                table: "tb_CVDIs");

            migrationBuilder.DropColumn(
                name: "SLBanluu_CVDI",
                table: "tb_CVDIs");

            migrationBuilder.AlterColumn<bool>(
                name: "TrangThai_CVDI",
                table: "tb_CVDIs",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "File_CVDI",
                table: "tb_CVDIs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "TrangThai_CVDI",
                table: "tb_CVDIs",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "File_CVDI",
                table: "tb_CVDIs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoiNhan_BL",
                table: "tb_CVDIs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SLBanluu_CVDI",
                table: "tb_CVDIs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
