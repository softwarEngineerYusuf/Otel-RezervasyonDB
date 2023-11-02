using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otel_Rezervasyon.Context;
using Otel_Rezervasyon.Entities;

namespace Otel_Rezervasyon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalisanController : ControllerBase
    {
        private readonly OtelRezervasyonDbContext _dbContext;

        public CalisanController(OtelRezervasyonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calisan>>> GetCalisanlar()
        {
            var calisanlar = await _dbContext.Calisanlar.ToListAsync();
            return Ok(calisanlar);
        }

     
        [HttpGet("{id}")]
        public async Task<ActionResult<Calisan>> GetCalisan(int id)
        {
            var calisan = await _dbContext.Calisanlar.FindAsync(id);

            if (calisan == null)
            {
                return NotFound();
            }

            return Ok(calisan);
        }

      
        [HttpPost]
        public async Task<ActionResult<Calisan>> CreateCalisan(Calisan calisan)
        {
            _dbContext.Calisanlar.Add(calisan);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetCalisan", new { id = calisan.Id }, calisan);
        }

     
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCalisan(int id, Calisan calisan)
        {
            if (id != calisan.Id)
            {
                return BadRequest();
            }

            var existingCalisan = await _dbContext.Calisanlar.FindAsync(id);
            if (existingCalisan == null)
            {
                return NotFound();
            }

            existingCalisan.Isim = calisan.Isim;
            existingCalisan.Soyisim = calisan.Soyisim;
            existingCalisan.Telefon = calisan.Telefon;
            existingCalisan.Email = calisan.Email;
            existingCalisan.Departman = calisan.Departman;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalisan(int id)
        {
            var calisan = await _dbContext.Calisanlar.FindAsync(id);
            if (calisan == null)
            {
                return NotFound();
            }

            _dbContext.Calisanlar.Remove(calisan);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool CalisanExists(int id)
        {
            return _dbContext.Calisanlar.Any(e => e.Id == id);
        }
    }
}
