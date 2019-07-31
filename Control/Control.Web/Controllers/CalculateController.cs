namespace Control.Web.Controllers
{
    using Control.Web.Data;
    using Control.Web.Data.Entities;
    using Control.Web.Data.Repositories;
    using Control.Web.Helpers;
    using Control.Web.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Dynamic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CalculateController : Controller
    {
        private string fecha;
        private readonly IKiuReportRepository kiuReportRepository;
        private readonly IPassangerRepository passangerRepository;
        private readonly IConfiguration configuration;
        private readonly IUserHelper userHelper;

        public CalculateController(IKiuReportRepository kiuReportRepository, IPassangerRepository passangerRepository, IConfiguration configuration, IUserHelper userHelper)
        {
            this.kiuReportRepository = kiuReportRepository;
            this.passangerRepository = passangerRepository;
            this.configuration = configuration;
            this.userHelper = userHelper;
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


        
                          
       
        public  IActionResult Details(KiuReportViewModel view)
        {
            if (string.IsNullOrEmpty(view.PublishOn))
            {
                return RedirectToAction(nameof(Index));
            }

            fecha = view.PublishOn.ToString();

            dynamic model = new ExpandoObject();
            //model.KiuReporte = this.kiuReportRepository.GetAll();
            // model.TcpReporte = this.passangerRepository.GetAll();
            model.KiuReporte = GetCustomers(fecha);
            model.TcpReporte = GetEmployees(fecha);
           model.Calculate = GetCalculateTables(fecha);
            return View(model);
        }












                                    
        //******************************************************************

        private List<KiuPassanger> GetCustomers(string date)
        {
            List<KiuPassanger> customers = new List<KiuPassanger>();
            string query = $"SELECT  Flight, Adult, Child, Infant, Total FROM KiuPassangers WHERE PublishOn ='{date}' ORDER BY Flight";
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
                               // PublishOnKIU = sdr["PublishOnKIU"].ToString(),
                                Flight = sdr["Flight"].ToString(),
                                Adult = int.Parse(sdr["Adult"].ToString()),
                                Child = int.Parse(sdr["Child"].ToString()),
                                Infant = int.Parse(sdr["Infant"].ToString()),
                                Total = int.Parse(sdr["Total"].ToString())
                            });
                        }
                    }
                    con.Close();
                    return customers;
                }
            }
        }

        private List<Passanger> GetEmployees(string date)
        {
            List<Passanger> employees = new List<Passanger>();
            string query = $"SELECT Flight, Adult, Child, Infant, Total FROM Passangers WHERE PublishOn ='{date}' ORDER BY Flight";
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
                            employees.Add(new Passanger
                            {
                                //PublishOn = DateTime.Parse(sdr["PublisOn"].ToString()),
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


        //////***************DIFERENCIA ENTRE TABLAS************************///////////

        private List<KiuPassanger> GetCalculateTables(string date)
        {
            
            List<KiuPassanger> customers = new List<KiuPassanger>();
            string query = $"SELECT DISTINCT * FROM (SELECT * FROM (SELECT PublishOn, Flight, Adult, Infant, Child, Total, UserId FROM Passangers WHERE PublishOn ='{date}'  UNION ALL SELECT PublishOn, Flight, Adult, Infant, Child, Total, UserId FROM KiuPassangers WHERE PublishOn ='{date}') Tbls GROUP BY PublishOn, Flight, Adult, Infant, Child, Total, UserId HAVING COUNT(*) < 2) Diff ";
            string queri = $"Select case when A.Adult = B.Adult then '1' else '0' end as Adult, case when A.Child = B.Child then '1' else '0' end as Child, case when A.Infant = B.Infant then '1' else '0' end as Infant, case when A.Total = B.Total then '1' else '0' end as Total, case when A.Flight = B.Flight then A.Flight else 'x' end as Flight, case when A.PublishOn = B.PublishOn then A.PublishOn else 'x' end as Flight from KiuPassangers A Join Passangers B on(A.PublishOn = '{date}'AND B.PublishOn = '{date}' AND A.Flight = B.Flight);";

            string constr = this.configuration.GetConnectionString("DefaultConnection");
            //string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(queri))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        

                        while (sdr.Read())
                        {
                           // var user = this.userHelper.GetUserByIdAsync(sdr["UserId"].ToString());

                            customers.Add(new KiuPassanger
                            {
                                
                                //PublishOn = sdr["PublishOn"].ToString(),
                                Flight = sdr["Flight"].ToString(),
                                Adult = int.Parse(sdr["Adult"].ToString()),
                                Child = int.Parse(sdr["Child"].ToString()),
                                Infant = int.Parse(sdr["Infant"].ToString()),
                                Total = int.Parse(sdr["Total"].ToString()),
                               // User = user.Result


                        });
                        }
                    }
                    con.Close();
                    return customers;
                }
            }
        }



    }
}
