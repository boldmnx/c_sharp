using System;
using System.Collections.Generic;

namespace lab7.Models;

public partial class TMovie
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int Tid { get; set; }

    public int? Released { get; set; }

    public string? Image { get; set; }

    public double? Rating { get; set; }

    public virtual TTurul TidNavigation { get; set; } = null!;
}
