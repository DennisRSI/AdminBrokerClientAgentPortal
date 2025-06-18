using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    public partial class CodeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrokerId",
                table: "UsedCodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BrokerId",
                table: "UnusedCodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BrokerId",
                table: "PendingCodes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UsedCodes_BrokerId",
                table: "UsedCodes",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_UnusedCodes_BrokerId",
                table: "UnusedCodes",
                column: "BrokerId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingCodes_BrokerId",
                table: "PendingCodes",
                column: "BrokerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PendingCodes_Brokers_BrokerId",
                table: "PendingCodes",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UnusedCodes_Brokers_BrokerId",
                table: "UnusedCodes",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UsedCodes_Brokers_BrokerId",
                table: "UsedCodes",
                column: "BrokerId",
                principalTable: "Brokers",
                principalColumn: "BrokerId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingCodes_Brokers_BrokerId",
                table: "PendingCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UnusedCodes_Brokers_BrokerId",
                table: "UnusedCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsedCodes_Brokers_BrokerId",
                table: "UsedCodes");

            migrationBuilder.DropIndex(
                name: "IX_UsedCodes_BrokerId",
                table: "UsedCodes");

            migrationBuilder.DropIndex(
                name: "IX_UnusedCodes_BrokerId",
                table: "UnusedCodes");

            migrationBuilder.DropIndex(
                name: "IX_PendingCodes_BrokerId",
                table: "PendingCodes");

            migrationBuilder.DropColumn(
                name: "BrokerId",
                table: "UsedCodes");

            migrationBuilder.DropColumn(
                name: "BrokerId",
                table: "UnusedCodes");

            migrationBuilder.DropColumn(
                name: "BrokerId",
                table: "PendingCodes");
        }
    }
}
