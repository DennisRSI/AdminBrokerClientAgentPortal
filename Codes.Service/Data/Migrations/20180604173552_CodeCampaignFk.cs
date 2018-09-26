using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes.Service.Data.Migrations
{
    public partial class CodeCampaignFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Codes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Codes_CampaignId",
                table: "Codes",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Codes_Campaigns_CampaignId",
                table: "Codes",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codes_Campaigns_CampaignId",
                table: "Codes");

            migrationBuilder.DropIndex(
                name: "IX_Codes_CampaignId",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Codes");

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
    }
}
