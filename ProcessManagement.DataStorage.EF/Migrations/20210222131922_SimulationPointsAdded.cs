using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessManagement.DataStorage.EF.Migrations
{
    public partial class SimulationPointsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimulationModel",
                columns: table => new
                {
                    SimulationModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessPointId = table.Column<string>(nullable: true),
                    ProcessPointName = table.Column<string>(nullable: true),
                    SortingNumber = table.Column<int>(nullable: false),
                    IsUnderProcess = table.Column<bool>(nullable: true),
                    SimulationStatus = table.Column<string>(nullable: true),
                    Deactivated = table.Column<bool>(nullable: false),
                    SimulationError = table.Column<bool>(nullable: false),
                    SuccessPercentage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationModel", x => x.SimulationModelId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimulationModel");
        }
    }
}
