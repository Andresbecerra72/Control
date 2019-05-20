using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Control.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Control.Web.Data.Entities;


namespace Control.Web.Controllers
{
    public class PassangerController : Controller
    {
        // GET: Passangers
        public IActionResult Index()
        {
            DataContext context = HttpContext.RequestServices.GetService(typeof(Control.Web.Data.DataContext)) as DataContext;
            return View(context.GetAllPassanger());
        }
        // GET: Passangers/Create
        public IActionResult Create()
        {

            return View();
        }
        
    }
}