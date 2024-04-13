using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccsessLayer.Migrations
{
    public partial class Update_CVDEN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_CVDENs_tb_NhanViens_ID_NV",
                table: "tb_CVDENs");

            migrationBuilder.AlterColumn<bool>(
                name: "TrangThai_CVDI",
                table: "tb_CVDENs",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ID_NV",
                table: "tb_CVDENs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ID_ND",
                table: "tb_CVDENs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_CVDENs_AspNetUsers_ID_NV",
                table: "tb_CVDENs",
                column: "ID_NV",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_CVDENs_AspNetUsers_ID_NV",
                table: "tb_CVDENs");

            migrationBuilder.DropColumn(
                name: "ID_ND",
                table: "tb_CVDENs");

            migrationBuilder.AlterColumn<bool>(
                name: "TrangThai_CVDI",
                table: "tb_CVDENs",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "ID_NV",
                table: "tb_CVDENs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_CVDENs_tb_NhanViens_ID_NV",
                table: "tb_CVDENs",
                column: "ID_NV",
                principalTable: "tb_NhanViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
