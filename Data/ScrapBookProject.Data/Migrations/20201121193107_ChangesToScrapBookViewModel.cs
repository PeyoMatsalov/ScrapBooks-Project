using Microsoft.EntityFrameworkCore.Migrations;

namespace ScrapBookProject.Data.Migrations
{
    public partial class ChangesToScrapBookViewModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "ScrapBooks");

            migrationBuilder.AddColumn<string>(
                name: "Visibility",
                table: "ScrapBooks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visibility",
                table: "ScrapBooks");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "ScrapBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
