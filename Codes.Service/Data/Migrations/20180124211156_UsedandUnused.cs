using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Codes1.Service.Data.Migrations
{
    public partial class UsedandUnused : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodeActivations_CodeRanges_CodeRangeId",
                table: "CodeActivations");

            migrationBuilder.DropIndex(
                name: "IX_CodeActivations_CodeRangeId",
                table: "CodeActivations");

            migrationBuilder.DropColumn(
                name: "CondoPoints",
                table: "CodeRanges");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CodeRanges");

            migrationBuilder.DropColumn(
                name: "HotelPoints",
                table: "CodeRanges");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "CodeRanges");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CodeRanges");

            migrationBuilder.DropColumn(
                name: "VerifyEmail",
                table: "CodeRanges");

            migrationBuilder.DropColumn(
                name: "CodeRangeId",
                table: "CodeActivations");

            migrationBuilder.AddColumn<float>(
                name: "Points",
                table: "CodeRanges",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "CodeRangeModelCodeRangeId",
                table: "CodeActivations",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PendingCodes",
                columns: table => new
                {
                    PendingCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address1 = table.Column<string>(maxLength: 255, nullable: true),
                    Address2 = table.Column<string>(maxLength: 255, nullable: true),
                    CampaignId = table.Column<int>(nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Code = table.Column<string>(maxLength: 1000, nullable: false),
                    CodeRangeId = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Country = table.Column<string>(maxLength: 100, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 255, nullable: false),
                    NumberOfUses = table.Column<int>(nullable: false),
                    OrigionalCreationDate = table.Column<DateTime>(nullable: false),
                    PackageId = table.Column<int>(nullable: false),
                    Paid = table.Column<decimal>(nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Phone1 = table.Column<string>(maxLength: 50, nullable: true),
                    Phone2 = table.Column<string>(maxLength: 50, nullable: true),
                    Points = table.Column<float>(nullable: false),
                    PostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    RSIId = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    State = table.Column<string>(maxLength: 100, nullable: true),
                    Username = table.Column<string>(maxLength: 100, nullable: false),
                    VerifyEmail = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingCodes", x => x.PendingCodeId);
                    table.ForeignKey(
                        name: "FK_PendingCodes_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PendingCodes_CodeRanges_CodeRangeId",
                        column: x => x.CodeRangeId,
                        principalTable: "CodeRanges",
                        principalColumn: "CodeRangeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnusedCodes",
                columns: table => new
                {
                    UnusedCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CampaignId = table.Column<int>(nullable: true),
                    Code = table.Column<string>(maxLength: 1000, nullable: false),
                    CodeRangeId = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatorIP = table.Column<string>(maxLength: 50, nullable: false),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    DeactivationReason = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    NumberOfUses = table.Column<int>(nullable: false),
                    PackageId = table.Column<int>(nullable: false),
                    Points = table.Column<float>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    VerifyEmail = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnusedCodes", x => x.UnusedCodeId);
                    table.ForeignKey(
                        name: "FK_UnusedCodes_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnusedCodes_CodeRanges_CodeRangeId",
                        column: x => x.CodeRangeId,
                        principalTable: "CodeRanges",
                        principalColumn: "CodeRangeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsedCodes",
                columns: table => new
                {
                    UsedCodeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address1 = table.Column<string>(maxLength: 255, nullable: true),
                    Address2 = table.Column<string>(maxLength: 255, nullable: true),
                    CampaignId = table.Column<int>(nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    Code = table.Column<string>(maxLength: 1000, nullable: false),
                    CodeRangeId = table.Column<int>(nullable: false),
                    Cost = table.Column<decimal>(nullable: false),
                    Country = table.Column<string>(maxLength: 100, nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatorIP = table.Column<string>(maxLength: 50, nullable: false),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    EmailVerifiedDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 255, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 255, nullable: false),
                    NumberOfUses = table.Column<int>(nullable: false),
                    OrigionalCreationDate = table.Column<DateTime>(nullable: false),
                    PackageId = table.Column<int>(nullable: false),
                    Paid = table.Column<decimal>(nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Phone1 = table.Column<string>(maxLength: 50, nullable: true),
                    Phone2 = table.Column<string>(maxLength: 50, nullable: true),
                    Points = table.Column<float>(nullable: false),
                    PostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    RSIId = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    State = table.Column<string>(maxLength: 100, nullable: true),
                    Username = table.Column<string>(maxLength: 100, nullable: false),
                    VerifyEmail = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsedCodes", x => x.UsedCodeId);
                    table.ForeignKey(
                        name: "FK_UsedCodes_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "CampaignId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsedCodes_CodeRanges_CodeRangeId",
                        column: x => x.CodeRangeId,
                        principalTable: "CodeRanges",
                        principalColumn: "CodeRangeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodeActivations_CodeRangeModelCodeRangeId",
                table: "CodeActivations",
                column: "CodeRangeModelCodeRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingCodes_CampaignId",
                table: "PendingCodes",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingCodes_CodeRangeId",
                table: "PendingCodes",
                column: "CodeRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnusedCodes_CampaignId",
                table: "UnusedCodes",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_UnusedCodes_CodeRangeId",
                table: "UnusedCodes",
                column: "CodeRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCodes_CampaignId",
                table: "UsedCodes",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_UsedCodes_CodeRangeId",
                table: "UsedCodes",
                column: "CodeRangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CodeActivations_CodeRanges_CodeRangeModelCodeRangeId",
                table: "CodeActivations",
                column: "CodeRangeModelCodeRangeId",
                principalTable: "CodeRanges",
                principalColumn: "CodeRangeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CodeActivations_CodeRanges_CodeRangeModelCodeRangeId",
                table: "CodeActivations");

            migrationBuilder.DropTable(
                name: "PendingCodes");

            migrationBuilder.DropTable(
                name: "UnusedCodes");

            migrationBuilder.DropTable(
                name: "UsedCodes");

            migrationBuilder.DropIndex(
                name: "IX_CodeActivations_CodeRangeModelCodeRangeId",
                table: "CodeActivations");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "CodeRanges");

            migrationBuilder.DropColumn(
                name: "CodeRangeModelCodeRangeId",
                table: "CodeActivations");

            migrationBuilder.AddColumn<decimal>(
                name: "CondoPoints",
                table: "CodeRanges",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "CodeRanges",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "HotelPoints",
                table: "CodeRanges",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "CodeRanges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "CodeRanges",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "VerifyEmail",
                table: "CodeRanges",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CodeRangeId",
                table: "CodeActivations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CodeActivations_CodeRangeId",
                table: "CodeActivations",
                column: "CodeRangeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CodeActivations_CodeRanges_CodeRangeId",
                table: "CodeActivations",
                column: "CodeRangeId",
                principalTable: "CodeRanges",
                principalColumn: "CodeRangeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
