﻿namespace Control.Web.Controllers
{
    using Control.Web.Data.Entities;
    using Control.Web.Data.Repositories;
    using Control.Web.Helpers;
    using Control.Web.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Dynamic;

    [Authorize(Roles = "Super, Admin")]
    public class CalculateController : Controller
    {
        private string fecha;
       
        private readonly IKiuReportRepository kiuReportRepository;
        private readonly IConfiguration configuration;
        private readonly IUserHelper userHelper;

        public CalculateController(IKiuReportRepository kiuReportRepository, IConfiguration configuration, IUserHelper userHelper)
        {
            this.kiuReportRepository = kiuReportRepository;
            this.configuration = configuration;
            this.userHelper = userHelper;
        }

        //metodos


        // GET:
        public IActionResult Index()//pagina INDEX
        {


            var model = new KiuReportViewModel
            {
                Fechas = this.kiuReportRepository.GetComboFechas()//este codigo carga las fechas del reporte kiu (archivo de excel)

            };

            return this.View(model);

        }





        public IActionResult Details(KiuReportViewModel view)
        {
            if (string.IsNullOrEmpty(view.PublishOn))
            {
                return RedirectToAction(nameof(Index));//si no hay fechas cargadas devuelve la pagina index
            }
            
            fecha = view.PublishOn;// almacena la fecha seleccionada
            


            dynamic model = new ExpandoObject();
           
            //devuelve las comparaciones y consultas a las tablas KiuPassangers y Passangers
            model.KiuReporte = GetCustomers(fecha);
            model.TcpReporte = GetEmployees(fecha);
            model.Calculate = GetCalculateTables(fecha);
            model.GroupBy = GetCalculateTablesGroupBy(fecha);
            model.Fecha = view.PublishOn.ToString();
            return View(model);
        }



         //metodos de consulta que comparan las tablas KiuPassangers y Passangers                           

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
            string query = $"SELECT Flight, Adult, Child, Infant, Total FROM Passangers WHERE DATEDIFF(DAY, PublishOn, '{date}')=0 ORDER BY Flight";
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
                                Infant = int.Parse(sdr["Infant"].ToString()),
                                Total = int.Parse(sdr["Total"].ToString())
                            });
                        }
                        con.Close();
                        return employees;
                    }
                }
            }
        }


        //////***************DIFERENCIA ENTRE TABLAS MisMatch/Match ************************///////////

        private List<KiuPassanger> GetCalculateTables(string date)
        {

            List<KiuPassanger> customers = new List<KiuPassanger>();

            string queri = $"Select case when A.Adult = B.Adult then '0' else '99' end as Adult, case when A.Child = B.Child then '0' else '99' end as Child, case when A.Infant = B.Infant then '0' else '99' end as Infant, case when A.Total = B.Total then '0' else '99' end as Total, case when A.Flight = B.Flight then A.Flight else 'x' end as Flight, case when A.PublishOn = B.PublishOn then A.PublishOn else 'x' end as Flight from KiuPassangers A Join Passangers B on(A.PublishOn = '{date}' AND DATEDIFF(DAY, B.PublishOn, '{date}')=0 AND A.Flight = B.Flight);";
            string constr = this.configuration.GetConnectionString("DefaultConnection");

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


                            customers.Add(new KiuPassanger
                            {

                                //PublishOn = sdr["PublishOn"].ToString(),
                                Flight = sdr["Flight"].ToString(),
                                Adult = int.Parse(sdr["Adult"].ToString()),
                                Child = int.Parse(sdr["Child"].ToString()),
                                Infant = int.Parse(sdr["Infant"].ToString()),
                                Total = int.Parse(sdr["Total"].ToString()),



                            });
                        }
                    }
                    con.Close();
                    return customers;
                }
            }
        }


        //************************COMPARA TABLAS UNA/UNA*******************************
        private List<KiuPassanger> GetCalculateTablesGroupBy(string date)
        {

            List<KiuPassanger> customers = new List<KiuPassanger>();

            string query = $"SELECT DISTINCT * FROM (SELECT * FROM (SELECT Flight, Adult, Infant, Child, Total FROM Passangers WHERE DATEDIFF(DAY, PublishOn, '{date}')=0  UNION ALL SELECT Flight, Adult, Infant, Child, Total FROM KiuPassangers WHERE PublishOn ='{date}') Tbls GROUP BY Flight, Adult, Infant, Child, Total HAVING COUNT(*) < 2) Diff ";
            string constr = this.configuration.GetConnectionString("DefaultConnection");

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
                            //var user = this.userHelper.GetUserByIdAsync(sdr["UserId"].ToString());

                            customers.Add(new KiuPassanger
                            {

                                //PublishOn = sdr["PublishOn"].ToString(),
                                Flight = sdr["Flight"].ToString(),
                                Adult = int.Parse(sdr["Adult"].ToString()),
                                Child = int.Parse(sdr["Child"].ToString()),
                                Infant = int.Parse(sdr["Infant"].ToString()),
                                Total = int.Parse(sdr["Total"].ToString()),
                                //User = user.Result


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