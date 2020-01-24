using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forenzika.Data;

namespace Forenzika.Controllers
{
    public class PersoniController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersoniController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Personi.ToListAsync());
        }

        // GET: Personi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personi = await _context.Personi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personi == null)
            {
                return NotFound();
            }

            return View(personi);
        }

        // GET: Personi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Emri,Mbiemri,NumriPersonal,Adresa,DataLindjes")] Personi personi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personi);
        }

        // GET: Personi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personi = await _context.Personi.FindAsync(id);
            if (personi == null)
            {
                return NotFound();
            }
            return View(personi);
        }

        // POST: Personi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Emri,Mbiemri,NumriPersonal,Adresa,DataLindjes")] Personi personi)
        {
            if (id != personi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersoniExists(personi.Id))
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
            return View(personi);
        }

        // GET: Personi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personi = await _context.Personi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personi == null)
            {
                return NotFound();
            }

            return View(personi);
        }

        // POST: Personi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personi = await _context.Personi.FindAsync(id);
            _context.Personi.Remove(personi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersoniExists(int id)
        {
            return _context.Personi.Any(e => e.Id == id);
        }
    }
}
