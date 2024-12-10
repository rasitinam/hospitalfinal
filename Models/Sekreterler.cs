using System;
using System.Collections.Generic;

namespace hospitalfinal.Models;

public partial class Sekreterler
{
    public int SekreterId { get; set; }

    public string SekreterAd { get; set; } = null!;

    public string SekreterSoyad { get; set; } = null!;

    public string SekreterTc { get; set; } = null!;

    public string Telefon { get; set; } = null!;

    public string? Email { get; set; }

    public string Sifre { get; set; } = null!;
}
