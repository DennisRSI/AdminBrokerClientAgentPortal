using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes.Service.Data.Migrations
{
    public partial class PendingAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "PendingCodes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatorIP",
                table: "PendingCodes",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationDate",
                table: "PendingCodes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PendingCodes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "PendingCodes",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "PendingCodes");

            migrationBuilder.DropColumn(
                name: "CreatorIP",
                table: "PendingCodes");

            migrationBuilder.DropColumn(
                name: "DeactivationDate",
                table: "PendingCodes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PendingCodes");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "PendingCodes");
        }
    }
}
