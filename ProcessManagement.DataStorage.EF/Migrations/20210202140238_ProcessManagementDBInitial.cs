using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessManagement.DataStorage.EF.Migrations
{
    public partial class ProcessManagementDBInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Process",
                columns: table => new
                {
                    ProcessModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Process", x => x.ProcessModelId);
                });

            migrationBuilder.CreateTable(
                name: "ProcessGroup",
                columns: table => new
                {
                    ProcessGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: true),
                    SortingNumber = table.Column<int>(nullable: false),
                    ProcessModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessGroup", x => x.ProcessGroupId);
                    table.ForeignKey(
                        name: "FK_ProcessGroup_Process_ProcessModelId",
                        column: x => x.ProcessModelId,
                        principalTable: "Process",
                        principalColumn: "ProcessModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessPoint",
                columns: table => new
                {
                    ProcessPointID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessPointTypeName = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: true),
                    SortingNumber = table.Column<int>(nullable: false),
                    ProcessGroupModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessPoint", x => x.ProcessPointID);
                    table.ForeignKey(
                        name: "FK_ProcessPoint_ProcessGroup_ProcessGroupModelId",
                        column: x => x.ProcessGroupModelId,
                        principalTable: "ProcessGroup",
                        principalColumn: "ProcessGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessGroup_ProcessModelId",
                table: "ProcessGroup",
                column: "ProcessModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessPoint_ProcessGroupModelId",
                table: "ProcessPoint",
                column: "ProcessGroupModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessPoint");

            migrationBuilder.DropTable(
                name: "ProcessGroup");

            migrationBuilder.DropTable(
                name: "Process");
        }
    }
}
