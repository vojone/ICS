using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carpool.DAL.Migrations
{
    public partial class EntityRideEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Users_UserId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Locations_ArrivalLId",
                table: "Rides");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Locations_DepartureLId",
                table: "Rides");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Users_DriverId",
                table: "Rides");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Rides_ArrivalLId",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Rides_DepartureLId",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "ArrivalLId",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "DepartureLId",
                table: "Rides");

            migrationBuilder.AddColumn<string>(
                name: "ArrivalL",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DepartureL",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Users_UserId",
                table: "Participants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Users_DriverId",
                table: "Rides",
                column: "DriverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Users_UserId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Users_DriverId",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "ArrivalL",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "DepartureL",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rides");

            migrationBuilder.AddColumn<Guid>(
                name: "ArrivalLId",
                table: "Rides",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DepartureLId",
                table: "Rides",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rides_ArrivalLId",
                table: "Rides",
                column: "ArrivalLId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rides_DepartureLId",
                table: "Rides",
                column: "DepartureLId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Users_UserId",
                table: "Participants",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Locations_ArrivalLId",
                table: "Rides",
                column: "ArrivalLId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Locations_DepartureLId",
                table: "Rides",
                column: "DepartureLId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Users_DriverId",
                table: "Rides",
                column: "DriverId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
