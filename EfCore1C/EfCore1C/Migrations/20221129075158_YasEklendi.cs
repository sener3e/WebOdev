using Microsoft.EntityFrameworkCore.Migrations;

namespace EfCore1C.Migrations
{
    public partial class YasEklendi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "YazarYas",
                table: "Yazarlar",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YazarYas",
                table: "Yazarlar");
        }
    }
}
