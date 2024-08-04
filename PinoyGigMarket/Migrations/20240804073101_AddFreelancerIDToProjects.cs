using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PinoyGigMarket.Migrations
{
    public partial class AddFreelancerIDToProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ClientID",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "FreelancerID",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_FreelancerID",
                table: "Projects",
                column: "FreelancerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ClientID",
                table: "Projects",
                column: "ClientID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_FreelancerID",
                table: "Projects",
                column: "FreelancerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ClientID",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_FreelancerID",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_FreelancerID",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FreelancerID",
                table: "Projects");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ClientID",
                table: "Projects",
                column: "ClientID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
