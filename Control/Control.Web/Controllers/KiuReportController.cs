using Control.Web.Data.Entities;
using Control.Web.Data.Repositories;
using Control.Web.Helpers;
using Control.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Control.Web.Controllers
{
    [Authorize(Roles = "Super, Admin")]
    public class KiuReportController : Controller
    {
       

        private readonly IKiuReportRepository kiuReportRepository;



        public KiuReportController(IKiuReportRepository kiuReportRepository)
        {
            this.kiuReportRepository = kiuReportRepository;

        }



        // GET: KIU REPORT listado de reortes ingresadors en la BD
        public IActionResult Index()//pagina INDEX
        {

            return View(this.kiuReportRepository.GetAll());
           // return View();
        }


        // POST: KIU REPORT importa el archivo de excel en la BD 
        public IActionResult Create()//pagina INDEX
        {


            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<DemoResponse<List<KiuReport>>> Index(KiuReportViewModel formFile)
        public async Task<IActionResult> Create(KiuReportViewModel view)
        {
            

            if (view.ExcelFile != null || view.ExcelFile.Length > 0)
            {
                if (Path.GetExtension(view.ExcelFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    var list = new List<KiuReport>();

                    using (var stream = new MemoryStream())
                    {
                        await view.ExcelFile.CopyToAsync(stream);

                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                            var rowCount = worksheet.Dimension.Rows;

                            for (int row = 2; row <= rowCount; row++)
                            {


                                list.Add(new KiuReport
                                {

                                    Hour = worksheet.Cells[row, 1].Text.ToString().Trim(),
                                    Vuelo = worksheet.Cells[row, 2].Text.ToString().Trim(),
                                    Origen_Itinerario = worksheet.Cells[row, 3].Text.ToString().Trim(),
                                    Source = worksheet.Cells[row, 4].Text.ToString().Trim(),
                                    Dest = worksheet.Cells[row, 5].Text.ToString().Trim(),
                                    Equipo = worksheet.Cells[row, 6].Text.ToString().Trim(),
                                    Matricula = worksheet.Cells[row, 7].Text.ToString().Trim(),
                                    Delay = worksheet.Cells[row, 8].Text.ToString().Trim(),
                                    Pais_emision = worksheet.Cells[row, 9].Text.ToString().Trim(),
                                    Emisor = worksheet.Cells[row, 10].Text.ToString().Trim(),
                                    Agente = worksheet.Cells[row, 11].Text.ToString().Trim(),
                                    Fecha_emision = worksheet.Cells[row, 12].Value.ToString().Trim(),
                                    Fecha_vuelo_real = worksheet.Cells[row, 13].Value.ToString().Trim(),
                                    Fecha_vuelo_programada = worksheet.Cells[row, 14].Value.ToString().Trim(),
                                    Foid = worksheet.Cells[row, 15].Text.ToString().Trim(),
                                    Nrotkt = worksheet.Cells[row, 16].Value.ToString().Trim(),
                                    Fim = worksheet.Cells[row, 17].Value.ToString().Trim(),
                                    Cupon = worksheet.Cells[row, 18].Value.ToString().Trim(),
                                    Tpax = worksheet.Cells[row, 19].Value.ToString().Trim(),
                                    Pax = worksheet.Cells[row, 20].Value.ToString().Trim(),
                                    Contact_pax = worksheet.Cells[row, 21].Text.ToString().Trim(),
                                    Class = worksheet.Cells[row, 22].Value.ToString().Trim(),
                                    Fbasis = worksheet.Cells[row, 23].Value.ToString().Trim(),
                                    Tour_code = worksheet.Cells[row, 24].Text.ToString().Trim(),
                                    Moneda = worksheet.Cells[row, 25].Value.ToString().Trim(),
                                    Importe = worksheet.Cells[row, 26].Value.ToString().Trim(),
                                    Record_locator = worksheet.Cells[row, 27].Value.ToString().Trim(),
                                    Carrier = worksheet.Cells[row, 28].Value.ToString().Trim(),
                                    Monlocal = worksheet.Cells[row, 29].Value.ToString().Trim(),
                                    Implocal = worksheet.Cells[row, 30].Value.ToString().Trim(),
                                    Endoso = worksheet.Cells[row, 31].Value.ToString().Trim(),
                                    Info_adicional_fc = worksheet.Cells[row, 32].Text.ToString().Trim(),
                                    Sac = worksheet.Cells[row, 33].Text.ToString().Trim()

                                    //Age = int.Parse(worksheet.Cells[row, 2].Value.ToString().Trim()),
                                });
                            }
                        }
                        await this.kiuReportRepository.CreateKiuReportAsync(list);
                        return RedirectToAction(nameof(Index));
                    }


                }
                return NotFound();
            }
            return NotFound();

            
        }





        // GET: Passangers/Delete/5
       
        public async Task<IActionResult> Delete()
        {

            await this.kiuReportRepository.DeleteKiuReportAsync();

            return RedirectToAction(nameof(Index));


        }

      





    }
}
