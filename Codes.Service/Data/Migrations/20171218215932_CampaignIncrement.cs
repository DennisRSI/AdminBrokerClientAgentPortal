using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    public partial class CampaignIncrement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrokerName",
                table: "Brokers");

            migrationBuilder.AddColumn<int>(
                name: "IncrementByNumber",
                table: "CodeRanges",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncrementByNumber",
                table: "CodeRanges");

            migrationBuilder.AddColumn<string>(
                name: "BrokerName",
                table: "Brokers",
                maxLength: 500,
                nullable: true);
        }
    }
}
