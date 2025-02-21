using System.ComponentModel.DataAnnotations;


namespace lect3
{
    public class Major
    {
        [Key]
        public int MajorId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Мэргэжил")]
        public string? MajorName { get; set; }

        public virtual ICollection<Student>? Students { get; set; } = new List<Student>();
    }
}
