using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Codes1.Service.Data.Migrations
{
    public partial class FkClientToAgent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgentId",
                table: "Clients",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_AgentId",
                table: "Clients",
                column: "AgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_Agents_AgentId",
                table: "Clients",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "AgentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_Agents_AgentId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_AgentId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Clients");
        }
    }
}
