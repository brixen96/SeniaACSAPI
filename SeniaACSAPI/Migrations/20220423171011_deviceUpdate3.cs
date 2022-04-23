using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeniaACSAPI.Migrations
{
    public partial class deviceUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "MAC",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MAC",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Devices");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
