using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.Models
{
    public class Discount
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DiscountId { get; set; }
        [Required]
        public int DiscountPercentage { get; set; }
        [Required]
        public int Description { get; set; }
        [Required]
        public DateOnly EndingDate { get; set; }
    }
}
