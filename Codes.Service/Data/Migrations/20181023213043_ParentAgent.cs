using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes.Service.Data.Migrations
{
    public partial class ParentAgent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentAgentId",
                table: "Agents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agents_ParentAgentId",
                table: "Agents",
                column: "ParentAgentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_Agents_ParentAgentId",
                table: "Agents",
                column: "ParentAgentId",
                principalTable: "Agents",
                principalColumn: "AgentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_Agents_ParentAgentId",
                table: "Agents");

            migrationBuilder.DropIndex(
                name: "IX_Agents_ParentAgentId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "ParentAgentId",
                table: "Agents");
        }
    }
}
