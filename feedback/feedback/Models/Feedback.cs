using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace feedback.Models
{
    public class Feedback
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Харьяалагдах хэрэглэгчийн ID
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }

        // Админ хариу
        [StringLength(1000)]
        public string? AdminReply { get; set; }
    }

}
