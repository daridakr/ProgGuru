using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daridakr.ProgGuru.Migrations
{
    public partial class DefaultValue_For_ProfilePictureUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProfilePictureUrl",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "https://res.cloudinary.com/dgvd7jieo/image/upload/v1644954588/oazu4v5a7nsaxqtjd0mg.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProfilePictureUrl",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "https://res.cloudinary.com/dgvd7jieo/image/upload/v1644954588/oazu4v5a7nsaxqtjd0mg.jpg");
        }
    }
}
