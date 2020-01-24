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

namespace Forenzika.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonatController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Personat
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personi>>> GetPersoni()
        {
            return await _context.Personi.ToListAsync();
        }

        // GET: api/Personat/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personi>> GetPersoni(int id)
        {
            var personi = await _context.Personi.FindAsync(id);

            if (personi == null)
            {
                return NotFound();
            }

            return personi;
        }

        // PUT: api/Personat/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersoni(int id, Personi personi)
        {
            if (id != personi.Id)
            {
                return BadRequest();
            }

            _context.Entry(personi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersoniExists(id))
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

        // POST: api/Personat
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Personi>> PostPersoni(Personi personi)
        {
            _context.Personi.Add(personi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersoni", new { id = personi.Id }, personi);
        }

        // DELETE: api/Personat/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Personi>> DeletePersoni(int id)
        {
            var personi = await _context.Personi.FindAsync(id);
            if (personi == null)
            {
                return NotFound();
            }

            _context.Personi.Remove(personi);
            await _context.SaveChangesAsync();

            return personi;
        }

        private bool PersoniExists(int id)
        {
            return _context.Personi.Any(e => e.Id == id);
        }
    }
}
