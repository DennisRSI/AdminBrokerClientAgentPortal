using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    public partial class OldCodeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OldCodeId",
                table: "UsedCodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OldCodeId",
                table: "UnusedCodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OldCodeId",
                table: "PendingCodes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldCodeId",
                table: "UsedCodes");

            migrationBuilder.DropColumn(
                name: "OldCodeId",
                table: "UnusedCodes");

            migrationBuilder.DropColumn(
                name: "OldCodeId",
                table: "PendingCodes");
        }
    }
}
