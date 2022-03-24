using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daridakr.ProgGuru.Migrations
{
    public partial class SocialNetworks_For_User_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBusy",
                table: "AbpUsers");

            migrationBuilder.AddColumn<string>(
                name: "DiscordCode",
                table: "AbpUsers",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GithubUsername",
                table: "AbpUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "AbpUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TelegramUsername",
                table: "AbpUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropColumn(
                name: "DiscordCode",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "GithubUsername",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "TelegramUsername",
                table: "AbpUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsBusy",
                table: "AbpUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
