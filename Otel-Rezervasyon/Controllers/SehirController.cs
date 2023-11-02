using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otel_Rezervasyon.Context;
using Otel_Rezervasyon.Entities;

namespace Otel_Rezervasyon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SehirController : ControllerBase
    {
        private readonly OtelRezervasyonDbContext _dbContext;

        public SehirController(OtelRezervasyonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sehir>>> GetSehirler()
        {
            var sehirler = await _dbContext.Sehirler.ToListAsync();
            return Ok(sehirler);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Sehir>> GetSehir(int id)
        {
            var sehir = await _dbContext.Sehirler.FindAsync(id);

            if (sehir == null)
            {
                return NotFound();
            }

            return Ok(sehir);
        }

      
        [HttpPost]
        public async Task<ActionResult<Sehir>> CreateSehir(Sehir sehir)
        {
            _dbContext.Sehirler.Add(sehir);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetSehir", new { id = sehir.Id }, sehir);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSehir(int id, Sehir sehir)
        {
            if (id != sehir.Id)
            {
                return BadRequest();
            }

            var existingSehir = await _dbContext.Sehirler.FindAsync(id);
            if (existingSehir == null)
            {
                return NotFound();
            }

            existingSehir.Isim = sehir.Isim;
            existingSehir.SehirResim = sehir.SehirResim;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSehir(int id)
        {
            var sehir = await _dbContext.Sehirler.FindAsync(id);
            if (sehir == null)
            {
                return NotFound();
            }

            _dbContext.Sehirler.Remove(sehir);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool SehirExists(int id)
        {
            return _dbContext.Sehirler.Any(e => e.Id == id);
        }
    }
}
