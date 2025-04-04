using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lab7.Models;

public partial class TTurul
{
    public int Id { get; set; }

    [Display(Name ="Төрөл")]
    public string? Name { get; set; }

    public virtual ICollection<TMovie> TMovies { get; set; } = new List<TMovie>();
}
