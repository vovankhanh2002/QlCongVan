using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccsessLayer.Migrations
{
    public partial class Update_CVDEN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_PhongBan",
                table: "tb_CVDIs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID_PhongBan",
                table: "tb_CVDENs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SLTrang_CVDEN",
                table: "tb_CVDENs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SL_BPH",
                table: "tb_CVDENs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDIs_ID_PhongBan",
                table: "tb_CVDIs",
                column: "ID_PhongBan");

            migrationBuilder.CreateIndex(
                name: "IX_tb_CVDENs_ID_PhongBan",
                table: "tb_CVDENs",
                column: "ID_PhongBan");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_CVDENs_tb_PhongBans_ID_PhongBan",
                table: "tb_CVDENs",
                column: "ID_PhongBan",
                principalTable: "tb_PhongBans",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_CVDIs_tb_PhongBans_ID_PhongBan",
                table: "tb_CVDIs",
                column: "ID_PhongBan",
                principalTable: "tb_PhongBans",
                principalColumn: "ID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_CVDENs_tb_PhongBans_ID_PhongBan",
                table: "tb_CVDENs");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_CVDIs_tb_PhongBans_ID_PhongBan",
                table: "tb_CVDIs");

            migrationBuilder.DropIndex(
                name: "IX_tb_CVDIs_ID_PhongBan",
                table: "tb_CVDIs");

            migrationBuilder.DropIndex(
                name: "IX_tb_CVDENs_ID_PhongBan",
                table: "tb_CVDENs");

            migrationBuilder.DropColumn(
                name: "ID_PhongBan",
                table: "tb_CVDIs");

            migrationBuilder.DropColumn(
                name: "ID_PhongBan",
                table: "tb_CVDENs");

            migrationBuilder.DropColumn(
                name: "SLTrang_CVDEN",
                table: "tb_CVDENs");

            migrationBuilder.DropColumn(
                name: "SL_BPH",
                table: "tb_CVDENs");
        }
    }
}
