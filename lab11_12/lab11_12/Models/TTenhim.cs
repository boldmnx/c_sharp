using System;
using System.Collections.Generic;

namespace lab11_12.Models;

public partial class TTenhim
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<TMergejil> TMergejils { get; set; } = new List<TMergejil>();
}
