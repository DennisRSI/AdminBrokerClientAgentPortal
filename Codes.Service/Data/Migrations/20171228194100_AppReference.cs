using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    public partial class AppReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationReference",
                table: "Clients",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationReference",
                table: "Brokers",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationReference",
                table: "Agents",
                maxLength: 450,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationReference",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ApplicationReference",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "ApplicationReference",
                table: "Agents");
        }
    }
}
