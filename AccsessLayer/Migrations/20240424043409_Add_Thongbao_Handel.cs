using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccsessLayer.Migrations
{
    public partial class Add_Thongbao_Handel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserHandelID",
                table: "tb_Thongbaos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserHandelID",
                table: "tb_Thongbaos");
        }
    }
}
