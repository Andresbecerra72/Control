namespace Control.Web.Controllers
{
    using Control.Web.Data;
    using Control.Web.Data.Entities;
    using Control.Web.Data.Repositories;
    using Control.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Dynamic;
    using System.Threading.Tasks;

    public class CalculateController : Controller
    {
        private readonly IKiuReportRepository kiuReportRepository;
        private readonly IPassangerRepository passangerRepository;
        private readonly IConfiguration configuration;

        public CalculateController(IKiuReportRepository kiuReportRepository, IPassangerRepository passangerRepository, IConfiguration configuration)
        {
            this.kiuReportRepository = kiuReportRepository;
            this.passangerRepository = passangerRepository;
            this.configuration = configuration;
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


        






        public IActionResult Details()
        {
            dynamic model = new ExpandoObject();
            model.Customers = this.kiuReportRepository.GetAll();
            model.Employees = this.passangerRepository.GetAll();
            // model.Customers = GetCustomers();
            // model.Employees = GetEmployees();
            return View(model);

        }





















        //******************************************************************

        private List<KiuPassanger> GetCustomers()
        {
            List<KiuPassanger> customers = new List<KiuPassanger>();
            string query = "SELECT TOP 10 PublishOnKIU, Vuelo, TotalAdult, TotalChild, TotalInfant, TotalPax FROM KiuPassanger";
            string constr = this.configuration.GetConnectionString("DefaultConnection");
            //string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            customers.Add(new KiuPassanger
                            {
                                PublishOnKIU = sdr["PublishOnKIU"].ToString(),
                                Vuelo = sdr["Vuelo"].ToString(),
                                TotalAdult = int.Parse(sdr["TotalAdult"].ToString()),
                                TotalChild = int.Parse(sdr["TotalChild"].ToString()),
                                TotalInfant = int.Parse(sdr["TotalInfant"].ToString()),
                                TotalPax = int.Parse(sdr["TotalPax"].ToString())
                            });
                        }
                    }
                    con.Close();
                    return customers;
                }
            }
        }

        private static List<Passanger> GetEmployees()
        {
            List<Passanger> employees = new List<Passanger>();
            string query = "SELECT PublishOn, Flight, Adult, Child, Infant, Total FROM Passanger";
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            employees.Add(new Passanger
                            {
                                PublishOn = DateTime.Parse(sdr["Fecha"].ToString()),
                                Flight = sdr["Flight"].ToString(),
                                Adult = int.Parse(sdr["Adult"].ToString()),
                                Child = int.Parse(sdr["Child"].ToString()),
                                Infant= int.Parse(sdr["Infant"].ToString()),
                                Total = int.Parse(sdr["Total"].ToString())
                            });
                        }
                        con.Close();
                        return employees;
                    }
                }
            }
        }






    }
}
