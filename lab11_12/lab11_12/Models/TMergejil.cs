using System;
using System.Collections.Generic;

namespace lab11_12.Models;

public partial class TMergejil
{
    public string Mid { get; set; } = null!;

    public string? Name { get; set; }

    public int? Tid { get; set; }

    public virtual TTenhim? TidNavigation { get; set; }
}
