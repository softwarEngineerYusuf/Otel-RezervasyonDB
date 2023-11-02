namespace Otel_Rezervasyon.Entities
{
    public class Otel
    {
        public int Id { get; set; }
        public string Isim { get; set; }
        public int TlFiyati { get; set; }

        public string OtelResmi { get; set; }
        public string Aciklama { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public int YildizSayisi { get; set; }
        public string Sehirİsim { get; set; }
    }
}
