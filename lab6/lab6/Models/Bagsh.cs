using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using lab6.Models;
using Microsoft.EntityFrameworkCore;

namespace lab6.Models
{
    [Table("bagsh", Schema = "dbo")]
    public class Bagsh

    {
        [Column("bid", Order = 1)]
        public int Id { get; set; }

        [Column("bname", Order = 2, TypeName = "nvarchar(50)")]
        [Display(Name = "Багш")]
        public string Name { get; set; }

        [MinLength(10)]                         //8
        [MaxLength(10)]                         //9
        public string RegisteriinDugaar { get; set; }
        public int eid { get; set; }
        [ForeignKey("eid")]                                  //5
        public virtual ErdmiinZereg? ErdmiinZereg { get; set; }

        public Info? info { get; set; }
    }
}
