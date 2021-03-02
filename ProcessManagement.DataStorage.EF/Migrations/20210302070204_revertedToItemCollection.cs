using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessManagement.DataStorage.EF.Migrations
{
    public partial class revertedToItemCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessGroupModelProcessGroupId",
                table: "ProcessPoint",
                type: "int",
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
    }
}
