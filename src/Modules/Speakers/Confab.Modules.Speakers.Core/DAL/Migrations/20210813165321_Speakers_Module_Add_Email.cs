using Microsoft.EntityFrameworkCore.Migrations;

namespace Confab.Modules.Speakers.Core.DAL.Migrations
{
    public partial class Speakers_Module_Add_Email : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "speakers",
                table: "Speakers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "speakers",
                table: "Speakers");
        }
    }
}
