using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes.Service.Data.Migrations
{
    public partial class DeactivationReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationReasion",
                table: "CodeRanges",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationReasion",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationReasion",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationReasion",
                table: "Brokers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationReasion",
                table: "Agents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeactivationReasion",
                table: "CodeRanges");

            migrationBuilder.DropColumn(
                name: "DeactivationReasion",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "DeactivationReasion",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "DeactivationReasion",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "DeactivationReasion",
                table: "Agents");
        }
    }
}
