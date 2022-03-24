using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daridakr.ProgGuru.Migrations
{
    public partial class BlogPosts_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "CmsBlogPosts",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_CmsBlogPosts_Slug_BlogId",
                table: "CmsBlogPosts",
                newName: "IX_CmsBlogPosts_Slug_GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "CmsBlogPosts",
                newName: "BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_CmsBlogPosts_Slug_GroupId",
                table: "CmsBlogPosts",
                newName: "IX_CmsBlogPosts_Slug_BlogId");
        }
    }
}
