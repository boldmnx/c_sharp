using System.ComponentModel.DataAnnotations.Schema;

namespace lab6.Models
{
    [ComplexType]    // 4
    public class Info
    {
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
    }
}
