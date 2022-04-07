using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carpool.DAL.Migrations
{
    public partial class UserPhotoEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserPhotos_PhotoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PhotoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhotoId",
                table: "Users",
                column: "PhotoId",
                unique: true,
                filter: "[PhotoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserPhotos_PhotoId",
                table: "Users",
                column: "PhotoId",
                principalTable: "UserPhotos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
