using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Codes1.Service.Data.Migrations
{
    public partial class CreateVideo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostLoginVideoId",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreLoginVideoId",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    VideoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatorIP = table.Column<string>(maxLength: 50, nullable: false),
                    DeactivationDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsPreLogin = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Url = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.VideoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_PostLoginVideoId",
                table: "Campaigns",
                column: "PostLoginVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_PreLoginVideoId",
                table: "Campaigns",
                column: "PreLoginVideoId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Videos_PostLoginVideoId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Videos_PreLoginVideoId",
                table: "Campaigns");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_PostLoginVideoId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_PreLoginVideoId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "PostLoginVideoId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "PreLoginVideoId",
                table: "Campaigns");
        }
    }
}
