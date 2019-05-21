using Microsoft.EntityFrameworkCore.Migrations;

namespace Control.Web.Migrations
{
    public partial class idEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassangerId",
                table: "Passangers",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Passangers",
                newName: "PassangerId");
        }
    }
}
