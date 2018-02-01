using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes.Service.Data.Migrations
{
    public partial class DeactivationReasonFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DeactivationReason",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Clients",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeactivationReason",
                table: "Brokers",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<float>(
                name: "AgentCommissionPercentage",
                table: "Brokers",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "BrokerCommissionPercentage",
                table: "Brokers",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ClientCommissionPercentage",
                table: "Brokers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Brokers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue1000",
                table: "Brokers",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue10000",
                table: "Brokers",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue100000",
                table: "Brokers",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue25000",
                table: "Brokers",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue5000",
                table: "Brokers",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue50000",
                table: "Brokers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimeframeBetweenCapInHours",
                table: "Brokers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VirtualCardCap",
                table: "Brokers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeactivationReason",
                table: "Agents",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Agents",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AgentCommissionPercentage",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "BrokerCommissionPercentage",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "ClientCommissionPercentage",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "PhysicalCardsPercentOfFaceValue1000",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "PhysicalCardsPercentOfFaceValue10000",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "PhysicalCardsPercentOfFaceValue100000",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "PhysicalCardsPercentOfFaceValue25000",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "PhysicalCardsPercentOfFaceValue5000",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "PhysicalCardsPercentOfFaceValue50000",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "TimeframeBetweenCapInHours",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "VirtualCardCap",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Agents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeactivationReason",
                table: "Clients",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeactivationReason",
                table: "Brokers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeactivationReason",
                table: "Agents",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
