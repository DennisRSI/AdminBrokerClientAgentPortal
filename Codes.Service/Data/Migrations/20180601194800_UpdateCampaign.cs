using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes.Service.Data.Migrations
{
    public partial class UpdateCampaign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn("BenefitCondo", "Campaigns");
            migrationBuilder.DropColumn("BenefitHotel", "Campaigns");
            migrationBuilder.DropColumn("BenefitShopping", "Campaigns");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
