using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMessenger.Server.Migrations
{
    public partial class Added_AssemblyVersion_Property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssemblyVersion",
                table: "Launches",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssemblyVersion",
                table: "Launches");
        }
    }
}
