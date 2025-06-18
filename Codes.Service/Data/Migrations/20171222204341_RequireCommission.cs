using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    public partial class RequireCommission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VirtualCardCap",
                table: "Brokers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TimeframeBetweenCapInHours",
                table: "Brokers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue50000",
                table: "Brokers",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue5000",
                table: "Brokers",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue25000",
                table: "Brokers",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue100000",
                table: "Brokers",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue10000",
                table: "Brokers",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue1000",
                table: "Brokers",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ClientCommissionPercentage",
                table: "Brokers",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "BrokerCommissionPercentage",
                table: "Brokers",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "AgentCommissionPercentage",
                table: "Brokers",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "VirtualCardCap",
                table: "Brokers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TimeframeBetweenCapInHours",
                table: "Brokers",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue50000",
                table: "Brokers",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue5000",
                table: "Brokers",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue25000",
                table: "Brokers",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue100000",
                table: "Brokers",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue10000",
                table: "Brokers",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "PhysicalCardsPercentOfFaceValue1000",
                table: "Brokers",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "ClientCommissionPercentage",
                table: "Brokers",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "BrokerCommissionPercentage",
                table: "Brokers",
                nullable: true,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<float>(
                name: "AgentCommissionPercentage",
                table: "Brokers",
                nullable: true,
                oldClrType: typeof(float));
        }
    }
}
