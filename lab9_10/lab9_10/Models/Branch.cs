using System;
using System.Collections.Generic;

namespace lab9_10.Models;

public partial class Branch
{
    public int Bid { get; set; }

    public string? Bname { get; set; }

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
