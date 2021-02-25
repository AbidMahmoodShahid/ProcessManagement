using Microsoft.EntityFrameworkCore.Migrations;

namespace ProcessManagement.DataStorage.EF.Migrations
{
    public partial class TestingOld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessPointType",
                table: "ProcessPoint");

            migrationBuilder.AlterColumn<string>(
                name: "ProcessPointTypeName",
                table: "ProcessPoint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProcessPointTypeName",
                table: "ProcessPoint",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "ProcessPointType",
                table: "ProcessPoint",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
