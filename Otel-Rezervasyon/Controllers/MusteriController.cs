using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otel_Rezervasyon.Context;
using Otel_Rezervasyon.Entities;

namespace Otel_Rezervasyon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusteriController : ControllerBase
    {
        private readonly OtelRezervasyonDbContext _dbContext;

        public MusteriController(OtelRezervasyonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Musteri>>> GetMusteriler()
        {
            var musteriler = await _dbContext.Musteriler.ToListAsync();
            return Ok(musteriler);
        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<Musteri>> GetMusteri(int id)
        {
            var musteri = await _dbContext.Musteriler.FindAsync(id);

            if (musteri == null)
            {
                return NotFound();
            }

            return Ok(musteri);
        }

        [HttpPost]
        public async Task<ActionResult<Musteri>> CreateMusteri(Musteri musteri)
        {
            _dbContext.Musteriler.Add(musteri);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetMusteri", new { id = musteri.Id }, musteri);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMusteri(int id, Musteri musteri)
        {
            if (id != musteri.Id)
            {
                return BadRequest();
            }

            var existingMusteri = await _dbContext.Musteriler.FindAsync(id);
            if (existingMusteri == null)
            {
                return NotFound();
            }

            existingMusteri.Isim = musteri.Isim;
            existingMusteri.Soyisim = musteri.Soyisim;
            existingMusteri.Email = musteri.Email;
            existingMusteri.Telefon = musteri.Telefon;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusteri(int id)
        {
            var musteri = await _dbContext.Musteriler.FindAsync(id);
            if (musteri == null)
            {
                return NotFound();
            }

            _dbContext.Musteriler.Remove(musteri);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool MusteriExists(int id)
        {
            return _dbContext.Musteriler.Any(e => e.Id == id);
        }
    }
}
