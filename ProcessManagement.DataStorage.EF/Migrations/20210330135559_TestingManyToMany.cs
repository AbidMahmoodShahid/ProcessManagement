using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessManagement.DataStorage.EF.Migrations
{
    public partial class TestingManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TableA",
                columns: table => new
                {
                    TableAId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableA", x => x.TableAId);
                });

            migrationBuilder.CreateTable(
                name: "TableB",
                columns: table => new
                {
                    TableBId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableB", x => x.TableBId);
                });

            migrationBuilder.CreateTable(
                name: "TableAB",
                columns: table => new
                {
                    TableAId = table.Column<int>(nullable: false),
                    TableBId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableAB", x => new { x.TableAId, x.TableBId });
                    table.ForeignKey(
                        name: "FK_TableAB_TableA_TableAId",
                        column: x => x.TableAId,
                        principalTable: "TableA",
                        principalColumn: "TableAId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TableAB_TableB_TableBId",
                        column: x => x.TableBId,
                        principalTable: "TableB",
                        principalColumn: "TableBId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TableAB_TableBId",
                table: "TableAB",
                column: "TableBId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TableAB");

            migrationBuilder.DropTable(
                name: "TableA");

            migrationBuilder.DropTable(
                name: "TableB");
        }
    }
}
