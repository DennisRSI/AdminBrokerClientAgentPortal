using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Codes.Service.Data.Migrations
{
    public partial class CampaignCodeRange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CampaignCodeRanges",
                columns: table => new
                {
                    CampaignCodeRangeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignId = table.Column<int>(nullable: false),
                    CodeRangeId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatorIP = table.Column<string>(maxLength: 50, nullable: false),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignCodeRanges", x => x.CampaignCodeRangeId);
                    table.ForeignKey(
                        name: "FK_CampaignCodeRanges_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CampaignCodeRanges_CodeRanges_CodeRangeId",
                        column: x => x.CodeRangeId,
                        principalTable: "CodeRanges",
                        principalColumn: "CodeRangeId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCodeRanges_CampaignId",
                table: "CampaignCodeRanges",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignCodeRanges_CodeRangeId",
                table: "CampaignCodeRanges",
                column: "CodeRangeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignCodeRanges");
        }
    }
}
