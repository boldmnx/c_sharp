using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace lab6.Models
{
    [Owned]    // 4
    public class Info
    {
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Үүсгэсэн хүн")]
        public string? CreatedBy { get; set; }
    }
}
