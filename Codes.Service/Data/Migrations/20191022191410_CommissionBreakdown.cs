using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Codes1.Service.Data.Migrations
{
    public partial class CommissionBreakdown : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingCommissions",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false),
                    BookingType = table.Column<string>(maxLength: 50, nullable: false),
                    MemberSavings = table.Column<decimal>(nullable: false),
                    CompanyBucketPrecentage = table.Column<float>(nullable: false),
                    ClientPrecentage = table.Column<float>(nullable: false),
                    AgentBucketPrecentage = table.Column<float>(nullable: false),
                    CompanyBucketCommission = table.Column<decimal>(nullable: false),
                    ClientCommission = table.Column<decimal>(nullable: false),
                    AgentBucketCommission = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCommissions", x => x.BookingId);
                });

            migrationBuilder.CreateTable(
                name: "BookingCommissionBreakdowns",
                columns: table => new
                {
                    BookingCommissionBreakdownId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookingId = table.Column<int>(nullable: false),
                    PersonType = table.Column<string>(maxLength: 50, nullable: false),
                    Id = table.Column<int>(nullable: false),
                    CommissionPrecentage = table.Column<float>(nullable: false),
                    Commission = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCommissionBreakdowns", x => x.BookingCommissionBreakdownId);
                    table.ForeignKey(
                        name: "FK_BookingCommissionBreakdowns_BookingCommissions_BookingId",
                        column: x => x.BookingId,
                        principalTable: "BookingCommissions",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingCommissionBreakdowns_BookingId",
                table: "BookingCommissionBreakdowns",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingCommissionBreakdowns");

            migrationBuilder.DropTable(
                name: "BookingCommissions");
        }
    }
}
