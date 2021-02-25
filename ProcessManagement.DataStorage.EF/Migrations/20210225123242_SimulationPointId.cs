using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessManagement.DataStorage.EF.Migrations
{
    public partial class SimulationPointId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SimulationPoint",
                table: "SimulationPoint");

            migrationBuilder.DropColumn(
                name: "SimulationModelId",
                table: "SimulationPoint");

            migrationBuilder.AddColumn<int>(
                name: "SimulationPointId",
                table: "SimulationPoint",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SimulationPoint",
                table: "SimulationPoint",
                column: "SimulationPointId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SimulationPoint",
                table: "SimulationPoint");

            migrationBuilder.DropColumn(
                name: "SimulationPointId",
                table: "SimulationPoint");

            migrationBuilder.AddColumn<int>(
                name: "SimulationModelId",
                table: "SimulationPoint",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SimulationPoint",
                table: "SimulationPoint",
                column: "SimulationModelId");
        }
    }
}
