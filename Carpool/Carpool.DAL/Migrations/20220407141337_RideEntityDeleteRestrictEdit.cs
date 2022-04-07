using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carpool.DAL.Migrations
{
    public partial class RideEntityDeleteRestrictEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rides_CarId",
                table: "Rides");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_CarId",
                table: "Rides",
                column: "CarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rides_CarId",
                table: "Rides");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_CarId",
                table: "Rides",
                column: "CarId",
                unique: true);
        }
    }
}
