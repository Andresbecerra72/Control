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
        private readonly DataContext _context;

        public PassangersController(DataContext context)
        {
            _context = context;
        }

        // GET: Passangers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Passangers.ToListAsync());
        }

        // GET: Passangers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passanger = await _context.Passangers
                .FirstOrDefaultAsync(m => m.PassangerId == id);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PassangerId,Flight,Adult,Child,Infant,Total,PublishOn")] Passanger passanger)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passanger);
                await _context.SaveChangesAsync();
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

            var passanger = await _context.Passangers.FindAsync(id);
            if (passanger == null)
            {
                return NotFound();
            }
            return View(passanger);
        }

        // POST: Passangers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PassangerId,Flight,Adult,Child,Infant,Total,PublishOn")] Passanger passanger)
        {
            if (id != passanger.PassangerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passanger);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassangerExists(passanger.PassangerId))
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

            var passanger = await _context.Passangers
                .FirstOrDefaultAsync(m => m.PassangerId == id);
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
            var passanger = await _context.Passangers.FindAsync(id);
            _context.Passangers.Remove(passanger);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassangerExists(int id)
        {
            return _context.Passangers.Any(e => e.PassangerId == id);
        }
    }
}
