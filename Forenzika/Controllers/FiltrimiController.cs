using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forenzika.Data;
using Forenzika.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forenzika.Controllers
{
    public class FiltrimiController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public FiltrimiController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            List<Kategoria> list = context.Kategoria.ToList();
            ViewData["kategorite"] = list;

            List<IdentityUser> hetuesit = context.Users.ToList();
            ViewData["hetuesit"] = hetuesit;

            Filtrimi filtrimi = new Filtrimi();

            return View(filtrimi);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Filtrimi filtrmi)
        {

            List<Kategoria> list = context.Kategoria.ToList();
            ViewData["kategorite"] = list;

            List<IdentityUser> hetuesit = context.Users.ToList();
            ViewData["hetuesit"] = hetuesit;

            IQueryable<Rasti> query = context.Rasti.AsQueryable();

            if (filtrmi.fromDate > filtrmi.toDate)
            {
                return Ok("from date eshte me e madhe se to date");
            }

            if (filtrmi.fromDate.HasValue)
            {
                query = query.Where(r => r.Data > filtrmi.fromDate);
            }

            if (filtrmi.toDate.HasValue)
            {
                query = query.Where(r => r.Data < filtrmi.toDate);
            }
            if (filtrmi.kategoria.HasValue)
            {
                Kategoria kategoria = context.Kategoria.Find(filtrmi.kategoria);
                query = query.Where(r => r.Kategoria == kategoria);

            }


            IdentityUser hetuesi = await userManager.FindByIdAsync(filtrmi.hetuesi.ToString());
            query = query.Where(r => r.Hetuesi == hetuesi);

            filtrmi.rastet = query.ToList();

            return View(filtrmi);
        }
    }
}