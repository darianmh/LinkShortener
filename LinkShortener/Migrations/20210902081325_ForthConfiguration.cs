using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkShortener.Migrations
{
    public partial class ForthConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalVisitCount",
                table: "Links",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalVisitCount",
                table: "Links");
        }
    }
}
