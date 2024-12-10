using System;
using System.Collections.Generic;

namespace hospitalfinal.Models;

public partial class RandevuMusait
{
    public int RandevuMusaitId { get; set; }

    public int DoktorId { get; set; }

    public DateTime MusaitTarih { get; set; }

    public TimeSpan MusaitSaat { get; set; }

    public int Durum { get; set; }

    public virtual Doktorlar Doktor { get; set; } = null!;
}
