using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes.Service.Data.Migrations
{
    public partial class emailverification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CondoRewards",
                table: "Codes",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CodeActivities",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailVerifiedDate",
                table: "CodeActivities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CondoRewards",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "CodeActivities");

            migrationBuilder.DropColumn(
                name: "EmailVerifiedDate",
                table: "CodeActivities");
        }
    }
}
