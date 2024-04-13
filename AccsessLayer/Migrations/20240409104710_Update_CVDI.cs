using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccsessLayer.Migrations
{
    public partial class Update_CVDI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nguoinhan_CVDI",
                table: "tb_CVDIs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nguoigui_CVDEN",
                table: "tb_CVDENs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nguoinhan_CVDI",
                table: "tb_CVDIs");

            migrationBuilder.DropColumn(
                name: "Nguoigui_CVDEN",
                table: "tb_CVDENs");
        }
    }
}
