using Microsoft.EntityFrameworkCore.Migrations;

namespace ScrapBookProject.Data.Migrations
{
    public partial class RemovedPagesCountFromScrapBookModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PagesCount",
                table: "ScrapBooks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PagesCount",
                table: "ScrapBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
