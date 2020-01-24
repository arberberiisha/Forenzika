using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forenzika.Data;
using Microsoft.AspNetCore.Identity;
using Forenzika.Models;
using Microsoft.AspNetCore.Authorization;

namespace Forenzika.Controllers
{
    [Authorize]
    public class RastisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager;

        public RastisController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Rastis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rasti.Include(r => r.Hetuesi).ToListAsync());
        }

        // GET: Rastis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rasti = await _context.Rasti
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rasti == null)
            {
                return NotFound();
            }

            return View(rasti);
        }

        // GET: Rastis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rastis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RastiViewModel rastivm)
        {
            if (ModelState.IsValid)
            {
                Rasti rasti = new Rasti
                {
                    EmriRastit = rastivm.EmriRastit,

                    Data = rastivm.Data,

                    Pershkrimi = rastivm.Pershkrimi,
                    Vendi = rastivm.Vendi,
                    IAkuzuari = _context.Personi.Find(rastivm.IAkuzuari),
                    Viktimi = _context.Personi.Find(rastivm.Viktimi),
                    Kategoria = _context.Kategoria.Find(rastivm.Kategoria),
                    Hetuesi = await userManager.GetUserAsync(User)
                };

                _context.Add(rasti);
                await _context.SaveChangesAsync();
                rastivm.ID = rasti.ID;
                return RedirectToAction(nameof(Index));
            }
            return View(rastivm);
        }

        // GET: Rastis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rasti = await _context.Rasti.FindAsync(id);
            if (rasti == null)
            {
                return NotFound();
            }
            return View(rasti);
        }

        // POST: Rastis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,EmriRastit,Vendi,Pershkrimi,Data")] Rasti rasti)
        {
            if (id != rasti.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rasti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RastiExists(rasti.ID))
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
            return View(rasti);
        }

        // GET: Rastis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rasti = await _context.Rasti
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rasti == null)
            {
                return NotFound();
            }

            return View(rasti);
        }

        // POST: Rastis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rasti = await _context.Rasti.FindAsync(id);
            _context.Rasti.Remove(rasti);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RastiExists(int id)
        {
            return _context.Rasti.Any(e => e.ID == id);
        }
    }
}
