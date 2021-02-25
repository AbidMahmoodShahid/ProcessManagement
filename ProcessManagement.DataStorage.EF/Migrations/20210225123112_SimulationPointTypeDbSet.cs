using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessManagement.DataStorage.EF.Migrations
{
    public partial class SimulationPointTypeDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimulationModel");

            migrationBuilder.CreateTable(
                name: "SimulationPoint",
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
                    table.PrimaryKey("PK_SimulationPoint", x => x.SimulationModelId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimulationPoint");

            migrationBuilder.CreateTable(
                name: "SimulationModel",
                columns: table => new
                {
                    SimulationModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deactivated = table.Column<bool>(type: "bit", nullable: false),
                    IsUnderProcess = table.Column<bool>(type: "bit", nullable: true),
                    ProcessPointId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessPointName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SimulationError = table.Column<bool>(type: "bit", nullable: false),
                    SimulationStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SortingNumber = table.Column<int>(type: "int", nullable: false),
                    SuccessPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulationModel", x => x.SimulationModelId);
                });
        }
    }
}
