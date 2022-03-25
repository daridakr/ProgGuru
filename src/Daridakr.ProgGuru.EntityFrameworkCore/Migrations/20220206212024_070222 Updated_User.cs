using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daridakr.ProgGuru.Migrations
{
    public partial class _070222Updated_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BeginningYear",
                table: "AbpUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "AbpUsers",
                type: "nvarchar(3780)",
                maxLength: 3780,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "BornDate",
                table: "AbpUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "BornLocation",
                table: "AbpUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "AbpUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CurrentLocation",
                table: "AbpUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsBusy",
                table: "AbpUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProfileCoverPictureUrl",
                table: "AbpUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AbpUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BeginningYear",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Bio",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "BornDate",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "BornLocation",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "CurrentLocation",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "IsBusy",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "ProfileCoverPictureUrl",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AbpUsers");
        }
    }
}
