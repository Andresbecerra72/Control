using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Control.Web.Migrations
{
    public partial class KiuReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KiuReports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Hour = table.Column<string>(nullable: true),
                    Vuelo = table.Column<string>(nullable: true),
                    Origen_Itinerario = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Dest = table.Column<string>(nullable: true),
                    Equipo = table.Column<string>(nullable: true),
                    Matricula = table.Column<string>(nullable: true),
                    Delay = table.Column<string>(nullable: true),
                    Pais_emision = table.Column<string>(nullable: true),
                    Emisor = table.Column<string>(nullable: true),
                    Agente = table.Column<string>(nullable: true),
                    Fecha_emision = table.Column<string>(nullable: true),
                    Fecha_vuelo_real = table.Column<string>(nullable: true),
                    Fecha_vuelo_programada = table.Column<string>(nullable: true),
                    Foid = table.Column<string>(nullable: true),
                    Nrotkt = table.Column<string>(nullable: true),
                    Fim = table.Column<string>(nullable: true),
                    Cupon = table.Column<string>(nullable: true),
                    Tpax = table.Column<string>(nullable: true),
                    Pax = table.Column<string>(nullable: true),
                    Contact_pax = table.Column<string>(nullable: true),
                    Class = table.Column<string>(nullable: true),
                    Fbasis = table.Column<string>(nullable: true),
                    Tour_code = table.Column<string>(nullable: true),
                    Moneda = table.Column<string>(nullable: true),
                    Importe = table.Column<string>(nullable: true),
                    Record_locator = table.Column<string>(nullable: true),
                    Carrier = table.Column<string>(nullable: true),
                    Monlocal = table.Column<string>(nullable: true),
                    Implocal = table.Column<string>(nullable: true),
                    Endoso = table.Column<string>(nullable: true),
                    Info_adicional_fc = table.Column<string>(nullable: true),
                    Sac = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KiuReports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KiuReports");
        }
    }
}
