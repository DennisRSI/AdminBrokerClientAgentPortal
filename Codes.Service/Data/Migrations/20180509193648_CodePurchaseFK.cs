using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Codes.Service.Data.Migrations
{
    public partial class CodePurchaseFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PurchaseId",
                table: "Codes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Codes_PurchaseId",
                table: "Codes",
                column: "PurchaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Codes_Purchases_PurchaseId",
                table: "Codes",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Codes_Purchases_PurchaseId",
                table: "Codes");

            migrationBuilder.DropIndex(
                name: "IX_Codes_PurchaseId",
                table: "Codes");

            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "Codes");
        }
    }
}
