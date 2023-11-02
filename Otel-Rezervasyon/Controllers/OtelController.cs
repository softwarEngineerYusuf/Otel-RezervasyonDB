using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Otel_Rezervasyon.Context;
using Otel_Rezervasyon.Entities;

namespace Otel_Rezervasyon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OtelController : ControllerBase
    {
        private readonly OtelRezervasyonDbContext _dbContext;

        public OtelController(OtelRezervasyonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Otel>>> GetOteller()
        {
            var oteller = await _dbContext.Oteller.ToListAsync();
            return Ok(oteller);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Otel>> GetOtel(int id)
        {
            var otel = await _dbContext.Oteller.FindAsync(id);

            if (otel == null)
            {
                return NotFound();
            }

            return Ok(otel);
        }

       
        [HttpPost]
        public async Task<ActionResult<Otel>> CreateOtel(Otel otel)
        {
            _dbContext.Oteller.Add(otel);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetOtel", new { id = otel.Id }, otel);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOtel(int id, Otel otel)
        {
            if (id != otel.Id)
            {
                return BadRequest();
            }

            var existingOtel = await _dbContext.Oteller.FindAsync(id);
            if (existingOtel == null)
            {
                return NotFound();
            }

            existingOtel.Isim = otel.Isim;
            existingOtel.TlFiyati = otel.TlFiyati;
            existingOtel.OtelResmi = otel.OtelResmi;
            existingOtel.Aciklama = otel.Aciklama;
            existingOtel.Adres = otel.Adres;
            existingOtel.Telefon = otel.Telefon;
            existingOtel.YildizSayisi = otel.YildizSayisi;
            existingOtel.Sehirİsim = otel.Sehirİsim;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOtel(int id)
        {
            var otel = await _dbContext.Oteller.FindAsync(id);
            if (otel == null)
            {
                return NotFound();
            }

            _dbContext.Oteller.Remove(otel);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool OtelExists(int id)
        {
            return _dbContext.Oteller.Any(e => e.Id == id);
        }
    }
}
