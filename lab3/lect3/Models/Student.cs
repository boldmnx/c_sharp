using System.ComponentModel.DataAnnotations;


namespace lect3
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name ="Оюутан")]

        public string Name { get; set; }
        
        [Display(Name = "Мэргэжил")]
        public int? MajorId { get; set; }  
        public virtual Major? Major { get; set; }
    }
}
