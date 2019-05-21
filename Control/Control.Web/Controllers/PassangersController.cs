namespace Control.Web.Controllers
{
    using Data;
    using Data.Entities;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;


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
        public IActionResult Index()
        {
            return View(this.passangerRepository.GetAll());//llama del repositorio el metodo getProducts
        }

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
        public async Task<IActionResult> Create(Passanger passanger)
        {
            if (ModelState.IsValid)
            {
                passanger.User = await this.userHelper.GetUserByEmailAsync("andres.becerra@satena.com");//TODO:****pendiente por cambio por usuario logueado
                await this.passangerRepository.CreateAsync(passanger);//salva los cambios en la base de datos pormedio del repositorio
                return RedirectToAction(nameof(Index));
            }
            return View(passanger);
        }

        // GET: Passangers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passanger =  await this.passangerRepository.GetByIdAsync(id.Value);//consulta el vuelo que va a editar
            if (passanger == null)
            {
                return NotFound();
            }
            return View(passanger);
        }

        // POST: Passangers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Passanger passanger)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    //actualiza los cambios del vuelo editado
                    passanger.User = await this.userHelper.GetUserByEmailAsync("andres.becerra@satena.com");//TODO:****pendiente por cambio por usuario logueado
                    await this.passangerRepository.UpdateAsync(passanger);//salvalos cambios en la base de datos
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await this.passangerRepository.ExistAsync(passanger.Id))//valida si el vuelo existe
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
            return View(passanger);
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
