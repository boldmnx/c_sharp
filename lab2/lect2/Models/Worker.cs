using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace lect2.Models;

public partial class Worker
{
    public int Wid { get; set; }

    [Display(Name ="Ажилтан")]
    public string? Wname { get; set; }
    public int Bid { get; set; }
    [Display(Name = "Салбар")]
    public virtual Branch? BidNavigation { get; set; } = null!;
}
