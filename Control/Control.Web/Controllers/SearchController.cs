namespace Control.Web.Controllers
{

    using System;   
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Data.SqlClient;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc; 
    using Microsoft.Extensions.Configuration;
  

    [Authorize] // ACTIVAR SOLICITAR LOGUEO
    public class SearchController : Controller
    {
        /*
         * Controlador para gestionar la busqueda de registros de vuelo por fecha
         * solo contiene un VIEW (Index) con un formulario y una tabla
         *  - desde la tabla se navega a las paginas del CRUD (PassangerController)
         */

        private List<Passanger> PassangersData;        

        private readonly IPassangerRepository passangerRepository;//esta es la coneccion al repository para que modifique la base de datos por medio del repositorio
        private readonly IUserHelper userHelper;//esta es la conexion a las tablas de usuario
        private readonly IConfiguration configuration;  // usado para hacer llamadas a BD

        public SearchController(IPassangerRepository passangerRepository, IConfiguration configuration, IUserHelper userHelper)
        {
            this.passangerRepository = passangerRepository;//inyeccion del repositorio para la conexion a BD
            this.configuration = configuration;
            this.userHelper = userHelper;
           

        }

        // GET: Passangers 
        public IActionResult Index(string Fecha)//pagina INDEX
        {

            // System.Diagnostics.Debug.WriteLine(Fecha);
            
            if (Fecha != "")
            {
               
                System.Diagnostics.Debug.WriteLine(Fecha);
                PassangersData = GetDataByDateAsync(Fecha).Result; // .Result evita convertir el metodo en Async

                return View(PassangersData);

            }           

            string strDateFormat = string.Empty;
            strDateFormat = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

           
            PassangersData = GetDataByDateAsync(strDateFormat).Result; // .Result evita convertir el metodo en Async

            return View(PassangersData);

        
        }

       

        // ------------------------------------------------
        // metodo para traer la busqueda de los vuelos por fecha
        private async Task<List<Passanger>> GetDataByDateAsync(string date)
        {
            List<Passanger> passangersByDate = new List<Passanger>();
            string query = $"SELECT Id, PublishOn, Flight, Adult, Child, Infant, Total, ImageUrl, UserId FROM Passangers WHERE DATEDIFF(DAY, PublishOn, '{date}')=0 ORDER BY Flight";
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
                            passangersByDate.Add(new Passanger
                            {
                                Id = int.Parse(sdr["Id"].ToString()),
                                PublishOn = DateTime.Parse(sdr["PublishOn"].ToString()),                               
                                Flight = sdr["Flight"].ToString(),
                                Adult = int.Parse(sdr["Adult"].ToString()),
                                Child = int.Parse(sdr["Child"].ToString()),
                                Infant = int.Parse(sdr["Infant"].ToString()),
                                Total = int.Parse(sdr["Total"].ToString()),
                                ImageUrl = sdr["ImageUrl"].ToString(),// se envia la foto que tiene 
                                User = await this.userHelper.GetUserByIdAsync(sdr["UserId"].ToString()) // busqueda de usuario
                        });
                        }
                        con.Close();
                        return passangersByDate;
                    }
                }
            }
        }


        // -----------------------------------------------------------------------------------------------------------------------------------
        // -------------------Codigo para navegar a PassangerController para realizar el CRUD (botones de la tabla)-----------------------------------------------
        // --------------------------------------------------------------------------------------------------------------------------------


        // GET: Passangers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            System.Diagnostics.Debug.WriteLine(id);

            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");//Redireccionamiento Pagina NOT FOUND
            }

            var passanger = await this.passangerRepository.GetByIdAsync(id.Value);//se pasa el valor del repositorio
            if (passanger == null)
            {
                return new NotFoundViewResult("ProductNotFound");//Redireccionamiento Pagina NOT FOUND
            }

            // codigo para redireccionar la accion, pasa parametros por Url
            return this.RedirectToAction("Details", "Passangers", new { id = passanger.Id, page = "search" }); // view , controller , params
        }



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
          
            // codigo para redireccionar la accion, pasa parametros por Url
            return this.RedirectToAction("Edit", "Passangers", new { id = passanger.Id, page = "search" }); // view , controller , params
        }


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

            // codigo para redireccionar la accion, pasa parametros por Url
            return this.RedirectToAction("Delete", "Passangers", new { id = passanger.Id, page = "search" }); // view , controller , params
        }







    } // END class
}