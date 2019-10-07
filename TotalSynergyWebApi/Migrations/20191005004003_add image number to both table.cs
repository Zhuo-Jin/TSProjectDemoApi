using Microsoft.EntityFrameworkCore.Migrations;

namespace TotalSynergyWebApi.Migrations
{
    public partial class addimagenumbertobothtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageNumber",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageNumber",
                table: "Contacts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageNumber",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ImageNumber",
                table: "Contacts");
        }
    }
}
