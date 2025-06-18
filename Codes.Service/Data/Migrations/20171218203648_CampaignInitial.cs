using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Codes1.Service.Data.Migrations
{
    public partial class CampaignInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactPhone",
                table: "Brokers");

            migrationBuilder.AlterColumn<string>(
                name: "BrokerName",
                table: "Brokers",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Brokers",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrokerFirstName",
                table: "Brokers",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrokerLastName",
                table: "Brokers",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Brokers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Brokers",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EIN",
                table: "Brokers",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Brokers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FaxExtension",
                table: "Brokers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobilePhone",
                table: "Brokers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficeExtension",
                table: "Brokers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OfficePhone",
                table: "Brokers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Brokers",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Brokers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    AgentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    AgentFirstName = table.Column<string>(maxLength: 255, nullable: true),
                    AgentLastName = table.Column<string>(maxLength: 255, nullable: true),
                    BrokerId = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    CommissionRate = table.Column<float>(nullable: false),
                    CompanyName = table.Column<string>(maxLength: 500, nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatorIP = table.Column<string>(maxLength: 50, nullable: false),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    EIN = table.Column<string>(maxLength: 500, nullable: true),
                    Fax = table.Column<string>(maxLength: 50, nullable: true),
                    FaxExtension = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    MobilePhone = table.Column<string>(maxLength: 50, nullable: true),
                    OfficeExtension = table.Column<string>(maxLength: 50, nullable: true),
                    OfficePhone = table.Column<string>(maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.AgentId);
                    table.ForeignKey(
                        name: "FK_Agents_Brokers_BrokerId",
                        column: x => x.BrokerId,
                        principalTable: "Brokers",
                        principalColumn: "BrokerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    CampaignId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrokerId = table.Column<int>(nullable: false),
                    CampaignDescription = table.Column<string>(nullable: true),
                    CampaignName = table.Column<string>(maxLength: 100, nullable: false),
                    CampaignType = table.Column<string>(maxLength: 50, nullable: false),
                    CardQuantity = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatorIP = table.Column<string>(maxLength: 50, nullable: false),
                    CustomCSS = table.Column<string>(nullable: true),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    GoogleAnalyticsCode = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    PackageId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StartDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.CampaignId);
                    table.ForeignKey(
                        name: "FK_Campaigns_Brokers_BrokerId",
                        column: x => x.BrokerId,
                        principalTable: "Brokers",
                        principalColumn: "BrokerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    BrokerId = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    CompanyName = table.Column<string>(maxLength: 500, nullable: false),
                    ContactFirstName = table.Column<string>(maxLength: 255, nullable: true),
                    ContactLastName = table.Column<string>(maxLength: 255, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatorIP = table.Column<string>(maxLength: 50, nullable: false),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    EIN = table.Column<string>(maxLength: 500, nullable: true),
                    Fax = table.Column<string>(maxLength: 50, nullable: true),
                    FaxExtension = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    MobilePhone = table.Column<string>(maxLength: 50, nullable: true),
                    OfficeExtension = table.Column<string>(maxLength: 50, nullable: true),
                    OfficePhone = table.Column<string>(maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    State = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_Brokers_BrokerId",
                        column: x => x.BrokerId,
                        principalTable: "Brokers",
                        principalColumn: "BrokerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CodeRanges",
                columns: table => new
                {
                    CodeRangeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrokerId = table.Column<int>(nullable: false),
                    CodeType = table.Column<string>(maxLength: 50, nullable: false),
                    CondoPoints = table.Column<decimal>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatorIP = table.Column<string>(maxLength: 50, nullable: false),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    EndNumber = table.Column<int>(nullable: false),
                    HotelPoints = table.Column<decimal>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    NumberOfUses = table.Column<int>(nullable: false),
                    PackageId = table.Column<int>(nullable: false),
                    PostAlphaCharacters = table.Column<string>(maxLength: 50, nullable: true),
                    PreAlphaCharacters = table.Column<string>(maxLength: 50, nullable: true),
                    RSIOrganizationId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    StartNumber = table.Column<int>(nullable: false),
                    VerifyEmail = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeRanges", x => x.CodeRangeId);
                    table.ForeignKey(
                        name: "FK_CodeRanges_Brokers_BrokerId",
                        column: x => x.BrokerId,
                        principalTable: "Brokers",
                        principalColumn: "BrokerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CampaignAgents",
                columns: table => new
                {
                    CampaignId = table.Column<int>(nullable: false),
                    AgentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignAgents", x => new { x.CampaignId, x.AgentId });
                    table.ForeignKey(
                        name: "FK_CampaignAgents_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CampaignAgents_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_BrokerId",
                table: "Agents",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignAgents_AgentId",
                table: "CampaignAgents",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_BrokerId",
                table: "Campaigns",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_BrokerId",
                table: "Clients",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_CodeRanges_BrokerId",
                table: "CodeRanges",
                column: "BrokerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignAgents");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "CodeRanges");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "BrokerFirstName",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "BrokerLastName",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "EIN",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "FaxExtension",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "MobilePhone",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "OfficeExtension",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "OfficePhone",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Brokers");

            migrationBuilder.AlterColumn<string>(
                name: "BrokerName",
                table: "Brokers",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPhone",
                table: "Brokers",
                nullable: true);
        }
    }
}
