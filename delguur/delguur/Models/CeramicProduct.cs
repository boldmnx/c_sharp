using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace delguur.Models
{
    public class CeramicProduct
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Бүтээгдэхүүний нэр шаардлагатай")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Тайлбар шаардлагатай")]
        [StringLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Үнэ шаардлагатай")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
