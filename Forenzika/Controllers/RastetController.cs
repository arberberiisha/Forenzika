using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forenzika.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Forenzika.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Forenzika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RastetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public RastetController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: api/Rastet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rasti>>> GetRasti()
        {
            return await _context.Rasti.ToListAsync();
        }

        // GET: api/Rastet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rasti>> GetRasti(int id)
        {
            var rasti = await _context.Rasti.FindAsync(id);

            if (rasti == null)
            {
                return NotFound();
            }

            return rasti;
        }

        // PUT: api/Rastet/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRasti(int id, Rasti rasti)
        {
            if (id != rasti.ID)
            {
                return BadRequest();
            }

            _context.Entry(rasti).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RastiExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rastet
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Rasti>> PostRasti(RastiViewModel rastivm)
        {
            ClaimsIdentity claimsIdentity = this.User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            IdentityUser identityUser = await userManager.FindByIdAsync(userId);

            Rasti rasti = new Rasti
            {
                EmriRastit = rastivm.EmriRastit,
                Vendi = rastivm.Vendi,
                Pershkrimi = rastivm.Pershkrimi,
                Data = rastivm.Data,
                Hetuesi = identityUser,
                IAkuzuari = _context.Personi.Find(rastivm.IAkuzuari),
                Viktimi = _context.Personi.Find(rastivm.Viktimi),
                Kategoria = _context.Kategoria.Find(rastivm.Kategoria)
            };

            _context.Rasti.Add(rasti);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRasti", new { id = rasti.ID }, rasti);
        }

        // DELETE: api/Rastet/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rasti>> DeleteRasti(int id)
        {
            var rasti = await _context.Rasti.FindAsync(id);
            if (rasti == null)
            {
                return NotFound();
            }

            _context.Rasti.Remove(rasti);
            await _context.SaveChangesAsync();

            return rasti;
        }

        private bool RastiExists(int id)
        {
            return _context.Rasti.Any(e => e.ID == id);
        }
    }
}
