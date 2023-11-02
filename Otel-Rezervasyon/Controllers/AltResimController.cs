using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otel_Rezervasyon.Context;
using Otel_Rezervasyon.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Otel_Rezervasyon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AltResimController : ControllerBase
    {
        private readonly OtelRezervasyonDbContext _dbContext;

        public AltResimController(OtelRezervasyonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AltResim>>> GetAltResimler()
        {
            var altResimler = await _dbContext.AltResimler.ToListAsync();
            return Ok(altResimler);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AltResim>> GetAltResim(int id)
        {
            var altResim = await _dbContext.AltResimler.FindAsync(id);

            if (altResim == null)
            {
                return NotFound();
            }

            return Ok(altResim);
        }

        [HttpPost]
        public async Task<ActionResult<AltResim>> CreateAltResim(AltResim altResim)
        {
            _dbContext.AltResimler.Add(altResim);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetAltResim", new { id = altResim.Id }, altResim);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAltResim(int id, AltResim altResim)
        {
            if (id != altResim.Id)
            {
                return BadRequest();
            }

            var existingAltResim = await _dbContext.AltResimler.FindAsync(id);
            if (existingAltResim == null)
            {
                return NotFound();
            }

            existingAltResim.AltResimUrl = altResim.AltResimUrl; 
            existingAltResim.Otelİsim = altResim.Otelİsim;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAltResim(int id)
        {
            var altResim = await _dbContext.AltResimler.FindAsync(id);
            if (altResim == null)
            {
                return NotFound();
            }

            _dbContext.AltResimler.Remove(altResim);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool AltResimExists(int id)
        {
            return _dbContext.AltResimler.Any(e => e.Id == id);
        }
    }
}

