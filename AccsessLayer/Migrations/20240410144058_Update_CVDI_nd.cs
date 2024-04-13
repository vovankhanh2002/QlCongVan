using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccsessLayer.Migrations
{
    public partial class Update_CVDI_nd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_CVDIs_tb_NhanViens_ID_NV",
                table: "tb_CVDIs");

            migrationBuilder.DropIndex(
                name: "IX_tb_CVDIs_ID_NV",
                table: "tb_CVDIs");

            migrationBuilder.DropColumn(
                name: "ID_NV",
                table: "tb_CVDIs");

            migrationBuilder.AddColumn<string>(
                name: "ID_ND",
                table: "tb_CVDIs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDIs_ID_ND",
                table: "tb_CVDIs",
                column: "ID_ND");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_CVDIs_AspNetUsers_ID_ND",
                table: "tb_CVDIs",
                column: "ID_ND",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_CVDIs_AspNetUsers_ID_ND",
                table: "tb_CVDIs");

            migrationBuilder.DropIndex(
                name: "IX_tb_CVDIs_ID_ND",
                table: "tb_CVDIs");

            migrationBuilder.DropColumn(
                name: "ID_ND",
                table: "tb_CVDIs");

            migrationBuilder.AddColumn<int>(
                name: "ID_NV",
                table: "tb_CVDIs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDIs_ID_NV",
                table: "tb_CVDIs",
                column: "ID_NV");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_CVDIs_tb_NhanViens_ID_NV",
                table: "tb_CVDIs",
                column: "ID_NV",
                principalTable: "tb_NhanViens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
