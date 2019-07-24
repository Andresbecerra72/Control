namespace Control.Web.Controllers
{
    using Control.Web.Data.Repositories;
    using Control.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using Control.Web.Data;

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

           //view.Total= int.Parse(this.kiuReportRepository.TotalPaxKiuReportAsync(view.PublishOn.ToString(), view.Flight).ToString());
            return View();
        }


        // GET:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(PassangerViewModel view)//pagina INDEX
        {

            view.Total = int.Parse(this.kiuReportRepository.TotalPaxKiuReportAsync(view.PublishOn.ToString(), view.Flight).ToString());

            return View(view);
        }


    }
}
