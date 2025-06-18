using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    public partial class ChangeStruct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardQuantity",
                table: "Campaigns");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDateTime",
                table: "Campaigns",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDateTime",
                table: "Campaigns",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "CampaignAgents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatorIP",
                table: "CampaignAgents",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeactivationDate",
                table: "CampaignAgents",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CampaignAgents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "CampaignAgents",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "CampaignAgents");

            migrationBuilder.DropColumn(
                name: "CreatorIP",
                table: "CampaignAgents");

            migrationBuilder.DropColumn(
                name: "DeactivationDate",
                table: "CampaignAgents");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CampaignAgents");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "CampaignAgents");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDateTime",
                table: "Campaigns",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDateTime",
                table: "Campaigns",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardQuantity",
                table: "Campaigns",
                nullable: false,
                defaultValue: 0);
        }
    }
}
