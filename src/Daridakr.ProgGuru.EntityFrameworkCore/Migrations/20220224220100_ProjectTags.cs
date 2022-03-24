using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daridakr.ProgGuru.Migrations
{
    public partial class ProjectTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectTags_ProjectId_TagId",
                table: "ProjectTags");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "ProjectTags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "ProjectTags",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "ProjectTags");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "ProjectTags");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTags_ProjectId_TagId",
                table: "ProjectTags",
                columns: new[] { "ProjectId", "TagId" });
        }
    }
}
