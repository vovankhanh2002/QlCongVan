using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccsessLayer.Migrations
{
    public partial class Update_CVDI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File_CVDI",
                table: "tb_CVDIs");

            migrationBuilder.DropColumn(
                name: "part",
                table: "tb_CVDENs");

            migrationBuilder.AddColumn<byte[]>(
                name: "File_CVDEN",
                table: "tb_CVDIs",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File_CVDEN",
                table: "tb_CVDIs");

            migrationBuilder.AddColumn<string>(
                name: "File_CVDI",
                table: "tb_CVDIs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "part",
                table: "tb_CVDENs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
