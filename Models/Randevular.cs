using System;
using System.Collections.Generic;

namespace hospitalfinal.Models;

public partial class Randevular
{
    public int RandevuId { get; set; }

    public int HastaId { get; set; }

    public int DoktorId { get; set; }

    public DateTime RandevuTarihi { get; set; }

    public TimeSpan RandevuSaati { get; set; }

    public int OnayDurumu { get; set; }

    public virtual Doktorlar Doktor { get; set; } = null!;

    public virtual Hastalar Hasta { get; set; } = null!;
}
