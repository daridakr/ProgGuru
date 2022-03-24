using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daridakr.ProgGuru.Migrations
{
    public partial class BlogPosts_Creator_Cover_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CmsBlogPosts_CmsUsers_AuthorId",
                table: "CmsBlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_CmsBlogPosts_AuthorId",
                table: "CmsBlogPosts");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "CmsBlogPosts");

            migrationBuilder.DropColumn(
                name: "CoverImageMediaId",
                table: "CmsBlogPosts");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "CmsBlogPosts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverImagePath",
                table: "CmsBlogPosts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImagePath",
                table: "CmsBlogPosts");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatorId",
                table: "CmsBlogPosts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "CmsBlogPosts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CoverImageMediaId",
                table: "CmsBlogPosts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CmsBlogPosts_AuthorId",
                table: "CmsBlogPosts",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CmsBlogPosts_CmsUsers_AuthorId",
                table: "CmsBlogPosts",
                column: "AuthorId",
                principalTable: "CmsUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
