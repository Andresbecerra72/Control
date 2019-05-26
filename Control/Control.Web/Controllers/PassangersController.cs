namespace Control.Web.Controllers
{

    using Models;
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;

    [Authorize]
    public class PassangersController : Controller
    {
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
            return View(this.passangerRepository.GetAll().OrderBy(p => p.PublishOn));//llama del repositorio generico el metodo getAll y lo ordena por fecha
        }                                                                           //por que es esecifico del repositorio passanger

        // GET: Passangers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passanger = await this.passangerRepository.GetByIdAsync(id.Value);//se pasa el valor del repositorio
            if (passanger == null)
            {
                return NotFound();
            }

            return View(passanger);
        }

        // GET: Passangers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Passangers/Create
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
                    passanger.User = await this.userHelper.GetUserByEmailAsync("andres.becerra@satena.com");//TODO:****pendiente por cambio por usuario logueado
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
                User = view.User
            };
        }

        // GET: Passangers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passanger = await this.passangerRepository.GetByIdAsync(id.Value);//consulta el vuelo que va a editar
            if (passanger == null)
            {
                return NotFound();
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
                User = passanger.User
            };
        }

        // POST: Passangers/Edit/5
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
                    passanger.User = await this.userHelper.GetUserByEmailAsync("andres.becerra@satena.com");
                    await this.passangerRepository.UpdateAsync(passanger);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.passangerRepository.ExistAsync(view.Id))
                    {
                        return NotFound();
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

       

        // GET: Passangers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passanger = await this.passangerRepository.GetByIdAsync(id.Value);//consulta el vuelo que va a eliminar
            if (passanger == null)
            {
                return NotFound();
            }

            return View(passanger);
        }

        // POST: Passangers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passanger = await this.passangerRepository.GetByIdAsync(id);//pasa el dato
            await this.passangerRepository.DeleteAsync(passanger);//y salva el cambio en la base de datos
            return RedirectToAction(nameof(Index));
        }


    }
}
