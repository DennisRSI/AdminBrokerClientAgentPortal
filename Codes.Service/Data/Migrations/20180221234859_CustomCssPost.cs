using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Codes1.Service.Data.Migrations
{
    public partial class CustomCssPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Videos_PostLoginVideoId",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Videos_PreLoginVideoId",
                table: "Campaigns");

            migrationBuilder.AlterColumn<int>(
                name: "PreLoginVideoId",
                table: "Campaigns",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PostLoginVideoId",
                table: "Campaigns",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomCssPost",
                table: "Campaigns",
                nullable: true);

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

            migrationBuilder.DropColumn(
                name: "CustomCssPost",
                table: "Campaigns");

            migrationBuilder.AlterColumn<int>(
                name: "PreLoginVideoId",
                table: "Campaigns",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "PostLoginVideoId",
                table: "Campaigns",
                nullable: true,
                oldClrType: typeof(int));

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
    }
}
