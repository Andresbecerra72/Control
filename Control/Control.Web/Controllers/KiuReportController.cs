namespace Control.Web.Controllers
{
    using Control.Web.Data.Entities;
    using Control.Web.Data.Repositories;
    using Control.Web.Helpers;
    using Control.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using OfficeOpenXml;
    using System;
    using System.IO;
    using System.Threading.Tasks;


    [Authorize(Roles = "Super, Admin")]
    public class KiuReportController : Controller
    {

        private string publishOnKiu;
        private string vuelo;
        private int totalAdult = 0;
        private int totalChild = 0;
        private int totalInfant = 0;
        private int totalPax = 0;
        private DateTime dateKiu;
        private string day;
        private string month;
        private string year;

        private readonly IKiuReportRepository kiuReportRepository;
        private readonly IUserHelper userHelper;

        public KiuReportController(IKiuReportRepository kiuReportRepository, IUserHelper userHelper)
        {
            this.kiuReportRepository = kiuReportRepository;
            this.userHelper = userHelper;
        }

        //metodos

        // GET: KIU REPORT listado de reportes ingresadors en la BD
        public IActionResult Index()//pagina INDEX
        {

            return View(this.kiuReportRepository.GetAll());
            
        }


        // POST: KIU REPORT importa el archivo de excel en la BD 
        public IActionResult Create()//pagina INDEX
        {
            return View();
        }

        //ESTE POST PERMITE LEER EL ARCHIVO DE EXCEL Y GUARDARLO EN LA BD KIUPASSANGER
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<DemoResponse<List<KiuReport>>> Index(KiuReportViewModel formFile)
        public async Task<IActionResult> Create(KiuReportViewModel view)
        {


            if (view.ExcelFile != null || view.ExcelFile.Length > 0)
            {
                if (Path.GetExtension(view.ExcelFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {


                    using (var stream = new MemoryStream())
                    {
                        await view.ExcelFile.CopyToAsync(stream);

                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowCount = worksheet.Dimension.Rows;

                            for (int row = 2; row <= rowCount; row++)
                            {


                                //Condicion cuando Fecha ==  / vuelo ==
                                if (worksheet.Cells[row, 13].Text.ToString().Trim() == worksheet.Cells[row + 1, 13].Text.ToString().Trim() &&
                                    worksheet.Cells[row, 2].Text.ToString().Trim() == worksheet.Cells[row + 1, 2].Text.ToString().Trim())
                                {
                                    if (worksheet.Cells[row, 13].Text.ToString().Trim() == worksheet.Cells[row + 1, 13].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 2].Text.ToString().Trim() == worksheet.Cells[row + 1, 2].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 19].Text.ToString().Trim() == "A")
                                    {
                                        totalAdult = totalAdult + 1;

                                    }
                                    else if (worksheet.Cells[row, 13].Text.ToString().Trim() == worksheet.Cells[row + 1, 13].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 2].Text.ToString().Trim() == worksheet.Cells[row + 1, 2].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 19].Text.ToString().Trim() == "C")
                                    {

                                        totalChild = totalChild + 1;

                                    }
                                    else if (worksheet.Cells[row, 13].Text.ToString().Trim() == worksheet.Cells[row + 1, 13].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 2].Text.ToString().Trim() == worksheet.Cells[row + 1, 2].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 19].Text.ToString().Trim() == "I")
                                    {

                                        totalInfant = totalInfant + 1;

                                    }

                                    dateKiu = DateTime.Parse(worksheet.Cells[row, 13].Value.ToString(), null);
                                    publishOnKiu = dateKiu.ToString("yyyy-MM-dd");// cambia el formato de la fecha, queda como string
                                    day = dateKiu.ToString("dd");
                                    month = dateKiu.ToString("MM");
                                    year = dateKiu.ToString("yyyy");
                                    vuelo = worksheet.Cells[row, 2].Text.ToString().Trim();





                                }
                                //Condicion cuando Fecha ==  / vuelo !=
                                else if (worksheet.Cells[row, 13].Text.ToString().Trim() == worksheet.Cells[row + 1, 13].Text.ToString().Trim() &&
                                    worksheet.Cells[row, 2].Text.ToString().Trim() != worksheet.Cells[row + 1, 2].Text.ToString().Trim())
                                {
                                    if (worksheet.Cells[row, 13].Text.ToString().Trim() == worksheet.Cells[row + 1, 13].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 2].Text.ToString().Trim() != worksheet.Cells[row + 1, 2].Text.ToString().Trim()
                                         && worksheet.Cells[row, 19].Text.ToString().Trim() == "A")
                                    {
                                        totalAdult = totalAdult + 1;
                                    }
                                    else if (worksheet.Cells[row, 13].Text.ToString().Trim() == worksheet.Cells[row + 1, 13].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 2].Text.ToString().Trim() != worksheet.Cells[row + 1, 2].Text.ToString().Trim()
                                         && worksheet.Cells[row, 19].Text.ToString().Trim() == "C")
                                    {
                                        totalChild = totalChild + 1;
                                    }
                                    else if (worksheet.Cells[row, 13].Text.ToString().Trim() == worksheet.Cells[row + 1, 13].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 2].Text.ToString().Trim() != worksheet.Cells[row + 1, 2].Text.ToString().Trim()
                                         && worksheet.Cells[row, 19].Text.ToString().Trim() == "I")
                                    {
                                        totalInfant = totalInfant + 1;
                                    }

                                    await GrabarAsync();


                                }
                                //Condicion cuando Fecha !=   / vuelo != 
                                else if (worksheet.Cells[row, 13].Text.ToString().Trim() != worksheet.Cells[row + 1, 13].Text.ToString().Trim() &&
                                    worksheet.Cells[row, 2].Text.ToString().Trim() != worksheet.Cells[row + 1, 2].Text.ToString().Trim())
                                {
                                    if (worksheet.Cells[row, 13].Text.ToString().Trim() != worksheet.Cells[row + 1, 13].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 2].Text.ToString().Trim() != worksheet.Cells[row + 1, 2].Text.ToString().Trim()
                                         && worksheet.Cells[row, 19].Text.ToString().Trim() == "A")
                                    {
                                        totalAdult = totalAdult + 1;
                                    }
                                    else if (worksheet.Cells[row, 13].Text.ToString().Trim() != worksheet.Cells[row + 1, 13].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 2].Text.ToString().Trim() != worksheet.Cells[row + 1, 2].Text.ToString().Trim()
                                         && worksheet.Cells[row, 19].Text.ToString().Trim() == "C")
                                    {
                                        totalChild = totalChild + 1;
                                    }
                                    else if (worksheet.Cells[row, 13].Text.ToString().Trim() != worksheet.Cells[row + 1, 13].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 2].Text.ToString().Trim() != worksheet.Cells[row + 1, 2].Text.ToString().Trim()
                                         && worksheet.Cells[row, 19].Text.ToString().Trim() == "I")
                                    {
                                        totalInfant = totalInfant + 1;
                                    }

                                    await GrabarAsync();



                                }

                                //Condicion cuando Fecha == RowCount  / vuelo == RowCount
                                else if (worksheet.Cells[row, 13].Text.ToString().Trim() == worksheet.Cells[rowCount, 13].Text.ToString().Trim())
                                {

                                    if (worksheet.Cells[row, 13].Text.ToString().Trim() == worksheet.Cells[rowCount, 13].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 2].Text.ToString().Trim() == worksheet.Cells[rowCount, 2].Text.ToString().Trim()
                                         && worksheet.Cells[rowCount, 19].Text.ToString().Trim() == "A")
                                    {
                                        totalAdult = totalAdult + 1;
                                    }
                                    else if (worksheet.Cells[row, 13].Text.ToString().Trim() == worksheet.Cells[rowCount, 13].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 2].Text.ToString().Trim() == worksheet.Cells[rowCount, 2].Text.ToString().Trim()
                                         && worksheet.Cells[rowCount, 19].Text.ToString().Trim() == "C")
                                    {
                                        totalChild = totalChild + 1;
                                    }
                                    else if (worksheet.Cells[row, 13].Text.ToString().Trim() == worksheet.Cells[rowCount, 13].Text.ToString().Trim() &&
                                        worksheet.Cells[row, 2].Text.ToString().Trim() == worksheet.Cells[rowCount, 2].Text.ToString().Trim()
                                         && worksheet.Cells[rowCount, 19].Text.ToString().Trim() == "I")
                                    {
                                        totalInfant = totalInfant + 1;
                                    }

                                    await GrabarAsync();
                                }


                            }



                        }
                        // await this.kiuReportRepository.CreateKiuReportAsync(list);
                        return RedirectToAction(nameof(Index));
                    }


                }
                return NotFound();
            }
            return NotFound();


        }

        //metodo para almacenar la lista en la BD KiuPassanger
        public async Task GrabarAsync()
        {
            var user = await this.userHelper.GetUserByEmailAsync("administrador.kiu@satena.com");
            totalPax = totalAdult + totalChild + totalInfant;

            await this.kiuReportRepository.CreateAsync(new KiuPassanger
            {
                Flight = vuelo,
                PublishOn = publishOnKiu,
                Adult = totalAdult,
                Child = totalChild,
                Infant = totalInfant,
                Total = totalPax,
                Day = day,
                Month = month,
                Year = year,
                User = user
        


            });
            //reset de las variables
            totalAdult = 0;
            totalChild = 0;
            totalInfant = 0;
            totalPax = 0;

        }



        // GET: Passangers/Delete/5

        public async Task<IActionResult> Delete()
        {

            await this.kiuReportRepository.DeleteKiuReportAsync();

            return RedirectToAction(nameof(Index));


        }







    }
}
