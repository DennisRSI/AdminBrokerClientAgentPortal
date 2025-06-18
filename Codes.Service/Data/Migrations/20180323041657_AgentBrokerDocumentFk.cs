using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Codes1.Service.Data.Migrations
{
    public partial class AgentBrokerDocumentFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentOtherId",
                table: "Brokers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentW9Id",
                table: "Brokers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentOtherId",
                table: "Agents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentW9Id",
                table: "Agents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brokers_DocumentOtherId",
                table: "Brokers",
                column: "DocumentOtherId");

            migrationBuilder.CreateIndex(
                name: "IX_Brokers_DocumentW9Id",
                table: "Brokers",
                column: "DocumentW9Id");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_DocumentOtherId",
                table: "Agents",
                column: "DocumentOtherId");

            migrationBuilder.CreateIndex(
                name: "IX_Agents_DocumentW9Id",
                table: "Agents",
                column: "DocumentW9Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Documents_DocumentOtherId",
                table: "Agents",
                column: "DocumentOtherId",
                principalTable: "Documents",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Documents_DocumentW9Id",
                table: "Agents",
                column: "DocumentW9Id",
                principalTable: "Documents",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Brokers_Documents_DocumentOtherId",
                table: "Brokers",
                column: "DocumentOtherId",
                principalTable: "Documents",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Brokers_Documents_DocumentW9Id",
                table: "Brokers",
                column: "DocumentW9Id",
                principalTable: "Documents",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Documents_DocumentOtherId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Documents_DocumentW9Id",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Brokers_Documents_DocumentOtherId",
                table: "Brokers");

            migrationBuilder.DropForeignKey(
                name: "FK_Brokers_Documents_DocumentW9Id",
                table: "Brokers");

            migrationBuilder.DropIndex(
                name: "IX_Brokers_DocumentOtherId",
                table: "Brokers");

            migrationBuilder.DropIndex(
                name: "IX_Brokers_DocumentW9Id",
                table: "Brokers");

            migrationBuilder.DropIndex(
                name: "IX_Agents_DocumentOtherId",
                table: "Agents");

            migrationBuilder.DropIndex(
                name: "IX_Agents_DocumentW9Id",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "DocumentOtherId",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "DocumentW9Id",
                table: "Brokers");

            migrationBuilder.DropColumn(
                name: "DocumentOtherId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "DocumentW9Id",
                table: "Agents");
        }
    }
}
