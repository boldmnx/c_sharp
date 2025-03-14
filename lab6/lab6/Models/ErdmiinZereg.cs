using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab6.Models
{
    [Table("ermiin_zereg", Schema = "dbo")] // 1
    public class ErdmiinZereg
    {
        [Key]                               // 2
        [Column("eid", Order = 1)]          // 3
        public int Id { get; set; }

        [Display(Name = "Эрдминй зэрэг")]                             // 6
        [Column("ename", Order = 2, TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Эрдмийн зэрэг оруулна уу!")]        // 7
        public string Name { get; set; }
        public Info Info { get; set; } = new Info();


        public virtual ICollection<Bagsh>? BagshNar { get; set; }

    }
}
