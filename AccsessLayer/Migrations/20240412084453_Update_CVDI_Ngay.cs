using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccsessLayer.Migrations
{
    public partial class Update_CVDI_Ngay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Noigui_CVDEN",
                table: "tb_CVDIs",
                newName: "Noigui_CVDI");

            migrationBuilder.AddColumn<DateTime>(
                name: "ngay",
                table: "tb_CVDIs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ngay",
                table: "tb_CVDIs");

            migrationBuilder.RenameColumn(
                name: "Noigui_CVDI",
                table: "tb_CVDIs",
                newName: "Noigui_CVDEN");
        }
    }
}
