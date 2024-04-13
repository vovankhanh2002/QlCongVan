using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccsessLayer.Migrations
{
    public partial class Update_CVDEN_ID_ND : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_CVDENs_AspNetUsers_ID_NV",
                table: "tb_CVDENs");

            migrationBuilder.DropIndex(
                name: "IX_tb_CVDENs_ID_NV",
                table: "tb_CVDENs");

            migrationBuilder.DropColumn(
                name: "ID_NV",
                table: "tb_CVDENs");

            migrationBuilder.AlterColumn<string>(
                name: "ID_ND",
                table: "tb_CVDENs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDENs_ID_ND",
                table: "tb_CVDENs",
                column: "ID_ND");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_CVDENs_AspNetUsers_ID_ND",
                table: "tb_CVDENs",
                column: "ID_ND",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_CVDENs_AspNetUsers_ID_ND",
                table: "tb_CVDENs");

            migrationBuilder.DropIndex(
                name: "IX_tb_CVDENs_ID_ND",
                table: "tb_CVDENs");

            migrationBuilder.AlterColumn<string>(
                name: "ID_ND",
                table: "tb_CVDENs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ID_NV",
                table: "tb_CVDENs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDENs_ID_NV",
                table: "tb_CVDENs",
                column: "ID_NV");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_CVDENs_AspNetUsers_ID_NV",
                table: "tb_CVDENs",
                column: "ID_NV",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
