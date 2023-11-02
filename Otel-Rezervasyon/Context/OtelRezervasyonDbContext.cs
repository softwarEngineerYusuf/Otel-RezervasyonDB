using Microsoft.EntityFrameworkCore;
using Otel_Rezervasyon.Entities;

namespace Otel_Rezervasyon.Context
{
    public class OtelRezervasyonDbContext : DbContext
    {

        public OtelRezervasyonDbContext(DbContextOptions<OtelRezervasyonDbContext> options) : base(options)
        {
        }

        public DbSet<Sehir> Sehirler { get; set; }
        public DbSet<Otel> Oteller { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Calisan> Calisanlar { get; set; }
        public DbSet<AltResim> AltResimler { get; set; }

    }
}
