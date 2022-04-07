using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carpool.DAL.Migrations
{
    public partial class ModelBuilderDateOnlyFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPhotos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserPhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPhotos", x => x.Id);
                });
        }
    }
}
