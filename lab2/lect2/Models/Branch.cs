using System.ComponentModel.DataAnnotations;

namespace lect2.Models;

public partial class Branch
{
    public int Bid { get; set; }

    [Display(Name ="Салбар нэр")]
    public string? Bname { get; set; }

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
