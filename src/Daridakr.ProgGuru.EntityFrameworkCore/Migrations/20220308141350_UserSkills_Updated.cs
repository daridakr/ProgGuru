using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Daridakr.ProgGuru.Migrations
{
    public partial class UserSkills_Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LanguageUserSkills_CommonUserSkills_Id",
                table: "LanguageUserSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfUserSkills_CommonUserSkills_Id",
                table: "ProfUserSkills");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "ProfUserSkills",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "ProfUserSkills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "ProfUserSkills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "ProfUserSkills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "ProfUserSkills",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "ProfUserSkills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProfUserSkills",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "LanguageUserSkills",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "LanguageUserSkills",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "LanguageUserSkills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "LanguageUserSkills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "LanguageUserSkills",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "LanguageUserSkills",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LanguageUserSkills",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "ProfUserSkills");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "ProfUserSkills");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "ProfUserSkills");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "ProfUserSkills");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "ProfUserSkills");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "ProfUserSkills");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProfUserSkills");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "LanguageUserSkills");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "LanguageUserSkills");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "LanguageUserSkills");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "LanguageUserSkills");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "LanguageUserSkills");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "LanguageUserSkills");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "LanguageUserSkills");

            migrationBuilder.AddForeignKey(
                name: "FK_LanguageUserSkills_CommonUserSkills_Id",
                table: "LanguageUserSkills",
                column: "Id",
                principalTable: "CommonUserSkills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfUserSkills_CommonUserSkills_Id",
                table: "ProfUserSkills",
                column: "Id",
                principalTable: "CommonUserSkills",
                principalColumn: "Id");
        }
    }
}
