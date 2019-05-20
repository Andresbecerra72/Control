using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Control.Web.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Passangers",
                columns: table => new
                {
                    PassangerId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Flight = table.Column<string>(maxLength: 4, nullable: false),
                    Adult = table.Column<int>(nullable: false),
                    Child = table.Column<int>(nullable: false),
                    Infant = table.Column<int>(nullable: false),
                    Total = table.Column<int>(nullable: false),
                    PublishOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passangers", x => x.PassangerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passangers");
        }
    }
}
