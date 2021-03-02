using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessManagement.DataStorage.EF.Migrations
{
    public partial class TestingInverseNavigationProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessPoint_ProcessGroup_ProcessGroupModelId",
                table: "ProcessPoint");

            migrationBuilder.DropIndex(
                name: "IX_ProcessPoint_ProcessGroupModelId",
                table: "ProcessPoint");

            migrationBuilder.DropColumn(
                name: "ProcessGroupModelId",
                table: "ProcessPoint");

            migrationBuilder.AddColumn<int>(
                name: "ProcessGroupModelProcessGroupId",
                table: "ProcessPoint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessPoint_ProcessGroupModelProcessGroupId",
                table: "ProcessPoint",
                column: "ProcessGroupModelProcessGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessPoint_ProcessGroup_ProcessGroupModelProcessGroupId",
                table: "ProcessPoint",
                column: "ProcessGroupModelProcessGroupId",
                principalTable: "ProcessGroup",
                principalColumn: "ProcessGroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessPoint_ProcessGroup_ProcessGroupModelProcessGroupId",
                table: "ProcessPoint");

            migrationBuilder.DropIndex(
                name: "IX_ProcessPoint_ProcessGroupModelProcessGroupId",
                table: "ProcessPoint");

            migrationBuilder.DropColumn(
                name: "ProcessGroupModelProcessGroupId",
                table: "ProcessPoint");

            migrationBuilder.AddColumn<int>(
                name: "ProcessGroupModelId",
                table: "ProcessPoint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProcessPoint_ProcessGroupModelId",
                table: "ProcessPoint",
                column: "ProcessGroupModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessPoint_ProcessGroup_ProcessGroupModelId",
                table: "ProcessPoint",
                column: "ProcessGroupModelId",
                principalTable: "ProcessGroup",
                principalColumn: "ProcessGroupId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
