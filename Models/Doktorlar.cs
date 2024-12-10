using System;
using System.Collections.Generic;

namespace hospitalfinal.Models;

public partial class Doktorlar
{
    public int DoktorId { get; set; }

    public string DoktorAd { get; set; } = null!;

    public string DoktorSoyad { get; set; } = null!;

    public string DoktorTc { get; set; } = null!;

    public int BransId { get; set; }

    public string Telefon { get; set; } = null!;

    public string? Email { get; set; }

    public string Sifre { get; set; } = null!;

    public string? Uzmanlik { get; set; }

    public int Deneyim { get; set; }

    public virtual Branslar Brans { get; set; } = null!;

    public virtual ICollection<RandevuMusait> RandevuMusaits { get; set; } = new List<RandevuMusait>();

    public virtual ICollection<Randevular> Randevulars { get; set; } = new List<Randevular>();
}
