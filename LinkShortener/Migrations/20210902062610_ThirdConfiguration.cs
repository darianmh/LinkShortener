using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkShortener.Migrations
{
    public partial class ThirdConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HeaderText",
                table: "Links",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Links",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LinkTitle",
                table: "Links",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeaderText",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "LinkTitle",
                table: "Links");
        }
    }
}
