using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Codes.Service.Data.Migrations
{
    public partial class AddCampaignBenefits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "BenefitCondo",
                table: "Campaigns",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BenefitHotel",
                table: "Campaigns",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "BenefitShopping",
                table: "Campaigns",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BenefitCondo",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "BenefitHotel",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "BenefitShopping",
                table: "Campaigns");
        }
    }
}
