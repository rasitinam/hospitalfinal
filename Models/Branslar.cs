using System;
using System.Collections.Generic;

namespace hospitalfinal.Models;

public partial class Branslar
{
    public int BransId { get; set; }

    public string BransAd { get; set; } = null!;

    public virtual ICollection<Doktorlar> Doktorlars { get; set; } = new List<Doktorlar>();
}
