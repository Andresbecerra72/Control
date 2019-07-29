namespace Control.Web.Controllers
{
    using Control.Web.Data;
    using Control.Web.Data.Repositories;
    using Control.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    public class CalculateController : Controller
    {
        private readonly IKiuReportRepository kiuReportRepository;
        private readonly IPassangerRepository passangerRepository;

        public CalculateController(IKiuReportRepository kiuReportRepository, IPassangerRepository passangerRepository)
        {
            this.kiuReportRepository = kiuReportRepository;
            this.passangerRepository = passangerRepository;
        }

        //metodos


        // GET:
        public IActionResult Index()//pagina INDEX
        {
            var model = new KiuReportViewModel
            {
                Fechas = this.kiuReportRepository.GetComboFechas()

            };

            return this.View(model);

        }





    }
}
