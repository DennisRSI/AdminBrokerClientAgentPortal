using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Codes.Service.Data.Migrations
{
    public partial class AddPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Brokers_BrokerId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignAgents_Agents_AgentId",
                table: "CampaignAgents");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignAgents_Campaigns_CampaignId",
                table: "CampaignAgents");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignCodeRanges_Campaigns_CampaignId",
                table: "CampaignCodeRanges");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignCodeRanges_CodeRanges_CodeRangeId",
                table: "CampaignCodeRanges");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Brokers_BrokerId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Clients_ClientId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Videos_PostLoginVideoId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Videos_PreLoginVideoId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Brokers_BrokerId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_CodeActivations_CodeRanges_CodeRangeModelCodeRangeId",
                table: "CodeActivations");

            migrationBuilder.DropForeignKey(
                name: "FK_CodeActivities_Codes_CodeId",
                table: "CodeActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_CodeRanges_Brokers_BrokerId",
                table: "CodeRanges");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingCodes_Brokers_BrokerId",
                table: "PendingCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingCodes_Campaigns_CampaignId",
                table: "PendingCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingCodes_CodeRanges_CodeRangeId",
                table: "PendingCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UnusedCodes_Brokers_BrokerId",
                table: "UnusedCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UnusedCodes_Campaigns_CampaignId",
                table: "UnusedCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UnusedCodes_CodeRanges_CodeRangeId",
                table: "UnusedCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsedCodes_Brokers_BrokerId",
                table: "UsedCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsedCodes_Campaigns_CampaignId",
                table: "UsedCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsedCodes_CodeRanges_CodeRangeId",
                table: "UsedCodes");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BrokerModelBrokerId = table.Column<int>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatorIP = table.Column<string>(maxLength: 50, nullable: false),
                    Data = table.Column<byte[]>(nullable: false),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    DocumentType = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_Brokers_BrokerModelBrokerId",
                        column: x => x.BrokerModelBrokerId,
                        principalTable: "Brokers",
                        principalColumn: "BrokerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(maxLength: 200, nullable: false),
                    BillingZip = table.Column<string>(maxLength: 10, nullable: false),
                    BrokerId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatorIP = table.Column<string>(maxLength: 50, nullable: false),
                    CreditCardLast4 = table.Column<string>(maxLength: 4, nullable: false),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    FullName = table.Column<string>(maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PhysicalQuantity = table.Column<int>(nullable: false),
                    PhysicalValue = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ShippingZip = table.Column<string>(maxLength: 10, nullable: false),
                    State = table.Column<string>(maxLength: 2, nullable: false),
                    VirtualQuantity = table.Column<int>(nullable: false),
                    VirtualValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_Purchases_Brokers_BrokerId",
                        column: x => x.BrokerId,
                        principalTable: "Brokers",
                        principalColumn: "BrokerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_BrokerModelBrokerId",
                table: "Documents",
                column: "BrokerModelBrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_BrokerId",
                table: "Purchases",
                column: "BrokerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Brokers_BrokerId",
                table: "Agents",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignAgents_Agents_AgentId",
                table: "CampaignAgents",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "AgentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignAgents_Campaigns_CampaignId",
                table: "CampaignAgents",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignCodeRanges_Campaigns_CampaignId",
                table: "CampaignCodeRanges",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignCodeRanges_CodeRanges_CodeRangeId",
                table: "CampaignCodeRanges",
                column: "CodeRangeId",
                principalTable: "CodeRanges",
                principalColumn: "CodeRangeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Brokers_BrokerId",
                table: "Campaigns",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Clients_ClientId",
                table: "Campaigns",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Videos_PostLoginVideoId",
                table: "Campaigns",
                column: "PostLoginVideoId",
                principalTable: "Videos",
                principalColumn: "VideoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Videos_PreLoginVideoId",
                table: "Campaigns",
                column: "PreLoginVideoId",
                principalTable: "Videos",
                principalColumn: "VideoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Brokers_BrokerId",
                table: "Clients",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CodeActivations_CodeRanges_CodeRangeModelCodeRangeId",
                table: "CodeActivations",
                column: "CodeRangeModelCodeRangeId",
                principalTable: "CodeRanges",
                principalColumn: "CodeRangeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CodeActivities_Codes_CodeId",
                table: "CodeActivities",
                column: "CodeId",
                principalTable: "Codes",
                principalColumn: "CodeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CodeRanges_Brokers_BrokerId",
                table: "CodeRanges",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingCodes_Brokers_BrokerId",
                table: "PendingCodes",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingCodes_Campaigns_CampaignId",
                table: "PendingCodes",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingCodes_CodeRanges_CodeRangeId",
                table: "PendingCodes",
                column: "CodeRangeId",
                principalTable: "CodeRanges",
                principalColumn: "CodeRangeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnusedCodes_Brokers_BrokerId",
                table: "UnusedCodes",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnusedCodes_Campaigns_CampaignId",
                table: "UnusedCodes",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnusedCodes_CodeRanges_CodeRangeId",
                table: "UnusedCodes",
                column: "CodeRangeId",
                principalTable: "CodeRanges",
                principalColumn: "CodeRangeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsedCodes_Brokers_BrokerId",
                table: "UsedCodes",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsedCodes_Campaigns_CampaignId",
                table: "UsedCodes",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsedCodes_CodeRanges_CodeRangeId",
                table: "UsedCodes",
                column: "CodeRangeId",
                principalTable: "CodeRanges",
                principalColumn: "CodeRangeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Brokers_BrokerId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignAgents_Agents_AgentId",
                table: "CampaignAgents");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignAgents_Campaigns_CampaignId",
                table: "CampaignAgents");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignCodeRanges_Campaigns_CampaignId",
                table: "CampaignCodeRanges");

            migrationBuilder.DropForeignKey(
                name: "FK_CampaignCodeRanges_CodeRanges_CodeRangeId",
                table: "CampaignCodeRanges");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Brokers_BrokerId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Clients_ClientId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Videos_PostLoginVideoId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Videos_PreLoginVideoId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Brokers_BrokerId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_CodeActivations_CodeRanges_CodeRangeModelCodeRangeId",
                table: "CodeActivations");

            migrationBuilder.DropForeignKey(
                name: "FK_CodeActivities_Codes_CodeId",
                table: "CodeActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_CodeRanges_Brokers_BrokerId",
                table: "CodeRanges");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingCodes_Brokers_BrokerId",
                table: "PendingCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingCodes_Campaigns_CampaignId",
                table: "PendingCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_PendingCodes_CodeRanges_CodeRangeId",
                table: "PendingCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UnusedCodes_Brokers_BrokerId",
                table: "UnusedCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UnusedCodes_Campaigns_CampaignId",
                table: "UnusedCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UnusedCodes_CodeRanges_CodeRangeId",
                table: "UnusedCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsedCodes_Brokers_BrokerId",
                table: "UsedCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsedCodes_Campaigns_CampaignId",
                table: "UsedCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsedCodes_CodeRanges_CodeRangeId",
                table: "UsedCodes");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Brokers_BrokerId",
                table: "Agents",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignAgents_Agents_AgentId",
                table: "CampaignAgents",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "AgentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignAgents_Campaigns_CampaignId",
                table: "CampaignAgents",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignCodeRanges_Campaigns_CampaignId",
                table: "CampaignCodeRanges",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CampaignCodeRanges_CodeRanges_CodeRangeId",
                table: "CampaignCodeRanges",
                column: "CodeRangeId",
                principalTable: "CodeRanges",
                principalColumn: "CodeRangeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Brokers_BrokerId",
                table: "Campaigns",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Clients_ClientId",
                table: "Campaigns",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Videos_PostLoginVideoId",
                table: "Campaigns",
                column: "PostLoginVideoId",
                principalTable: "Videos",
                principalColumn: "VideoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Videos_PreLoginVideoId",
                table: "Campaigns",
                column: "PreLoginVideoId",
                principalTable: "Videos",
                principalColumn: "VideoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Brokers_BrokerId",
                table: "Clients",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CodeActivations_CodeRanges_CodeRangeModelCodeRangeId",
                table: "CodeActivations",
                column: "CodeRangeModelCodeRangeId",
                principalTable: "CodeRanges",
                principalColumn: "CodeRangeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CodeActivities_Codes_CodeId",
                table: "CodeActivities",
                column: "CodeId",
                principalTable: "Codes",
                principalColumn: "CodeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CodeRanges_Brokers_BrokerId",
                table: "CodeRanges",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingCodes_Brokers_BrokerId",
                table: "PendingCodes",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingCodes_Campaigns_CampaignId",
                table: "PendingCodes",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingCodes_CodeRanges_CodeRangeId",
                table: "PendingCodes",
                column: "CodeRangeId",
                principalTable: "CodeRanges",
                principalColumn: "CodeRangeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnusedCodes_Brokers_BrokerId",
                table: "UnusedCodes",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnusedCodes_Campaigns_CampaignId",
                table: "UnusedCodes",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnusedCodes_CodeRanges_CodeRangeId",
                table: "UnusedCodes",
                column: "CodeRangeId",
                principalTable: "CodeRanges",
                principalColumn: "CodeRangeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsedCodes_Brokers_BrokerId",
                table: "UsedCodes",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsedCodes_Campaigns_CampaignId",
                table: "UsedCodes",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "CampaignId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsedCodes_CodeRanges_CodeRangeId",
                table: "UsedCodes",
                column: "CodeRangeId",
                principalTable: "CodeRanges",
                principalColumn: "CodeRangeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
