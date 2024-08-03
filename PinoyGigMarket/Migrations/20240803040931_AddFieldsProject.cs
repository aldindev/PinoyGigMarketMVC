using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinoyGigMarket.Migrations
{
    public partial class AddFieldsProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GigPostPicturePath",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GigPostPicturePath",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Projects");
        }
    }
}
