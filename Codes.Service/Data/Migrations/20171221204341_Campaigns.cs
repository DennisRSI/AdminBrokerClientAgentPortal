using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes.Service.Data.Migrations
{
    public partial class Campaigns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_ClientId",
                table: "Campaigns",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Clients_ClientId",
                table: "Campaigns",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Clients_ClientId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_ClientId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Campaigns");
        }
    }
}
