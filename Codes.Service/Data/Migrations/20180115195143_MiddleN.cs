using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    public partial class MiddleN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "CommissionRate",
                table: "Clients",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<string>(
                name: "ContactMiddleName",
                table: "Clients",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrokerMiddleName",
                table: "Brokers",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgentMiddleName",
                table: "Agents",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CommissionRate",
                table: "Agents",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactMiddleName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "BrokerMiddleName",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "AgentMiddleName",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "CommissionRate",
                table: "Agents");

            migrationBuilder.AlterColumn<decimal>(
                name: "CommissionRate",
                table: "Clients",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
