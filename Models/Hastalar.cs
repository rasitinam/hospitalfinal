using System;
using System.Collections.Generic;

namespace hospitalfinal.Models;

public partial class Hastalar
{
    public int HastaId { get; set; }

    public string HastaAd { get; set; } = null!;

    public string HastaSoyad { get; set; } = null!;

    public string Tc { get; set; } = null!;

    public string? Telefon { get; set; }

    public string? Email { get; set; }

    public string Adres { get; set; } = null!;

    public DateTime? DogumTarihi { get; set; }

    public string Sifre { get; set; } = null!;

    public virtual ICollection<Randevular> Randevulars { get; set; } = new List<Randevular>();
}
