using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Control.Web.Data;
using Control.Web.Data.Entities;

namespace Control.Web.Controllers
{
    public class PassangersController : Controller
    {
        private readonly IRepository repository;//esta es la coneccion al repository para que modifique la base de datos por medio del repositorio

        public PassangersController(IRepository repository)
        {
            this.repository = repository;//inyeccion del repositorio para la conexion a BD
        }

        // GET: Passangers
        public IActionResult Index()
        {
            return View(this.repository.GetProducts());//llama del repositorio el metodo getProducts
        }

        // GET: Passangers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passanger = this.repository.GetProduct(id.Value);//se pasa el valor del repositorio
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
                this.repository.AddProduct(passanger);
                await this.repository.SaveAllAsync();//salva los cambios en la base de datos pormedio del repositorio
                return RedirectToAction(nameof(Index));
            }
            return View(passanger);
        }

        // GET: Passangers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passanger = this.repository.GetProduct(id.Value);//consulta el vuelo que va a editar
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
                    this.repository.UpdateProduct(passanger);//actualiza los cambios del vueloeeditado
                    await this.repository.SaveAllAsync();//salvalos cambios en la base de datos
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.repository.ProductExists(passanger.PassangerId))//valida si el vuelo existe
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
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passanger = this.repository.GetProduct(id.Value);//consulta el vuelo que va a eliminar
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
            var passanger = this.repository.GetProduct(id);//pasa el dato
            this.repository.RemoveProduct(passanger);//lo elimina
            await this.repository.SaveAllAsync();//y salva el cambio en la base de datos
            return RedirectToAction(nameof(Index));
        }

        
    }
}
