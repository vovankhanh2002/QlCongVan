using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccsessLayer.Migrations
{
    public partial class Update_CVDI_tenFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File_CVDEN",
                table: "tb_CVDIs",
                newName: "File_CVDI");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "File_CVDI",
                table: "tb_CVDIs",
                newName: "File_CVDEN");
        }
    }
}
