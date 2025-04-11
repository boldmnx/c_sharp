using System;
using System.Collections.Generic;

namespace lab9_10.Models;

public partial class Worker
{
    public int Wid { get; set; }

    public string? Wname { get; set; }

    public int Bid { get; set; }

    public virtual Branch BidNavigation { get; set; } = null!;
}
