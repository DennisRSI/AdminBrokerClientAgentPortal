using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    public partial class campaignFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "UnusedCodes");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "UnusedCodes");

            migrationBuilder.DropColumn(
                name: "NumberOfUses",
                table: "UnusedCodes");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "UnusedCodes");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "UnusedCodes");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "UnusedCodes");

            migrationBuilder.DropColumn(
                name: "VerifyEmail",
                table: "UnusedCodes");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Campaigns",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfUses",
                table: "Campaigns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Points",
                table: "Campaigns",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VerifyEmail",
                table: "Campaigns",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "NumberOfUses",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "VerifyEmail",
                table: "Campaigns");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "UnusedCodes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "UnusedCodes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfUses",
                table: "UnusedCodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "UnusedCodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Points",
                table: "UnusedCodes",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "UnusedCodes",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VerifyEmail",
                table: "UnusedCodes",
                nullable: false,
                defaultValue: false);
        }
    }
}
