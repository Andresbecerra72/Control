namespace Control.Web.Controllers
{

    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Drawing;
    using System.Linq;
    using System.Collections.Generic;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;    
    using OfficeOpenXml;
    using OfficeOpenXml.Style;   
    
   

    [Authorize] // ACTIVAR SOLICITAR LOGUEO
    public class PassangersController : Controller
    {
        /*
         *  Controlador usado para gestionar el CRUD de los registros de cada vuelo
         */
        
        private readonly IPassangerRepository passangerRepository;//esta es la coneccion al repository para que modifique la base de datos por medio del repositorio
        private readonly IUserHelper userHelper;//esta es la conexion a las tablas de usuario

      

        public PassangersController(IPassangerRepository passangerRepository, IUserHelper userHelper)
        {
            this.passangerRepository = passangerRepository;//inyeccion del repositorio para la conexion a BD
            this.userHelper = userHelper;

        }

        // GET: Passangers 
        public IActionResult Index()//pagina INDEX
        {

            if (this.User.IsInRole("Admin") || this.User.IsInRole("Super"))
            {

                

                IEnumerable<Passanger> PassangersEmpty = Enumerable.Empty<Passanger>();
                var PassangersData = this.passangerRepository.GetAllWithUsers(); //llama del repositorio generico el metodo getAll y lo ordena por fecha                     

                return View(PassangersData);   

            }

            return View(this.passangerRepository.GetAllWithUsersAuthenticated(this.User.Identity.Name.ToString()));
        }

        // -----------------------------------------


        // --------------------------------------------------------
        // ---------------Navegación Details view---------------------
        // --------------------------------------------------------

        // GET: Passangers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");//Redireccionamiento Pagina NOT FOUND
            }

            var passanger = await this.passangerRepository.GetByIdAsync(id.Value);//se pasa el valor del repositorio
            if (passanger == null)
            {
                return new NotFoundViewResult("ProductNotFound");//Redireccionamiento Pagina NOT FOUND
            }

            return View(passanger);
        }



        // --------------------------------------------------------
        // ---------------Navegación Create view---------------------
        // --------------------------------------------------------

        // GET: Passangers/Create

        //[Authorize(Roles = "Admin")]//acesso con login a usuarios con rol de administrador
        public IActionResult Create()
        {
            return View();
        }

        // POST: Passangers/Create     * Acción
        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PassangerViewModel view)
        {
            {
                if (ModelState.IsValid)
                {
                    var path = string.Empty;

                    if (view.ImageFile != null && view.ImageFile.Length > 0)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";
                        //ruta donde se guarda la imagen de captura
                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\Passangers",
                            file);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await view.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/Passangers/{file}";
                    }
                    //crea los datos del conteo de pasajeros ingresados en el formulario
                    var passanger = this.ToPassanger(view, path);
                    passanger.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name); //ingresa con usuario logueado
                    await this.passangerRepository.CreateAsync(passanger);
                    return RedirectToAction(nameof(Index));
                }

                return View(view);


            }

        }

        //este codigo permite la conversion de la vista Passangerviewmodel al objeto Passanger con todos sus atributos
        private Passanger ToPassanger(PassangerViewModel view, string path)
        {
            return new Passanger
            {
                Id = view.Id,
                ImageUrl = path,
                Flight = view.Flight,
                Adult = view.Adult,
                Child = view.Child,
                Infant = view.Infant,
                Total = view.Total,
                PublishOn = view.PublishOn,
                User = view.User,
                Remark = view.Remark,
                Day = view.PublishOn.ToString("dd"),
                Month = view.PublishOn.ToString("MMMM"),
                Year = view.PublishOn.ToString("yyyy")
            };
        }


        // --------------------------------------------------------
        // ---------------Navegación Edit view---------------------
        // --------------------------------------------------------

        // GET: Passangers/Edit/5
        //[Authorize(Roles = "Super, Admin")]//acesso con login a usuarios con rol de administrador
        public async Task<IActionResult> Edit(int? id)
        {
           
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");//Redireccionamiento Pagina NOT FOUND
            }

            var passanger = await this.passangerRepository.GetByIdAsync(id.Value);//consulta el vuelo que va a editar
            if (passanger == null)
            {
                return new NotFoundViewResult("ProductNotFound");//Redireccionamiento Pagina NOT FOUND
            }
            var view = this.ToPassangerViewModel(passanger);
            return View(view);
        }


        //se contruye la vista se toma el modelo con los atributos del entity
        private PassangerViewModel ToPassangerViewModel(Passanger passanger)
        {
            return new PassangerViewModel
            {
                Id = passanger.Id,
                Flight = passanger.Flight,
                Adult = passanger.Adult,
                Child = passanger.Child,
                ImageUrl = passanger.ImageUrl,// se envia la foto que tiene 
                Infant = passanger.Infant,
                Total = passanger.Total,
                PublishOn = passanger.PublishOn,
                User = passanger.User,
                Remark = passanger.Remark
            };
        }



        // POST: Passangers/Edit/5    * Acción
        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PassangerViewModel view)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    var path = view.ImageUrl;

                    if (view.ImageFile != null && view.ImageFile.Length > 0)
                    {
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\images\\Passangers",
                            file);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await view.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/Passangers/{file}";
                    }

                    //actualiza los cambios del vuelo editado
                    var passanger = this.ToPassanger(view, path);
                    passanger.User = await this.userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await this.passangerRepository.UpdateAsync(passanger);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.passangerRepository.ExistAsync(view.Id))
                    {
                        return new NotFoundViewResult("ProductNotFound");//Redireccionamiento Pagina NOT FOUND
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(view);
        }

        // --------------------------------------------------------
        // ---------------Navegación Delete view---------------------
        // --------------------------------------------------------

        // GET: Passangers/Delete/5
        [Authorize(Roles = "Super, Admin")]//acesso con login a usuarios con rol de administrador
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");//Redireccionamiento Pagina NOT FOUND
            }

            var passanger = await this.passangerRepository.GetByIdAsync(id.Value);//consulta el vuelo que va a eliminar
            if (passanger == null)
            {
                return new NotFoundViewResult("ProductNotFound");//Redireccionamiento Pagina NOT FOUND
            }

            return View(passanger);
        }

        // POST: Passangers/Delete/5    * Acción
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passanger = await this.passangerRepository.GetByIdAsync(id);//pasa el dato
            await this.passangerRepository.DeleteAsync(passanger);//y salva el cambio en la base de datos
            return RedirectToAction(nameof(Index));
        }

        //REDIRECCIONAMIENTO PAGE NOT FOUND
        public IActionResult ProductNotFound()
        {
            return this.View();
        }

        //DELETE from MODAL WINDOWS**
        public async Task<IActionResult> DeleteItem(int id)
        {

            await this.passangerRepository.DeleteItemAsync(id);
            return this.RedirectToAction(nameof(Index));


        }





        // -------------Codigo para descargar y eliminar la base de datos Passanger en Excel--------------------------------------------


        // Download data in Excel
        public IActionResult DownloadExcel(string button)
        {

          if(button == "Download") {

                using (var package = new ExcelPackage())
                {


                    //Add a new worksheet to the empty workbook
                    var worksheet = package.Workbook.Worksheets.Add("Data Base");


                    // Obtiene la informacion de la BD Passangers
                    var passagersArray = this.passangerRepository.GetAllDataAsync().Result; // llega como una lista              


                    // Cabecera de la Tabla
                    worksheet.Cells[1, 1].Value = "Date";
                    worksheet.Cells[1, 2].Value = "Fligth";
                    worksheet.Cells[1, 3].Value = "Adult";
                    worksheet.Cells[1, 4].Value = "Child";
                    worksheet.Cells[1, 5].Value = "Infant";
                    worksheet.Cells[1, 6].Value = "Total";
                    worksheet.Cells[1, 7].Value = "Remark";
                    worksheet.Cells[1, 8].Value = "Image Path";
                    worksheet.Cells[1, 9].Value = "User";



                    //Add some items...
                    for (int i = 0; i < passagersArray.Count(); i++)
                    {

                        worksheet.Cells[string.Format("A{0}", i + 2)].Value = passagersArray[i].PublishOn;
                        worksheet.Cells[string.Format("B{0}", i + 2)].Value = Int32.Parse(passagersArray[i].Flight);
                        worksheet.Cells[string.Format("C{0}", i + 2)].Value = passagersArray[i].Adult;
                        worksheet.Cells[string.Format("D{0}", i + 2)].Value = passagersArray[i].Child;
                        worksheet.Cells[string.Format("E{0}", i + 2)].Value = passagersArray[i].Infant;
                        worksheet.Cells[string.Format("F{0}", i + 2)].Value = passagersArray[i].Total;
                        worksheet.Cells[string.Format("G{0}", i + 2)].Value = passagersArray[i].Remark;
                        worksheet.Cells[string.Format("H{0}", i + 2)].Value = passagersArray[i].ImageFullPath;
                        worksheet.Cells[string.Format("I{0}", i + 2)].Value = passagersArray[i].User.FullName;
                    }
                                                                          
                    //Ok now format the values;
                    using (var range = worksheet.Cells[1, 1, 1, 9])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                        range.Style.Font.Color.SetColor(Color.White);
                    }

                    worksheet.Cells[string.Format("A2:A{0}", passagersArray.Count() + 1)].Style.Numberformat.Format = "dd/MM/yyyy";   //Format as Date

                    worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

                    // Lets set the header text 
                    worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" TCP Reports";
                    // Add the page number to the footer plus the total number of pages
                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                    // Add the sheet name to the footer
                    worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
                    // Add the file path to the footer
                    // worksheet.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;

                    //worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
                    //worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

                    // Change the sheet view to show it in page layout mode
                    // worksheet.View.PageLayoutView = true;

                    // Set some document properties
                    package.Workbook.Properties.Title = "TCP Reports";
                    package.Workbook.Properties.Author = "APP Control Pasajeros";
                    package.Workbook.Properties.Comments = "Data Base From SQL Server";

                    // Set some extended property values
                    package.Workbook.Properties.Company = "SATENA SA";


                    // genera el Archivo 
                    using (var stream = new MemoryStream())
                    {
                        string strDateFormat = string.Empty;
                        strDateFormat = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                        package.SaveAs(stream);
                        var content = stream.ToArray();

                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            string.Format("TCP Reports {0}.xlsx", strDateFormat));
                    }




                }


            }
            else
            {
                this.passangerRepository.DeleteAllReportAsync();
                return this.RedirectToAction(nameof(Index));
            }
            


               // return this.RedirectToAction(nameof(Index));
        }



      

    }
}
